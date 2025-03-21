using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Domain.DomainEvents;
using Tasks.Domain.Errors;
using Tasks.Domain.ValueObjects;

namespace Tasks.Domain.Entities
{
    public class SprintEntity : BaseEntity
    {
        /// <summary>
        /// Id пользователя из сервиса авторизации
        /// </summary>
        public long UserId { get; private set; }
        public SprintName Name { get; private set; }
        public SprintDescription Description { get; private set; }
        public DateTimeOffset StartDate { get; private set; }
        public DateTimeOffset EndDate { get; private set; }
        public SprintStatus Status { get; private set; }

        public long FieldActivityId { get; private set; }
        public FieldActivityEntity? FieldActivity { get; private set; }

        private List<TargetEntity> _targets = [];
        public IReadOnlyList<TargetEntity> Targets => _targets;

        #region Конструкторы
        protected SprintEntity()
        {
            
        }

        protected SprintEntity(
            long userId,
            SprintName name,
            SprintDescription description,
            long fieldActivityId,
            DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            UserId = userId;
            Name = name;
            Description = description;
            FieldActivityId = fieldActivityId;
            StartDate = startDate;
            EndDate = endDate;
            Status = SprintStatus.Created;
        }
        #endregion

        #region DDD-методы
        public static IExecutionResult<SprintEntity> Create(
            long userId,
            string name,
            string description,
            long fieldActivityId,
            DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            if (userId == default)
                return ExecutionResult.Failure<SprintEntity>(SprintError.UserNotEmpty());

            if (startDate.Date < DateTimeOffset.Now)
                return ExecutionResult.Failure<SprintEntity>(SprintError.StartDateNotBeLessNow());

            if (startDate.Date < DateTimeOffset.Now)
                return ExecutionResult.Failure<SprintEntity>(SprintError.EndDateNotBeLessNow());

            var nameResult = SprintName.Create(name);
            if (nameResult.IsFailure)
                return ExecutionResult.Failure<SprintEntity>(nameResult.Error);

            var descriptionResult = SprintDescription.Create(description);
            if (descriptionResult.IsFailure)
                return ExecutionResult.Failure<SprintEntity>(descriptionResult.Error);

            return ExecutionResult.Success(new SprintEntity(userId, nameResult.Value, descriptionResult.Value, fieldActivityId, startDate, endDate));
        }

        public IExecutionResult ChangeStatus(string status)
        {
            var statusResult = SprintStatus.Create(status);
            if (statusResult.IsFailure)
                return ExecutionResult.Failure(statusResult.Error);

            Status = statusResult.Value;

            return ExecutionResult.Success();
        }

        public IExecutionResult SetName(string name)
        {
            var nameResult = SprintName.Create(name);
            if (nameResult.IsFailure)
                return ExecutionResult.Failure(nameResult.Error);

            Name = nameResult.Value;

            return ExecutionResult.Success();
        }

        public IExecutionResult SetDescription(string description)
        {
            var descriptionResult = SprintDescription.Create(description);
            if (descriptionResult.IsFailure)
                return ExecutionResult.Failure(descriptionResult.Error);

            Description = descriptionResult.Value;

            return ExecutionResult.Success();
        }

        public IExecutionResult ChangeFieldActivity(long fieldActivityId)
        {
            if (fieldActivityId == default)
                return ExecutionResult.Failure(SprintError.NotFoundFieldActivity());

            FieldActivityId = fieldActivityId;

            return ExecutionResult.Success();
        }

        public override void Delete()
        {
            foreach(var target in _targets)
                target.Delete();

            base.Delete();

            RiseDomainEvents(new DeleteSprintEvent(Name.Name, UserId));
        }
        #endregion
    }
}
