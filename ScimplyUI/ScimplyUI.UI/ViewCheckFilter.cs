using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ScimplyUI.UI
{
	public class ViewCheckFilter : Attribute, IActionFilter
	{

		public void OnActionExecuted(ActionExecutedContext context)
		{
			
		}



		public void OnActionExecuting(ActionExecutingContext context)
		{

			var path = context.HttpContext.Request.Path.Value.ToLower();

			var token = context.HttpContext.Request.Cookies["AccessToken"];

			if (string.IsNullOrEmpty(token))
			{
				context.Result = new RedirectToActionResult("Login", "Auth", null);
			}
			else
			{
				Console.WriteLine($"Authorized entry");
			}

        }


	}
}
