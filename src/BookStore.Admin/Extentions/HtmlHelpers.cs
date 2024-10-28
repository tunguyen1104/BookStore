using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Admin.Extentions
{
    public static class HtmlHelpers
    {
        public static string IsActive(this IHtmlHelper html, string? controllers = null, string? actions = null, string cssClass = "active")
        {
            var routeData = html.ViewContext.RouteData;

            var currentAction = routeData.Values["Action"]?.ToString();
            var currentController = routeData.Values["Controller"]?.ToString();

            var acceptedAction = (actions ?? currentAction)?.Split(',');
            var acceptedControllers = (controllers ?? currentController)?.Split(",");

            return acceptedAction.Contains(currentAction) && acceptedControllers.Contains(currentController) ? cssClass : string.Empty;
        }
    }
}
