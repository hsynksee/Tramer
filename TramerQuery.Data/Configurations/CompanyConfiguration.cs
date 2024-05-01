using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TramerQuery.Data.Entities;
using SharedKernel.Helpers;

namespace TramerQuery.Data.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.AddDefaults();
            builder.Property(c => c.Name).IsRequired();
        }
    }
}
