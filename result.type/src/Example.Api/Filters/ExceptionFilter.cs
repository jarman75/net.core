using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Example.Api.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Example.Api.Filters;

//middleware to catch exceptions and return a response

public class ExceptionFilter : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case FeatureDisabledException _:
                //405 Method Not Allowed
                context.Result = new StatusCodeResult(405);
                break;
            case NotFoundException _:
                //404 Not Found
                context.Result = new NotFoundObjectResult(context.Exception.Message);
                break;
            default:
                //500 Internal Server Error
                context.Result = new StatusCodeResult(500);
                break;
        }

        return Task.CompletedTask;
    }
}