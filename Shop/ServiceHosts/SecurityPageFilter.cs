using System.Reflection;
using _0_Framework.Application.Auth;
using _0_Framework.Repository;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ServiceHosts
{
    public class SecurityPageFilter : IPageFilter
    {
        private readonly IAuthHelper _authHelper;

        public SecurityPageFilter(IAuthHelper authHelper)
        {
            _authHelper = authHelper;
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var HandlerPermissions = (NeedsPermissionAttribute)
                context.HandlerMethod.MethodInfo.GetCustomAttribute(typeof(NeedsPermissionAttribute));

            if(HandlerPermissions == null)
                return;

            var AccountPermissions = _authHelper.GetPermissions();
            if (AccountPermissions.All(x => x != HandlerPermissions.Permission))
                context.HttpContext.Response.Redirect("/LoginReg");
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            
        }
    }
}
