namespace TaskManagerSystem.Common.Dtos
{
    public abstract class BaseFilter
    {
        public int? Id { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public string? SortBy { get; set; }
        public bool SortDesc { get; set; }
    }
}
