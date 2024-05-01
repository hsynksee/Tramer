using SharedKernel.Abstractions;
using SharedKernel.Enum;

namespace TramerQuery.Data.Entities
{
    public class User : AuditableBaseEntity
    {
        public int CompanyId { get; protected set; }
        public UserRoleEnum RoleId { get; protected set; }
        public string Name { get; protected set; }
        public string Surname { get; protected set; }
        public string Email { get; protected set; }
        public string? PhoneNumber { get; protected set; }
        public byte[] PasswordSalt { get; protected set; }
        public byte[] PasswordHash { get; protected set; }
        public bool IsActive { get; protected set; }
        public string? ProfilePicture { get; protected set; }
        public DateTime? LastLoginDate { get; protected set; }
        public Guid? ForgotPasswordKey { get; protected set; }
        public DateTime? ForgotPasswordValidDate { get; protected set; }

        public virtual Company Company { get; set; }

        public User SetBaseInformation(int companyId, UserRoleEnum roleId, string name, string surname, string emailPersonal, string phonePersonal, byte[] passwordSalt, byte[] passwordHash)
        {
            CompanyId = companyId;
            RoleId = roleId;
            Name = name;
            Surname = surname;
            Email = emailPersonal;
            PhoneNumber = phonePersonal;
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
            IsActive = true;


            return this;
        }

        public User Update(int companyId, UserRoleEnum roleId, string name, string surname, string email, string phoneNumber)
        {
            CompanyId = companyId;
            RoleId = roleId;
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phoneNumber;

            return this;
        }
        public User UpdatePicture(string? profilPicture)
        {
            ProfilePicture = profilPicture;

            return this;
        }

        public User SetActive(bool active)
        {
            IsActive = active;

            return this;
        }
        public User SetInActive()
        {
            IsActive = false;

            return this;
        }
        public User SetLoginDate()
        {
            LastLoginDate = DateTime.Now;

            return this;
        }

        public User SetForgotPasswordKey(Guid guid, DateTime validDate)
        {
            ForgotPasswordKey = guid;
            ForgotPasswordValidDate = validDate;

            return this;
        }

        public User SetChangePassword(byte[] passwordSalt, byte[] passwordHash)
        {
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
            ForgotPasswordKey = null;
            ForgotPasswordValidDate = null;

            return this;
        }
    }
}
