using Microsoft.EntityFrameworkCore;
using TramerQuery.Data.Abstractions;
using TramerQuery.Data.Entities;
using TramerQuery.Data.RepositoryInterfaces.Interfaces;

namespace TramerQuery.Data.RepositoryInterfaces
{
    public class UserRepository : IRepositoryBase<User>, IUserRepository
    {
        protected TramerQueriesContext _ctx { get; set; }
        public UserRepository(TramerQueriesContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<User> Create(User entity)
        {
            await _ctx.Users.AddAsync(entity);
            return entity;
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindById(int id)
        {
            return await _ctx.Users
                .Include(c=>c.Company)
                .SingleOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<User>> GetAll()
        {
            return await _ctx.Users
                .Include(i => i.Company)
                .OrderBy(o => o.Name).ThenBy(t => t.Surname).ToListAsync();
        }

        public async Task<User> GetByAuthKey(Guid forgotPasswordKey)
        {
            return await _ctx.Users.SingleOrDefaultAsync(f => f.ForgotPasswordKey == forgotPasswordKey && f.ForgotPasswordValidDate.Value >= DateTime.Now);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _ctx.Users.SingleOrDefaultAsync(f => f.Email == email);
        }

        public void Update(User entity)
        {
            _ctx.Users.Update(entity);
        }

        public async Task<List<User>> FindByCompanyId(int companyId)
        {
            return await _ctx.Users
                .Include(i => i.Company)
                .Where(f => f.CompanyId == companyId)
                .OrderBy(o => o.Name).ThenBy(t => t.Surname).ToListAsync();
        }
    }
}
