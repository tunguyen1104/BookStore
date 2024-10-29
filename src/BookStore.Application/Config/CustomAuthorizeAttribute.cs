using BookStore.Domain.Entities;
using BookStore.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System.Security.Claims;

namespace BookStore.Application.Config
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private const string LoginController = "Auth";
        private const string LoginAction = "Login";
        private const string AccessDeniedAction = "AccessDenied";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userDataClaim = context.HttpContext.User.FindFirst(ClaimTypes.UserData)?.Value;

            if (string.IsNullOrEmpty(userDataClaim))
            {
                RedirectTo(context, LoginController, LoginAction);
                return;
            }
            var userConfig = JsonConvert.DeserializeObject<User>(userDataClaim);
            if (userConfig == null || !IsAuthorizedRole(userConfig.RoleId))
            {
                RedirectTo(context, LoginController, AccessDeniedAction);
            }
        }

        private static bool IsAuthorizedRole(long roleType)
        {
            return roleType == (long)RoleEnum.ADMIN;
        }

        private static void RedirectTo(AuthorizationFilterContext context, string controller, string action)
        {
            context.Result = new RedirectToRouteResult(
                new RouteValueDictionary { { "controller", controller }, { "action", action } }
            );
        }
    }
}
