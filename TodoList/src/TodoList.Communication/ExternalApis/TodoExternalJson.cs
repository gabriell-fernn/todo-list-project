namespace TodoList.Communication.ExternalApis
{
    public class TodoExternalJson
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool Completed { get; set; }
    }
}
