using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TramerQuery.Data.RepositoryInterfaces.Interfaces;

namespace TramerQuery.Data.Abstractions
{
    public interface IRepositoryWrapper
    {
        IUserRepository UserRepository { get; }
        ICompanyRepository CompanyRepository { get; }
        ITramerQueryResultRepository TramerQueryResultRepository { get; }
        IUserTramerQueryRepository UserTramerQueryRepository { get; }

        Task<int> Save();
    }
}
