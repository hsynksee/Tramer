using FluentValidation;
using SharedKernel.Enum;
using TramerQuery.Data.Enums;

namespace TramerQuery.Service.Request.User
{
    public class UserCreateRequest
    {
        public int CompanyId { get; set; }
        public UserRoleEnum RoleId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
    {
        public UserCreateRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad alanı boş olamaz");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyad alanı boş olamaz");
            RuleFor(x => x.Email).NotEmpty().WithMessage("İş email alanı boş olamaz");
            RuleFor(x => x.RoleId).NotEmpty().WithMessage("Rol ataması yapılmalıdır.");
        }
    }
}
