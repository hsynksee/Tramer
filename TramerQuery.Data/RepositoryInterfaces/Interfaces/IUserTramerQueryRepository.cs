using TramerQuery.Data.Abstractions;
using TramerQuery.Data.Entities;

namespace TramerQuery.Data.RepositoryInterfaces.Interfaces
{
    public interface IUserTramerQueryRepository : IRepositoryBase<UserTramerQuery>
    {
        Task<List<UserTramerQuery>> GetAllByDate(DateTime startDate, DateTime endDate);
    }
}
