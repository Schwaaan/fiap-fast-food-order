using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;

namespace FourSix.WebApi.Modules.Commons
{
    [ExcludeFromCodeCoverage]
    public sealed class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        ///     Add Problem Details when occurs Domain Exception.
        /// </summary>
        public void OnException(ExceptionContext context)
        {
            ProblemDetails problemDetails = new ProblemDetails { Status = 500, Title = "Bad Request" };

            context.Result = new JsonResult(problemDetails);
            context.Exception = null!;
        }
    }
}