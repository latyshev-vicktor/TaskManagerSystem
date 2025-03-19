using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Domain.Entities;

namespace Tasks.DataAccess.Postgres.Configurations
{
    public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        protected abstract void Config(EntityTypeBuilder<TEntity> builder);

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.DeletedDate).HasDefaultValue(null).IsRequired(false);
            builder.HasQueryFilter(x => !x.IsDeleted);

            Config(builder);
        }
    }
}
