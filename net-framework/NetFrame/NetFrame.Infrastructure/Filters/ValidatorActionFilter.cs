using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetFrame.Infrastructure
{
    public class ValidatorActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var validationMessages = new List<string>();
                foreach (var item in context.ModelState)
                {
                    foreach (var error in item.Value.Errors)
                    {

                        validationMessages.Add(error.ErrorMessage);
                    }
                }
                var msj = String.Join(", ", validationMessages.ToArray());
                context.Result = new BadRequestObjectResult(new ServiceResult(msj, ResultType.Warning))
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Method intentionally left empty.
        }
    }
}
