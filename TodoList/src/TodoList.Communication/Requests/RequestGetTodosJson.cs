namespace TodoList.Communication.Requests
{
    public class RequestGetTodosJson
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Title { get; set; }
        public string Sort { get; set; } = "id";
        public string Order { get; set; } = "asc";
    }
}
