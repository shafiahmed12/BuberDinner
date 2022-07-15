using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BuberDinner.Api.Filters;

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        //if you want to log the exception you
        var ex = context.Exception;

        var problemDetails = new ProblemDetails
        {
            Title = "An unknown error occurred",
            Status = (int)HttpStatusCode.InternalServerError,
        };

        context.Result = new ObjectResult(problemDetails);
        context.ExceptionHandled =true;
    }
}