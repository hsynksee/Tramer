using System.ComponentModel;

namespace SharedKernel.Enum
{
    public enum UserRoleEnum : int
    {
        [Description("Sistem Yöneticisi")]
        SystemAdmin = 1,
        [Description("Şirket Yöneticisi")]
        CompanyAdmin = 2,
        [Description("Personel")]
        Personnel = 3,
    }
}
