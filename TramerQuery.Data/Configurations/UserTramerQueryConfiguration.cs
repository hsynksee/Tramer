using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Helpers;
using TramerQuery.Data.Entities;

namespace TramerQuery.Data.Configurations
{
    public class UserTramerQueryConfiguration : IEntityTypeConfiguration<UserTramerQuery>
    {
        public void Configure(EntityTypeBuilder<UserTramerQuery> builder)
        {
            builder.AddDefaults();
        }
    }
}

