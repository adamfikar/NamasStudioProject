using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using NamasStudio.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

DependencyInjectionService.AddServices(builder.Services);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/Auth/Login";
    options.AccessDeniedPath = "/Auth/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.


    // Handle 404 errors and use the 404.cshtml view
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStatusCodePagesWithRedirects("/Errors/{0}");


app.UseRouting();



app.UseAuthentication();
app.UseAuthorization();



app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "AccessDenied",
    pattern: "Auth/AccessDenied",
    defaults: new { controller = "Errors", action = "PageNeedAuthorize" }
    );

    endpoints.MapControllerRoute(
    name: "404",
    pattern: "Errors/404",
    defaults: new { controller = "Errors", action = "PageNotFound" }
    );
    // Other routes...

});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=login}");

app.Run();
