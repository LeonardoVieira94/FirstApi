using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalog.Filters;

public class ApiExceptionFilter :IExceptionFilter
{
    private readonly ILogger<ApiExceptionFilter> _logger;

    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "Not handled exception");
        context.Result = new ObjectResult("There was an error to process your request. Status Code: 500")
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }
}
