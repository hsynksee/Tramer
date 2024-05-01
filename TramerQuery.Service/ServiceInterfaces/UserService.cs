using AutoMapper;
using SharedKernel.Abstractions;
using SharedKernel.Helpers;
using TramerQuery.Data.Abstractions;
using TramerQuery.Service.Request.User;
using TramerQuery.Data.Entities;
using TramerQuery.Service.ServiceInterfaces.Interfaces;
using TramerQuery.Service.Response.User;
using TramerQuery.Data.Enums;
using SharedKernel.Enum;
using SharedKernel.Models;
using Microsoft.AspNetCore.Mvc;

namespace TramerQuery.Service.ServiceInterfaces
{
    public class UserService : BaseAppService, IUserService
    {
        public UserService(IMapper mapper, IRepositoryWrapper repository, IAppSettings appSettings) : base(mapper, repository, appSettings)
        {
        }

        public async Task<BaseResponse<int>> CreateUser(UserCreateRequest request)
        {
            var checkUserEmail = await _repository.UserRepository.GetByEmail(request.Email);
            if (checkUserEmail != null)
                return new BaseResponse<int>(0, "Bu kullanıcı mail adresine sahip başka bir kullanıcı bulunmaktadır..");

            byte[] passwordHash, passwordSalt;
            string password = RandomPasswordGenerator.GeneratePassword(true, true, true, true, 8);// Otomatik şifre oluşlturma
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User().SetBaseInformation(
                request.CompanyId,
                request.RoleId,
                request.Name,
                request.Surname,
                request.Email,
                request.PhoneNumber,
                passwordSalt,
                passwordHash
                );

            await _repository.UserRepository.Create(user);
            var result = await _repository.Save() > 0;
            #region SentMail
            if (result)
            {
                var mailSenderInfo = _appSettings.MailSenderInfo;
                var mail = new MailMessageInfo()
                {
                    To = new List<string> { request.Email },
                    Subject = "Tramer Sorgu Kullanıcı Bilgileri",
                    Body = "Merhaba, <br /><br />Kullanıcınız oluşturulmuştur. <br /> Mail : " + request.Email + "<br />Şifre : " + password + "<br /><br />İyi Çalışmalar..",
                    MailSender = mailSenderInfo
                };
                MailHelper.SendEmail(mail);
            }
            #endregion

            return new BaseResponse<int>(user.Id);
        }

        public async Task<BaseResponse<int>> UpdateUser(UserUpdateRequest request)
        {
            var user = await _repository.UserRepository.FindById(request.Id);
            if (user == null)
                return new BaseResponse<int>(0, "Kullanıcı bulunamadı..");

            var checkUserEmail = await _repository.UserRepository.GetByEmail(request.Email);
            if (checkUserEmail != null && user.Id != checkUserEmail.Id)
                return new BaseResponse<int>(0, "Bu kullanıcı mail adresine sahip başka bir kullanıcı bulunmaktadır..");

            user.Update(
                request.CompanyId,
                request.RoleId,
                request.Name,
                request.Surname,
                request.Email,
                request.PhoneNumber);

            _repository.UserRepository.Update(user);
            await _repository.Save();

            return new BaseResponse<int>(user.Id);
        }

        public async Task<BaseResponse<List<UserResponse>>> GetUsers(bool? isActive = null)
        {
            var allUsers = _mapper.Map<List<UserResponse>>(await _repository.UserRepository.GetAll());

            if (_appSettings.CurrentUser.RoleId == UserRoleEnum.Personnel)
                return new BaseResponse<List<UserResponse>>(null);

            if (_appSettings.CurrentUser.RoleId == UserRoleEnum.CompanyAdmin)
                allUsers = allUsers.Where(w => w.CompanyId == _appSettings.CurrentUser.CompanyId).ToList();

            if (isActive != null)
                allUsers = allUsers.Where(w => w.IsActive == isActive).ToList();

            return new BaseResponse<List<UserResponse>>(allUsers);
        }

        public async Task<BaseResponse<UserResponse>> GetUserById(int id)
        {
            var user = _mapper.Map<UserResponse>(await _repository.UserRepository.FindById(id));

            if (_appSettings.CurrentUser.RoleId == UserRoleEnum.Personnel && _appSettings.CurrentUser.Id != id)
                return new BaseResponse<UserResponse>(null);
            if (_appSettings.CurrentUser.RoleId == UserRoleEnum.CompanyAdmin && _appSettings.CurrentUser.CompanyId != user.CompanyId)
                return new BaseResponse<UserResponse>(null);

            return new BaseResponse<UserResponse>(user);
        }

