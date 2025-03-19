using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Domain.Entities;

namespace Tasks.DataAccess.Postgres.Configurations
{
    public class SprintStatusConfiguration : BaseEnumConfiguration<SprintStatusEntity>
    {
        protected override void Config(EntityTypeBuilder<SprintStatusEntity> builder)
        {
            builder.ToTable("SprintStatuses");
        }
    }
}
