using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Domain.Errors;
using Tasks.Domain.SeedWork;
using Tasks.Domain.ValueObjects;

namespace Tasks.Domain.Entities
{
    public class TaskEntity : BaseEntity
    {
        public TaskName Name { get; private set; }
        public TaskDescription Description { get; private set; }
        public long TargetId { get; private set; }
        public TargetEntity? Target { get; private set; }
        public TasksStatus Status { get; private set; }
        public long? WeekId { get; set; }
        public SprintWeekEntity? Week { get; private set; }

        protected TaskEntity() { }

        protected TaskEntity(TaskName name, TaskDescription description, long targetId, long? weekId)
        {
            Name = name;
            Description = description;
            TargetId = targetId;
            Status = TasksStatus.Created;
            WeekId = weekId;
        }

        #region DDD-методы
        public static IExecutionResult<TaskEntity> Create(
            string name,
            string description,
            long targetId,
            long? weekId)
        {
            var nameResult = TaskName.Create(name);
            if (nameResult.IsFailure)
                return ExecutionResult.Failure<TaskEntity>(nameResult.Error);

            var descriptionResult = TaskDescription.Create(description);
            if (descriptionResult.IsFailure)
                return ExecutionResult.Failure<TaskEntity>(descriptionResult.Error);

            if (targetId == default)
                return ExecutionResult.Failure<TaskEntity>(TaskError.TargetIdNotFound());

            return ExecutionResult.Success(new TaskEntity(nameResult.Value, descriptionResult.Value, targetId, weekId));
        }

        public void Completed()
        {
            Status = TasksStatus.Completed;
        }

        public IExecutionResult SetName(string name)
        {
            var nameResult = TaskName.Create(name);
            if (nameResult.IsFailure)
                return ExecutionResult.Failure(nameResult.Error);

            Name = nameResult.Value;

            return ExecutionResult.Success();
        }

        public IExecutionResult SetDescription(string description)
        {
            var descriptionResult = TaskDescription.Create(description);
            if (descriptionResult.IsFailure)
                return ExecutionResult.Failure(descriptionResult.Error);

            Description = descriptionResult.Value;

            return ExecutionResult.Success();
        }

        public IExecutionResult SetWeek(long weekId)
        {
            if (weekId == default)
                return ExecutionResult.Failure<TaskEntity>(TaskError.WeekNotFound());

            WeekId = weekId;

            return ExecutionResult.Success();
        }
        #endregion
    }
}
