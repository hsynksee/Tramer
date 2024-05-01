using TramerQuery.Data.Abstractions;
using TramerQuery.Data.Entities;

namespace TramerQuery.Data.RepositoryInterfaces.Interfaces
{
    public interface ITramerQueryResultRepository : IRepositoryBase<TramerQueryResult>
    {
        Task<TramerQueryResult> GetByQueryParameter(string queryParameter);
    }
}
