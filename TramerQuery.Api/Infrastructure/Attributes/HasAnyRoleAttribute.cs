using Microsoft.AspNetCore.Mvc.Filters;
using SharedKernel.Abstractions;
using SharedKernel.Enum;
using SharedKernel.Exceptions;
using System.Linq;
using System.Security;

namespace TramerQuery.Api.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class HasAnyRoleAttribute : ActionFilterAttribute
    {
        private readonly UserRoleEnum[] _userRoles;

        public HasAnyRoleAttribute(UserRoleEnum[] userRoles)
        {
            _userRoles = userRoles;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var svc = filterContext.HttpContext.RequestServices;
            var appSettings = (IAppSettings)svc.GetService(typeof(IAppSettings));

            if (appSettings.CurrentUser?.RoleId == null |
                !_userRoles.Any(a => appSettings.CurrentUser.RoleId == a))
                throw new ForbiddenAccessException();

            base.OnActionExecuting(filterContext);
        }
    }
}
