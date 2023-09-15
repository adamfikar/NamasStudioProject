using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NamasStudio.API.Models;
using NamasStudio.Services.Exceptions;
using System.Net;

namespace NamasStudio.API.Filters
{
    public class JsonExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var response = new JsonResponse<object>
            {
                Status = ResponseStatus.Error
            };

            switch (context.Exception)
            {
                case UnauthorizedAccessException e:
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.Message = e.Message;
                    break;
                case ForbiddenException e:
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    response.Message = e.Message;
                    break;
                case ArgumentException e:
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = e.Message;
                    break;
                default:
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Message = context.Exception.Message;
                    break;
            }

            context.Result = new JsonResult(response);
        }
    }
}
