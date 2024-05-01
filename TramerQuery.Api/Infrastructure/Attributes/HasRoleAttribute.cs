using Microsoft.AspNetCore.Mvc.Filters;
using SharedKernel.Abstractions;
using SharedKernel.Enum;
using SharedKernel.Exceptions;
using System.Security;

namespace TramerQuery.Api.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class HasRoleAttribute : ActionFilterAttribute
    {
        private readonly UserRoleEnum _userRole;

        public HasRoleAttribute(UserRoleEnum userRole) 
        {
            _userRole = userRole;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var svc = filterContext.HttpContext.RequestServices;
            var appSettings = (IAppSettings)svc.GetService(typeof(IAppSettings));

            if (appSettings.CurrentUser?.RoleId == null |
                appSettings.CurrentUser.RoleId != _userRole)
                throw new ForbiddenAccessException();

            base.OnActionExecuting(filterContext);
        }
    }
}
