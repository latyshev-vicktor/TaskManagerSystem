using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Domain.Errors;
using Tasks.Domain.SeedWork;
using Tasks.Domain.ValueObjects;

namespace Tasks.Domain.Entities
{
    public class TargetEntity : BaseEntity
    {
        public TargetName Name { get; private set; }
        public long SprintId { get; private set; }
        public SprintEntity? Sprint { get; private set; }

        private List<TaskEntity> _tasks = [];
        public IReadOnlyList<TaskEntity> Tasks => _tasks;

        #region Конструкторы
        protected TargetEntity() { }

        protected TargetEntity(TargetName name, SprintEntity sprint)
        {
            Name = name;
            Sprint = sprint;
        }
        #endregion

        #region DDD-методы
        public static IExecutionResult<TargetEntity> Create(string name, SprintEntity sprint)
        {
            if (sprint == null)
                return ExecutionResult.Failure<TargetEntity>(TargetError.SprintNotBeNull());

            var nameResult = TargetName.Create(name);
            if (nameResult.IsFailure)
                return ExecutionResult.Failure<TargetEntity>(nameResult.Error);

            return ExecutionResult.Success(new TargetEntity(nameResult.Value, sprint));
        }

        public IExecutionResult SetName(string name)
        {
            var nameResult = TargetName.Create(name);
            if (nameResult.IsFailure)
                return ExecutionResult.Failure<TargetEntity>(nameResult.Error);

            Name = nameResult.Value;

            return ExecutionResult.Success();
        }

        public void AddTask(TaskEntity task)
        {
            if (Tasks.Any(x => x.Id == task.Id) == false)
                _tasks.Add(task);
        }

        public override void Delete()
        {
            foreach(var task in _tasks)
                task.Delete();

            base.Delete();
        }

        #endregion

    }
}
