using TramerQuery.Data.RepositoryInterfaces;
using TramerQuery.Data.RepositoryInterfaces.Interfaces;

namespace TramerQuery.Data.Abstractions
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private TramerQueriesContext _ctx;


        public RepositoryWrapper(TramerQueriesContext ctx)
        {
            _ctx = ctx;
        }

        private IUserRepository _users;
        private ICompanyRepository _company;
        private ITramerQueryResultRepository _tramerQueryResult;
        private IUserTramerQueryRepository _userTramerQueryRepository;

        public IUserRepository UserRepository
        {
            get
            {
                if (_users == null)
                    _users = new UserRepository(_ctx);

                return _users;
            }
        }

        public ICompanyRepository CompanyRepository
        {
            get
            {
                if (_company == null)
                    _company = new CompanyRepository(_ctx);

                return _company;
            }
        }

        public ITramerQueryResultRepository TramerQueryResultRepository
        {
            get
            {
                if (_tramerQueryResult == null)
                    _tramerQueryResult = new TramerQueryResultRepository(_ctx);

                return _tramerQueryResult;
            }
        }
        
        public IUserTramerQueryRepository UserTramerQueryRepository
        {
            get
            {
                if (_userTramerQueryRepository == null)
                    _userTramerQueryRepository = new UserTramerQueryRepository(_ctx);

                return _userTramerQueryRepository;
            }
        }

        public async Task<int> Save()
        {
            return await _ctx.SaveChangesAsync();
        }
    }
}
