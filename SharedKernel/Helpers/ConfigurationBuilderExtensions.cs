using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SharedKernel.Helpers
{
    public static class ConfigurationBuilderExtensions
    {
        private static readonly string AuditableInterfaceName = "IAuditable";

        private static readonly string PkColumnName = "Id";
        private static readonly string DeletionColumnName = "IsDeleted";
        private static readonly string CreatedByColumnName = "CreatedUserId";
        private static readonly string CreatedOnColumnName = "CreatedDate";

        //public static PropertyBuilder<decimal?> HasPrecision(this PropertyBuilder<decimal?> builder, int precision, int scale)
        //{
        //    return builder.HasColumnType($"decimal({precision},{scale})");
        //}

        //public static PropertyBuilder<decimal> HasPrecision(this PropertyBuilder<decimal> builder, int precision, int scale)
        //{
        //    return builder.HasColumnType($"decimal({precision},{scale})");
        //}

        public static void AddDefaults<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class
        {
            builder.HasKey(PkColumnName);
            builder.Property(PkColumnName).IsRequired();
            builder.Property(PkColumnName).ValueGeneratedOnAdd();

            if (typeof(TEntity).GetInterfaces().Any(i => i.Name == AuditableInterfaceName))
            {
                builder.Property(CreatedByColumnName).IsRequired();
                builder.Property(CreatedOnColumnName).IsRequired();
            }

            if (typeof(TEntity).GetProperties().Any(i => i.Name == DeletionColumnName))
            {
                builder.HasQueryFilter(b => !EF.Property<bool>(b, DeletionColumnName));
                builder.Property(DeletionColumnName).IsRequired();
            }
        }
    }
}
