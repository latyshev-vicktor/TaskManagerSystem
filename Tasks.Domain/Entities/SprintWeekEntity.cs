using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Domain.Errors;
using Tasks.Domain.SeedWork;

namespace Tasks.Domain.Entities
{
    public class SprintWeekEntity : BaseEntity
    {
        public long SprintId { get; private set; }
        public SprintEntity? Sprint { get; private set; }
        public int WeekNumber { get; private set; }
        public DateTimeOffset StartDate { get; private set; }
        public DateTimeOffset EndDate { get; private set; }

        private List<TaskEntity> _tasks = [];
        public IReadOnlyList<TaskEntity> Tasks => _tasks;

        #region Конструкторы
        protected SprintWeekEntity()
        {
        }

        protected SprintWeekEntity(
            SprintEntity sprint,
            int weekNumber,
            DateTime startDate,
            DateTime endDate)
        {
            Sprint = sprint;
            WeekNumber = weekNumber;
            StartDate = startDate;
            EndDate = endDate;
        }
        #endregion

        #region DDD методы

        public static IExecutionResult<SprintWeekEntity> Create(
            SprintEntity sprint,
            int weekNumber,
            DateTime startDate,
            DateTime endDate
            )
        {
            if (sprint == null)
                return ExecutionResult.Failure<SprintWeekEntity>(SprintWeekError.SprintIsNull());

            if (weekNumber <= 0)
                return ExecutionResult.Failure<SprintWeekEntity>(SprintWeekError.WeekNumberNotBeZero());

            return ExecutionResult.Success(new SprintWeekEntity(sprint, weekNumber, startDate, endDate));
        }
        #endregion
    }
}
