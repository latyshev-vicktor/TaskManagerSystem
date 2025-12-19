using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notification.Domain.SeedWork;

namespace Notification.DataAccess.Postgres.Configurations
{
    public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity 
    {
        public abstract void Config(EntityTypeBuilder<TEntity> builder);

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.DeletedDate).HasDefaultValue(null);
            builder.HasQueryFilter(x => !x.IsDeleted);
            builder.Property(x => x.CreatedDate).IsRequired();

            Config(builder);
        }
    }
}