        public async Task<BaseResponse<List<UserResponse>>> GetUserByCompanyId(int companyId, bool? isActive = null)
        {
            var allUsers = _mapper.Map<List<UserResponse>>(await _repository.UserRepository.FindByCompanyId(companyId));

            if (_appSettings.CurrentUser.RoleId == UserRoleEnum.Personnel)
                return new BaseResponse<List<UserResponse>>(null);

            if (_appSettings.CurrentUser.RoleId == UserRoleEnum.CompanyAdmin)
                allUsers = allUsers.Where(w => w.CompanyId == _appSettings.CurrentUser.CompanyId).ToList();

            if (isActive != null)
                allUsers = allUsers.Where(w => w.IsActive == isActive).ToList();

            return new BaseResponse<List<UserResponse>>(allUsers);
        }

        public async Task<BaseResponse<bool>> SetUserActive(int id)
        {
            var user = await _repository.UserRepository.FindById(id);
            if (user == null)
                return new BaseResponse<bool>(false, "Kullanıcı bulunmamaktadır..");

            if (!user.Company.IsActive)
                return new BaseResponse<bool>(false, "Kullanıcı Firması aktif olmadığı için kullanıcı aktif yapılamaz..");

            user.SetActive(!user.IsActive);
            _repository.UserRepository.Update(user);
            await _repository.Save();

            string message = "";
            if (!user.IsActive) message = "Kullanıcı pasif yapılmıştır.";
            else message = "Kullanıcı aktif yapılmıştır.";

            return new BaseResponse<bool>(true, message);
        }

        public async Task<BaseResponse<bool>> ChangePassword(ChangePasswordRequest request)
        {
            if (_appSettings.CurrentUser.Id != request.UserId)
                return new BaseResponse<bool>(false, "Sadece kendi şifrenizi değiştirebilirsiniz..");

            var user = await _repository.UserRepository.FindById(request.UserId);

            var verifyPassword = HashingHelper.VerifyPasswordHash(request.OldPassword, user.PasswordHash, user.PasswordSalt);
            if (verifyPassword == false)
                return new BaseResponse<bool>(false, "Eski şifre hatalı..");

            if (request.NewPassword != request.NewPasswordRepeat)
                return new BaseResponse<bool>(false, "Şifreler uyuşmamaktadır..");

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(request.NewPassword, out passwordHash, out passwordSalt);

            user.SetChangePassword(passwordSalt, passwordHash);

            _repository.UserRepository.Update(user);
            await _repository.Save();

            return new BaseResponse<bool>(true, "Şifre değiştirilmiştir..");
        }

        public async Task<BaseResponse<bool>> ForgotPassword(ForgotRequest request)
        {
            var appConfig = _appSettings.ApplicationConfigurations;

            var user = await _repository.UserRepository.GetByEmail(request.Email);
            if (user == null)
                return new BaseResponse<bool>(false, "Kullanıcı bulunamamıştır..");
            else if (!user.IsActive)
                return new BaseResponse<bool>(false, "Kullanıcı bulunamamıştır..");

            Guid guidId = Guid.NewGuid();
            DateTime validDate = DateTime.Now.AddHours(8);
            user.SetForgotPasswordKey(guidId, validDate);
            _repository.UserRepository.Update(user);
            var result = await _repository.Save() > 0;

            #region SentMail
            if (result)
            {
                var mailSenderInfo = _appSettings.MailSenderInfo;
                var mail = new MailMessageInfo()
                {
                    To = new List<string> { request.Email },
                    Subject = "Tramer Sorgu Şifremi Unuttum",
                    Body = $"Merhaba, <br /><br />Şifrenizi { validDate.ToString("dd/MM/yyyy HH:mm") } tarihine kadar aşağıdaki şifre değiştirme url'i ile değiştirmeniz gerekmektedir.<br /><br /> Şifre Değiştirme Url : {appConfig.ChangePasswordUrl}/{guidId} <br /><br />İyi Çalışmalar..",
                    MailSender = mailSenderInfo
                };
                MailHelper.SendEmail(mail);
            }
            #endregion

            return new BaseResponse<bool>(true, "Şifre değişikliği için mail gönderilmiştir..");
        }

        public async Task<BaseResponse<bool>> ForgotChangePassword(ForgotChangePassword request)
        {
            var user = await _repository.UserRepository.GetByAuthKey(request.AuthKey);

            if (user == null)
                return new BaseResponse<bool>(false, "Kullanıcı bulunamamıştır..");
            else if (!user.IsActive)
                return new BaseResponse<bool>(false, "Kullanıcı bulunamamıştır..");

            if (request.NewPassword != request.NewPasswordRepeat)
                return new BaseResponse<bool>(false, "Şifreler uyuşmamaktadır..");

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(request.NewPassword, out passwordHash, out passwordSalt);

            user.SetChangePassword(passwordSalt, passwordHash);

            _repository.UserRepository.Update(user);
            await _repository.Save();

            return new BaseResponse<bool>(true, "Şifre değiştirilmiştir..");
        }
    }
}
