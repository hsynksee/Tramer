using TramerQuery.Data.Abstractions;
using TramerQuery.Data.Entities;

namespace TramerQuery.Data.RepositoryInterfaces.Interfaces
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        Task<Company> FindByTaxNumber(string taxNumber);
    }
}
