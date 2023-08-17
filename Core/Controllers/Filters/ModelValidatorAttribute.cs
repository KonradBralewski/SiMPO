using Blazorise;
using Core.Controllers.Base;
using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.Validation.Abstraction;

namespace Core.Controllers.Filters
{
    /// <summary>
    /// Filter that will try to receive fluentValidation validator before each request.
    /// </summary>
    public class ModelValidatorAttribute : ActionFilterAttribute
    {
        private CustomControllerBase? _controller;

        /// <summary>
        /// Method will try to receive validator if model inherits from IValidatable interface.
        /// Using reflection it will try to invoke ValidateAsync method from validator and process the result.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _controller = (CustomControllerBase)context.Controller;

            if (_controller is null) 
            {
                await base.OnActionExecutionAsync(context, next);
                return;
            }

            foreach (var actionArgument in context.ActionArguments)
            {
                if (actionArgument.Value is IValidatable validatableModel)
                {
                    var validatableModelType = actionArgument.Value.GetType();
                    var genericType = typeof(IValidator<>).MakeGenericType(validatableModelType);
                    var validator = context.HttpContext.RequestServices.GetService(genericType)!;

                    if (validator != null)
                    {
                        var validateAsyncMethod = validator
                            .GetType()
                            .GetMethod("ValidateAsync", new Type[] { validatableModelType, typeof(CancellationToken) });

                        if (validateAsyncMethod is null)
                        {
                            await base.OnActionExecutionAsync(context, next);
                            return;
                        }

                        var validationResult = await (Task<ValidationResult>)validateAsyncMethod!
                            .Invoke(validator, new[] { validatableModel, null })!;

                        if (validationResult is not null && !validationResult.IsValid)
                        {
                            var validationProblem = CreateValidationProblem(validationResult.Errors.ToList());
                            context.Result = validationProblem;
                        }
                    }
                }
            }
            await base.OnActionExecutionAsync(context, next);
        }

        private ActionResult CreateValidationProblem(List<ValidationFailure> validationFailures)
        {
            var errorList = validationFailures.Select(valFailure =>
                            Error.Validation(valFailure.PropertyName, valFailure.ErrorMessage)).ToList();

            var modelStateDictionary = new ModelStateDictionary();

            foreach (var error in errorList)
            {
                modelStateDictionary.AddModelError(error.Code, error.Description);
            }

            var validationProblem = _controller!.ValidationProblem(modelStateDictionary);

            return validationProblem;
        }
    }
}
