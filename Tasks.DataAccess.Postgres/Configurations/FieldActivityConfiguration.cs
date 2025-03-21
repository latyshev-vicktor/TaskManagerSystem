using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Domain.Entities;

namespace Tasks.DataAccess.Postgres.Configurations
{
    public class FieldActivityConfiguration : BaseConfiguration<FieldActivityEntity>
    {
        protected override void Config(EntityTypeBuilder<FieldActivityEntity> builder)
        {
            builder.ToTable("FieldActivities");
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
        }
    }
}
