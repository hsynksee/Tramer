using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Abstractions;
using SharedKernel.Helpers;
using TramerQuery.Api.Infrastructure.Abstractions;
using TramerQuery.Service.Request.User;
using TramerQuery.Service.ServiceInterfaces.Interfaces;

namespace TramerQuery.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IAppSettings _appSettings;

        public AuthController(IAuthService authService, IUserService userService, IAppSettings appSettings) : base()
        {
            _authService = authService;
            _appSettings = appSettings;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateRequest request)
        {
            var response = await _authService.Login(request);

            if (response.Data == null) return BadRequest(response);

            var claims = new Dictionary<string, object>()
            {
                { "unique_name", response.Data.Id },
                { "user", response.Data }
            };

            var token = JwtBuilder.Build(claims, _appSettings.JwtSettings);

            return Ok(new BaseResponse<object?>(token));
        }


        [HttpGet("CurrentUser")]
        public async Task<IActionResult> CurrentUser() => Ok(await _authService.CurrentUser());

        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotRequest request) => Ok(await _userService.ForgotPassword(request));

        [AllowAnonymous]
        [HttpPost("ForgotChangePassword")]
        public async Task<IActionResult> ForgotChangePassword([FromBody] ForgotChangePassword request) => Ok(await _userService.ForgotChangePassword(request));
    }
}
