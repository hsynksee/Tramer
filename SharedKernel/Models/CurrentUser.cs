using SharedKernel.Enum;

namespace SharedKernel.Models
{
    public class CurrentUser
    {
        public int Id { get; set; }
        public UserRoleEnum RoleId { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

    }
}
