using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SustentApp.DataTransfer.Utils.Errors.Response;
using SustentApp.Domain.Utils.Exceptions;

namespace SustentApp.API.Utils.Filters;

public class ExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        this._logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        Exception ex = context.Exception.InnerException ?? context.Exception;

        if (ex is RecordNotFoundException)
            context.HttpContext.Response.StatusCode = 404;
        else if (ex is DomainException)
            context.HttpContext.Response.StatusCode = 400;
        else
            context.HttpContext.Response.StatusCode = 500;

        context.Result = new JsonResult(new ErrorResponse
        {
            Message = context.HttpContext.Response.StatusCode == 500 ? "Internal Server Error." : ex.Message,
        });
    }
}
