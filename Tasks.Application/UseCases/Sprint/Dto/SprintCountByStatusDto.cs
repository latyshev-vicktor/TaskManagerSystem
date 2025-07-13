namespace Tasks.Application.UseCases.Sprint.Dto
{
    public class SprintCountByStatusDto
    {
        public int CreatedCount { get; }
        public int InProgressCount { get; }
        public int CompletedCount { get; }
        public SprintCountByStatusDto(int createdCount, int inProgressCount, int completedCount)
        {
            CreatedCount = createdCount;
            InProgressCount = inProgressCount;
            CompletedCount = completedCount;
        }
    }
}
