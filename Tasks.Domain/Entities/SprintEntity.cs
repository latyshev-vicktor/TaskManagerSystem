using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
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
            DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            UserId = userId;
            Name = name;
            Description = description;
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

            return ExecutionResult.Success(new SprintEntity(userId, nameResult.Value, descriptionResult.Value, startDate, endDate));
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
        #endregion
    }
}
