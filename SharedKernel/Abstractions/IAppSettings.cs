using SharedKernel.Helpers;
using SharedKernel.Models;

namespace SharedKernel.Abstractions
{
    public interface IAppSettings
    {
        CurrentUser CurrentUser { get; }
        JwtSettings JwtSettings { get; }
        ApplicationConfigurations ApplicationConfigurations { get; }
        MailSenderInfo MailSenderInfo { get; }
        bool IsExists { get; }
    }
}
