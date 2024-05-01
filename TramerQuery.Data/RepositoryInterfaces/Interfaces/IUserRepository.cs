using TramerQuery.Data.Abstractions;
using TramerQuery.Data.Entities;

namespace TramerQuery.Data.RepositoryInterfaces.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<List<User>> FindByCompanyId(int companyId);
        Task<User> GetByEmail(string email);
        Task<User> GetByAuthKey(Guid forgotPasswordKey);
    }
}
