using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ErrorOr;
using Core.Common.Errors.MightHappen;
using Core.Common.Http;

namespace Core.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public abstract class CustomControllerBase : ControllerBase
    {
        protected Guid RetrieveRequestSendingUserId()
        {
            Claim? userId = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userId is null)
            {
                //ExceptionsList.ThrowIdenificationTryException();
            }

            return Guid.Parse(userId!.Value);
        }

        protected IActionResult Problem(List<Error> errors)
        {
            if (errors.Count is 0)
            {
                return Problem();
            }

            if (errors.All(error => error.Type == ErrorType.Validation))
            {
                return ValidationProblem(errors);
            }

            Error firstError = errors.First();
            HttpContext.Items[HttpContextItemKeys.Errors] = errors;

            /// CUSTOM
            if (firstError == Errors.MightHappen.Authentication.InvalidCredentials)
            {
                return Problem(statusCode: StatusCodes.Status401Unauthorized, title: firstError.Description);
            }

            /// DEFAULT

            return Problem(firstError);
        }

        private IActionResult Problem(Error error)
        {
            int httpStatusCode = error.Type switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            return base.Problem(statusCode: httpStatusCode, title: error.Description);
        }

        private IActionResult ValidationProblem(List<Error> errors)
        {
            var modelStateDictionary = new ModelStateDictionary();

            foreach (var error in errors)
            {
                modelStateDictionary.AddModelError(error.Code, error.Description);
            }

            return base.ValidationProblem(modelStateDictionary);
        }
    }
}

