using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NamasStudio.API.Filters;
using NamasStudio.Service;
using NamasStudio.Services.Exceptions;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace NamasStudio.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            DependencyInjectionService.AddServices(builder.Services);

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("https://localhost:7002")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            builder.Services.AddControllers(options => options.Filters.Add(new JsonExceptionFilterAttribute()))
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Namas Studio API", Version = "v1" });
                c.AddSecurityDefinition("oauth2",
                new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the bearer scheme (bearer {token})",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });

      

            var app = builder.Build();

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    context.Response.ContentType = "application/json";

                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    if (exceptionHandlerPathFeature?.Error is ForbiddenException)
                    {
                        await context.Response.WriteAsync("You don't have permission to access this resource.");
                    }
                    else
                    {
                        await context.Response.WriteAsync("An unexpected error occurred.");
                    }
                });
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(); // Enable CORS middleware
            app.UseAuthentication();
            app.UseAuthorization();

      

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}
