using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ScimplyUI.UI
{
    public class SessionCheckFilter : Attribute, IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }


        public void OnActionExecuting(ActionExecutingContext context)
        {
            var path = context.HttpContext.Request.Path.Value.ToLower();

            var userId = context.HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId)) { context.Result = new RedirectToActionResult("Login", "Auth", null); }

            else { }
        }

    }
}
