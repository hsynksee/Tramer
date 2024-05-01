using Microsoft.AspNetCore.Http;
using SharedKernel.Abstractions;
using SharedKernel.Constants;
using SharedKernel.Helpers;
using SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TramerQuery.Service.AppSettings
{
    public sealed class AppSettings : IAppSettings
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppSettings(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

        }

        public CurrentUser CurrentUser => GetValueFromContext<CurrentUser>(HttpContextKeys.CurrentUser);
        public JwtSettings JwtSettings => GetValueFromContext<JwtSettings>(HttpContextKeys.JwtSettings);
        public ApplicationConfigurations ApplicationConfigurations => GetValueFromContext<ApplicationConfigurations>(HttpContextKeys.ApplicationConfigurations);
        public MailSenderInfo MailSenderInfo => GetValueFromContext<MailSenderInfo>(HttpContextKeys.MailSenderInfo);

        public bool IsExists => CurrentUser != null;

        private T GetValueFromContext<T>(string contextKey)
        {
            if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Items.TryGetValue(contextKey, out object contextObject) && contextObject is T typedObject)
                return typedObject;

            return default;
        }
    }
}
