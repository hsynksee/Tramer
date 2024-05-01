using Microsoft.EntityFrameworkCore;
using TramerQuery.Data.Abstractions;
using TramerQuery.Data.Entities;
using TramerQuery.Data.RepositoryInterfaces.Interfaces;

namespace TramerQuery.Data.RepositoryInterfaces
{
    public class CompanyRepository : IRepositoryBase<Company>, ICompanyRepository
    {
        protected TramerQueriesContext _ctx { get; set; }

        public CompanyRepository(TramerQueriesContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<Company>> GetAll()
        {
            return await _ctx.Companies.OrderBy(o => o.Name).ToListAsync();
        }

        public async Task<Company> FindById(int id)
        {
            return await _ctx.Companies.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Company> FindByTaxNumber(string taxNumber)
        {
            return await _ctx.Companies.FirstOrDefaultAsync(f => f.TaxNumber == taxNumber);
        }

        public async Task<Company> Create(Company entity)
        {
            await _ctx.Companies.AddAsync(entity);
            return entity;
        }

        public void Update(Company entity)
        {
            _ctx.Companies.Update(entity);
        }

        public async Task Delete(int id)
        {
            _ctx.Companies.Remove(await FindById(id));
        }
    }
}
