namespace TodoList.Communication.Requests
{
    public class RequestRegisterTodoJson
    {
        public string Title { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}
