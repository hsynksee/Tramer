using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Helpers;
using TramerQuery.Data.Entities;

namespace TramerQuery.Data.Configurations
{
    public class TramerQueryResultConfiguration : IEntityTypeConfiguration<TramerQueryResult>
    {
        public void Configure(EntityTypeBuilder<TramerQueryResult> builder)
        {
            builder.AddDefaults();
        }
    }
}