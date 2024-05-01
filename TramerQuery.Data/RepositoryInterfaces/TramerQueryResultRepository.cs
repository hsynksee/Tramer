using HandlebarsDotNet;
using Microsoft.EntityFrameworkCore;
using TramerQuery.Data.Abstractions;
using TramerQuery.Data.Entities;
using TramerQuery.Data.RepositoryInterfaces.Interfaces;

namespace TramerQuery.Data.RepositoryInterfaces
{
    public class TramerQueryResultRepository : IRepositoryBase<TramerQueryResult>, ITramerQueryResultRepository
    {
        protected TramerQueriesContext _ctx { get; set; }
        public TramerQueryResultRepository(TramerQueriesContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<TramerQueryResult> Create(TramerQueryResult entity)
        {
            await _ctx.TramerQueryResults.AddAsync(entity);
            return entity;
        }

        public async Task Delete(int id)
        {
            _ctx.TramerQueryResults.Remove(await FindById(id));
        }

        public async Task<TramerQueryResult> FindById(int id)
        {
            return await _ctx.TramerQueryResults.SingleOrDefaultAsync(t => t.Id == id);
        }

        public Task<List<TramerQueryResult>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<TramerQueryResult> GetByQueryParameter(string queryParameter)
        {
            return await _ctx.TramerQueryResults
                .SingleOrDefaultAsync(t => t.IsActive && t.IsSuccess && t.QueryParameter == queryParameter);
        }

        public void Update(TramerQueryResult entity)
        {
            _ctx.TramerQueryResults.Update(entity);
        }
    }
}
