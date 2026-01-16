namespace TodoList.Communication.Responses
{
    public class ResponseGetTodosJson
    {
        public List<ResponseTodoJson> Data { get; set; } = new();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (TotalCount + PageSize - 1) / PageSize;
    }
}
