using Microsoft.EntityFrameworkCore;
using SharedKernel.Abstractions;
using SharedKernel.Constants;
using System.Reflection;
using TramerQuery.Data.Entities;

namespace TramerQuery.Data
{
    public class TramerQueriesContext : DbContext
    {
        private IAppSettings _appSettings;

        public TramerQueriesContext(IAppSettings appSettings, DbContextOptions<TramerQueriesContext> options) : base(options)
        {
            _appSettings = appSettings;

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<TramerQueryResult> TramerQueryResults { get; set; }
        public DbSet<UserTramerQuery> UserTramerQueries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(GetType()));

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            CheckForSoftDelete();
            FillAudits();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void FillAudits()
        {
            this.ChangeTracker.DetectChanges();

            foreach (var entry in this.ChangeTracker.Entries<AuditableBaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedUserId = _appSettings.CurrentUser != null ? _appSettings.CurrentUser.Id : SystemConstants.SystemUserId;
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.IsDeleted = false;

                }
                else if (entry.State == EntityState.Modified && _appSettings.IsExists)
                {
                    entry.Entity.UpdatedUserId = _appSettings.CurrentUser != null ? _appSettings.CurrentUser.Id : SystemConstants.SystemUserId;
                    entry.Entity.UpdatedDate = DateTime.Now;
                }
            }
        }

        private void CheckForSoftDelete()
        {
            var deletedEntities = this.ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted);

            foreach (var item in deletedEntities)
            {
                if (item.Entity is AuditableBaseEntity entity)
                {
                    item.State = EntityState.Modified;
                    entity.IsDeleted = true;
                }
                else if (item.Entity is BaseEntity baseEntity)
                {
                    item.State = EntityState.Modified;
                    baseEntity.IsDeleted = true;
                }
            }
        }
    }
}
