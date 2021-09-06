using Bytka.Lib;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ProjectTemplate.Api
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (context.Error is ValidationException ve)
                return ValidationProblem(ve.Message, null, 400, "Request validation failed");

            if (context.Error is NotFoundException)
                return NotFound();

            return Problem(context.Error.Message);
        }

#if DEBUG
        [HttpGet("/error-test1")]
        public IActionResult ValidationExceptionTest()
        {
            throw new ValidationException("Validation exception test");
        }

        [HttpGet("/error-test2")]
        public IActionResult GenericExceptionTest()
        {
            throw new Exception("Generic exception test");
        }
#endif
    }
}