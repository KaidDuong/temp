using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Rikkonbi.WebAPI.Filters
{
    /// <summary>
    /// Introduces model state auto validation to reduce code duplication
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute" />
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Validates model automaticaly
        /// </summary>
        /// <param name="context">Current action executing context</param>
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }

            base.OnActionExecuting(context);
        }
    }
}