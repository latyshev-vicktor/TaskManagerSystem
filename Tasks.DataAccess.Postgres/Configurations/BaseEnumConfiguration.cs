using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Domain.SeedWork;

namespace Tasks.DataAccess.Postgres.Configurations
{
    public abstract class BaseEnumConfiguration<TEnumEntity> : IEntityTypeConfiguration<TEnumEntity>
        where TEnumEntity : BaseEnumEntity
    {
        protected abstract void Config(EntityTypeBuilder<TEnumEntity> builder);
        public void Configure(EntityTypeBuilder<TEnumEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();

            Config(builder);
        }
    }
}
