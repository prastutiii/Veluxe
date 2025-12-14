using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace Veluxe.Filters
{
   

    namespace Veluxe.Filters
    {
        public class AdminAuthorizeAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                var role = context.HttpContext.Session.GetString("user_role");

                if (role != "Admin")
                {
                    context.Result = new RedirectToActionResult("Login", "Registration", null);
                }

                base.OnActionExecuting(context);
            }
        }
    }

}
