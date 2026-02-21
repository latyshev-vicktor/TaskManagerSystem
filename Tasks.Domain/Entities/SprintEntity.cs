using Azure.Core;
using MediatR;
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
        private const int MAX_DAYS_SHORT_SPRINT = 14;

        /// <summary>
        /// Id пользователя из сервиса авторизации
        /// </summary>
        public Guid UserId { get; private set; }
        public SprintName Name { get; private set; }
        public SprintDescription Description { get; private set; }
        public DateTimeOffset StartDate { get; private set; }
   
        public DateTimeOffset EndDate { get; private set; }
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
            Guid userId,
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
            Guid userId,
            string name,
            string description,
            List<FieldActivityEntity> fieldActivities,
            int weekCount)
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

            var newSprint = new SprintEntity(userId, nameResult.Value, descriptionResult.Value, fieldActivities);

            var startDate = DateTimeOffset.UtcNow.Date;
            var dayWeekCount = 7;

            for (int weekIndex = 0; weekIndex < weekCount; weekIndex++)
            {
                var weekStart = startDate.AddDays(weekIndex * dayWeekCount);
                var weekEnd = weekStart.AddDays(dayWeekCount - 1);

                var weekResult = SprintWeekEntity.Create(
                    newSprint,
                    weekIndex + 1,
                    weekStart,
                    weekEnd);

                if (weekResult.IsFailure)
                    return ExecutionResult.Failure<SprintEntity>(weekResult.Error);

                newSprint.AddWeek(weekResult.Value);
            }

            newSprint.SetStartDate(startDate);
            newSprint.SetEndDate(newSprint.SprintWeeks[newSprint.SprintWeeks.Count - 1].EndDate);

            return ExecutionResult.Success(newSprint);
        }

        public IExecutionResult StartSprint()
        {
            if(Status == SprintStatus.InProgress)
                return ExecutionResult.Failure(SprintError.SprintAlreadyStarted());
            
            if(Status == SprintStatus.Completed)
                return ExecutionResult.Failure(SprintError.SprintAlreadyCompleted());

            Status = SprintStatus.InProgress;

            RiseDomainEvents(new SprintChangeStatusEvent(UserId, Name.Name, Status));
            return ExecutionResult.Success();
        }

        public IExecutionResult SetName(string name)
        {
            var nameResult = SprintName.Create(name);
            if (nameResult.IsFailure)
                return ExecutionResult.Failure(nameResult.Error);

            Name = nameResult.Value;
            RiseDomainEvents(new SprintChangeNameEvent(Name.Name, Id));

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

        public void SetStartDate(DateTimeOffset startDate)
        {
            StartDate = startDate;
        }

        public void SetEndDate(DateTimeOffset endDate)
        {
            EndDate = endDate;
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

        public void RemoveFieldActivities(List<FieldActivityEntity> fieldActivities)
        {
            var fieldActivitiesForRemove = SprintFieldActivities.Where(x => fieldActivities.Any(f => f.Id == x.FieldActivityId)).ToArray();
            foreach(var fieldActivity in fieldActivitiesForRemove)
            {
                SprintFieldActivities.Remove(fieldActivity);
            }
        }

        public void AddWeek(SprintWeekEntity week)
        {
            if(_sprintWeeks.Count == 0)
            {
                _sprintWeeks.Add(week);
                return;
            }

            if(_sprintWeeks.Any(x => x.Id != week.Id) || week.Id == default)
            {
                _sprintWeeks.Add(week);
            }
        }

        public void RecalculationStartAndEndDateWeek()
        {
            var startDate = DateTimeOffset.UtcNow.Date;
            var dayWeekCount = 7;

            var weekIndex = 0;
            foreach(var week in SprintWeeks.OrderBy(x => x.WeekNumber))
            {
                var weekStart = startDate.AddDays(weekIndex * dayWeekCount);
                var weekEnd = weekStart.AddDays(dayWeekCount - 1);

                week.SetStartDate(weekStart);
                week.SetEndDate(weekEnd);

                weekIndex++;
            }

            SetStartDate(startDate);
            SetEndDate(SprintWeeks.Last().EndDate);
        }

        public override void Delete()
        {
            base.Delete();

            RiseDomainEvents(new DeleteSprintEvent(Name.Name, UserId));
        }

        public bool IsShortSprint()
        {
            return (EndDate - StartDate).Days <= MAX_DAYS_SHORT_SPRINT;
        }
        #endregion
    }
}
