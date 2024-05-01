using Microsoft.AspNetCore.Mvc;
using SharedKernel.Enum;
using TramerQuery.Api.Infrastructure.Abstractions;
using TramerQuery.Api.Infrastructure.Attributes;
using TramerQuery.Service.Request.User;
using TramerQuery.Service.ServiceInterfaces.Interfaces;

namespace TramerQuery.Api.Controllers
{
    public class UserController : BaseController
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Kullanıcı Ekleme
        /// </summary>
        /// <param name="request">UserCreateRequest</param>
        /// <returns></returns>
        [HttpPost("CreateUser")]
        [HasAnyRole(userRoles: new UserRoleEnum[] { UserRoleEnum.SystemAdmin, UserRoleEnum.CompanyAdmin })]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateRequest request) => Ok(await _userService.CreateUser(request));

        /// <summary>
        /// Kullanıcı Bilgilerini Güncelleme
        /// </summary>
        /// <param name="request">UserUpdateRequest</param>
        /// <returns></returns>
        [HttpPut("UpdateUser")]
        [HasAnyRole(userRoles: new UserRoleEnum[] { UserRoleEnum.SystemAdmin, UserRoleEnum.CompanyAdmin })]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest request) => Ok(await _userService.UpdateUser(request));

        /// <summary>
        /// Kullanıcıları Listeleme
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUsers")]
        [HasAnyRole(userRoles: new UserRoleEnum[] { UserRoleEnum.SystemAdmin, UserRoleEnum.CompanyAdmin })]
        public async Task<IActionResult> GetUsers(bool? isActive = null) => Ok(await _userService.GetUsers(isActive));

        /// <summary>
        /// Kullanıcı Biglsini Getime
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns></returns>
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(int id) => Ok(await _userService.GetUserById(id));

        /// <summary>
        /// Firma Bazlı Kullanıcı Listesi Getime
        /// </summary>
        /// <param name="companyId">Firma Id</param>
        /// <returns></returns>
        [HttpGet("GetUserByCompanyId")]
        [HasAnyRole(userRoles: new UserRoleEnum[] { UserRoleEnum.SystemAdmin, UserRoleEnum.CompanyAdmin })]
        public async Task<IActionResult> GetUserByCompanyId(int companyId, bool? isActive = null) => Ok(await _userService.GetUserByCompanyId(companyId, isActive));

        /// <summary>
        /// Kullanıcı Aktif/Pasif Yapma
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns></returns>
        [HttpPut("SetUserActive")]
        [HasAnyRole(userRoles: new UserRoleEnum[] { UserRoleEnum.SystemAdmin, UserRoleEnum.CompanyAdmin })]
        public async Task<IActionResult> SetUserActive(int id) => Ok(await _userService.SetUserActive(id));

        /// <summary>
        /// Kullanıcı Şifre Değiştirme
        /// </summary>
        /// <param name="request">ChangePasswordRequest</param>
        /// <returns></returns>
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request) => Ok(await _userService.ChangePassword(request));
    }
}
