namespace Tasks.Application.Dto
{
    public abstract class BaseDto
    {
        public long Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
