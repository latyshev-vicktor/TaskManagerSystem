namespace Tasks.Application.UseCases.Sprint.Dto
{
    public class SprintCountByStatusDto
    {
        public int Created { get; set; }
        public int InProgress { get; set; }
        public int Completed { get; set; }

        public SprintCountByStatusDto(int created, int inProgress, int completed)
        {
            Created = created;
            InProgress = inProgress;
            Completed = completed;
        }
    }
}
