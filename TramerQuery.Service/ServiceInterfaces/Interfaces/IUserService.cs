using SharedKernel.Abstractions;
using TramerQuery.Service.Request.User;
using TramerQuery.Service.Response.User;

namespace TramerQuery.Service.ServiceInterfaces.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<int>> CreateUser(UserCreateRequest request);
        Task<BaseResponse<int>> UpdateUser(UserUpdateRequest request);
        Task<BaseResponse<List<UserResponse>>> GetUsers(bool? isActive = null);
        Task<BaseResponse<UserResponse>> GetUserById(int id);
        Task<BaseResponse<List<UserResponse>>> GetUserByCompanyId(int companyId, bool? isActive = null);
        Task<BaseResponse<bool>> SetUserActive(int id);
        Task<BaseResponse<bool>> ChangePassword(ChangePasswordRequest request);

        Task<BaseResponse<bool>> ForgotPassword(ForgotRequest request);
        Task<BaseResponse<bool>> ForgotChangePassword(ForgotChangePassword request);
    }
}
