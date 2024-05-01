using SharedKernel.Enum;
using TramerQuery.Data.Enums;

namespace TramerQuery.Service.Response.User
{
    public class UserResponse
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public UserRoleEnum RoleId { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
