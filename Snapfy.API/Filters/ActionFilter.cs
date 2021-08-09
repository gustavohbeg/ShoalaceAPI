using Microsoft.AspNetCore.Mvc.Filters;
using Shoalace.Domain.Commands;

namespace Shoalace.API.Filters
{
    public class ActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                if (context.ActionArguments["comando"] is Command comando)
                {
                    /*comando.ClienteId = int.Parse(context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                    comando.AcessoId = int.Parse(context.HttpContext.User.FindFirstValue(ClaimTypes.Actor));
                    comando.ClienteNome = context.HttpContext.User.FindFirstValue(ClaimTypes.Name);*/
                }
            }
            catch (System.Exception)
            {
            }
        }
    }
}
