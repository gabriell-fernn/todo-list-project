namespace TodoList.Exception.ExceptionBase
{
    public abstract class TodoListException : SystemException
    {
        protected TodoListException(string message) : base(message) { }

        public abstract int StatusCode { get; }
        public abstract List<string> GetErrors();
    }
}
