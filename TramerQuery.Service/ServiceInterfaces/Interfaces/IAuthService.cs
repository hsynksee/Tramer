using Microsoft.AspNetCore.Mvc;
using SharedKernel.Abstractions;
using SharedKernel.Models;
using TramerQuery.Service.Request.User;

namespace TramerQuery.Service.ServiceInterfaces.Interfaces
{
    public interface IAuthService
    {
        Task<BaseResponse<CurrentUser>> Login([FromBody] AuthenticateRequest request);
        Task<BaseResponse<CurrentUser>> CurrentUser();
    }
}
