using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Domain.Entities;

namespace Tasks.DataAccess.Postgres.Configurations
{
    public class TaskStatusConfiguration : BaseEnumConfiguration<TaskStatusEntity>
    {
        protected override void Config(EntityTypeBuilder<TaskStatusEntity> builder)
        {
            builder.ToTable("TaskStatuses");
        }
    }
}
