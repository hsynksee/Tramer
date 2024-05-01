using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Helpers;
using TramerQuery.Data.Entities;

namespace TramerQuery.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.AddDefaults();
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Surname).HasMaxLength(50).IsRequired();
            builder.Property(c => c.PasswordSalt).IsRequired();
            builder.Property(c => c.PasswordHash).IsRequired();
        }
    }
}
