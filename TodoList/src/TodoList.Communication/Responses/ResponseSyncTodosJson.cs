namespace TodoList.Communication.Responses
{
    public class ResponseSyncTodosJson
    {
        public string Message { get; set; } = string.Empty;
        public int SyncedCount { get; set; }
        public int SkippedCount { get; set; }
    }
}
