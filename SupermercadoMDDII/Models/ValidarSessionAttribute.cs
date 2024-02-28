using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace SupermercadoMDDII.Models
{
    public class ValidarSesionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;

            if (httpContext.Session.GetString("usuario") == null)
            {
                context.Result = new RedirectToRouteResult(
                   new RouteValueDictionary {
              {"controller", "Acceso"},
              {"action", "Login"}
                   });
            }

            base.OnActionExecuting(context);
        }
    }
}