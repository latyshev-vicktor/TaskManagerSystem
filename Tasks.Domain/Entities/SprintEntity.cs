using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Domain.DomainEvents;
using Tasks.Domain.Errors;
using Tasks.Domain.SeedWork;
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
        public DateTimeOffset StartDate 
        { 
            get
            {
                if (!SprintWeeks.Any())
                {
                    return DateTimeOffset.Now;
                }

                return SprintWeeks.SingleOrDefault(x => x.WeekNumber == 1)!.StartDate;
            }
        }
   
        public DateTimeOffset EndDate
        {
            get
            {
                if(!SprintWeeks.Any())
                {
                    return DateTimeOffset.Now;
                }

                var countWeeks = SprintWeeks.Count;

                return SprintWeeks.SingleOrDefault(x => x.WeekNumber == countWeeks)!.EndDate;
            }
        }
        public SprintStatus Status { get; private set; }

        public List<SprintFieldActivityEntity> SprintFieldActivities { get; private set; } = [];

        /// <summary>
        /// Связь с неделями, на которые разбивается спринт
        /// </summary>
        private List<SprintWeekEntity> _sprintWeeks = [];
        public IReadOnlyList<SprintWeekEntity> SprintWeeks => _sprintWeeks;

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
            List<FieldActivityEntity> fieldActivities)
        {
            UserId = userId;
            Name = name;
            Description = description;
            Status = SprintStatus.Created;
            AddFieldActivities(fieldActivities);
        }
        #endregion

        #region DDD-методы
        public static IExecutionResult<SprintEntity> Create(
            long userId,
            string name,
            string description,
            List<FieldActivityEntity> fieldActivities)
        {
            if (userId == default)
                return ExecutionResult.Failure<SprintEntity>(SprintError.UserNotEmpty());

            var nameResult = SprintName.Create(name);
            if (nameResult.IsFailure)
                return ExecutionResult.Failure<SprintEntity>(nameResult.Error);

            var descriptionResult = SprintDescription.Create(description);
            if (descriptionResult.IsFailure)
                return ExecutionResult.Failure<SprintEntity>(descriptionResult.Error);

            if (fieldActivities.Count == 0)
                return ExecutionResult.Failure<SprintEntity>(SprintError.NotFoundFieldActivities());

            return ExecutionResult.Success(new SprintEntity(userId, nameResult.Value, descriptionResult.Value, fieldActivities));
        }

        public IExecutionResult ChangeStatus(string status)
        {
            var statusResult = SprintStatus.Create(status);
            if (statusResult.IsFailure)
                return ExecutionResult.Failure(statusResult.Error);

            Status = statusResult.Value;

            return ExecutionResult.Success();
        }

        public IExecutionResult StartSprint()
        {
            if(Status == SprintStatus.InProgress)
                return ExecutionResult.Failure(SprintError.SprintAlreadyStarted());
            
            if(Status == SprintStatus.Completed)
                return ExecutionResult.Failure(SprintError.SprintAlreadyCompleted());

            Status = SprintStatus.InProgress;
            
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

        public void AddFieldActivities(List<FieldActivityEntity> fieldActivities)
        {
            var fieldActivitiesForAdd = fieldActivities.Where(x => !SprintFieldActivities.Select(x => x.FieldActivityId).Contains(x.Id));
            foreach (var fieldActivity in fieldActivitiesForAdd)
            {
                SprintFieldActivities.Add(new SprintFieldActivityEntity
                {
                    FieldActivity = fieldActivity,
                    Sprint = this
                });
            }
        }

        public void AddWeek(SprintWeekEntity week)
        {
            if(_sprintWeeks.Any(x => x.Id != week.Id))
            {
                _sprintWeeks.Add(week);
            }
        }

        public override void Delete()
        {
            base.Delete();

            RiseDomainEvents(new DeleteSprintEvent(Name.Name, UserId));
        }
        #endregion
    }
}
