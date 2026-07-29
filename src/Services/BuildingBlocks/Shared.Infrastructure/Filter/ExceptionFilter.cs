using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PersonalFinance.Communication.Responses;
using PersonalFinance.Exception;
using PersonalFinance.Exception.ExceptionBase;

namespace Shared.Infrastructure.Filter;

public class ExceptionFilter: IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ExceptionBase)
        {
            HandleProjectException(context: context);
        }
        else
        {
            ThrowUnknowError(context: context);
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        ExceptionBase? exceptionBase = context.Exception as ExceptionBase;
        ErrorResponse errorResponse = new(errorMessages: exceptionBase!.GetErrors());

        context.HttpContext.Response.StatusCode = exceptionBase.StatusCode;
        context.Result = new ObjectResult(value: errorResponse);
    }

    private void ThrowUnknowError(ExceptionContext context)
    {
        ErrorResponse errorResponse = new(errorMessage: ResourceErrorMessages.UNKNOWN_ERROR);
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(value: errorResponse);
    }
}