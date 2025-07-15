namespace TaskManagerSystem.Common.Dtos
{
    public class SearchData<T>(IReadOnlyList<T> data, int count)
        where T : class
    {
        public IReadOnlyList<T> Data { get; set; } = data;
        public int Count { get; set; } = count;
    }
}
