using Microsoft.EntityFrameworkCore;
using TramerQuery.Data.Abstractions;
using TramerQuery.Data.Entities;
using TramerQuery.Data.RepositoryInterfaces.Interfaces;

namespace TramerQuery.Data.RepositoryInterfaces
{
    public class UserTramerQueryRepository : IRepositoryBase<UserTramerQuery>, IUserTramerQueryRepository
    {
        protected TramerQueriesContext _ctx { get; set; }
        public UserTramerQueryRepository(TramerQueriesContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<UserTramerQuery>> GetAll()
        {
            return await _ctx.UserTramerQueries
                .Include(i=>i.User)
                .ThenInclude(a=>a.Company)
                .ToListAsync();
        }

        public async Task<List<UserTramerQuery>> GetAllByDate(DateTime startDate,DateTime endDate)
        {
            return await _ctx.UserTramerQueries
                            .Include(i => i.User)
                                .ThenInclude(u => u.Company)
                            .Include(i => i.TramerQueryResult)
                            .Include(i => i.OldTramerQueryResult)
                            .Where(c => c.CreatedDate >= startDate && c.CreatedDate <= endDate)
                            .ToListAsync();
        }

        public Task<UserTramerQuery> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserTramerQuery> Create(UserTramerQuery entity)
        {
            await _ctx.UserTramerQueries.AddAsync(entity);
            return entity;
        }

        public void Update(UserTramerQuery entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
