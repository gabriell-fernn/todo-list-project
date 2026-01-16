using System.Net;

namespace TodoList.Exception.ExceptionBase
{
    public class NotFoundException : TodoListException
    {
        public NotFoundException(string message) : base(message)
        {
        }
        public override int StatusCode => (int)HttpStatusCode.NotFound;

        public override List<string> GetErrors()
        {
            return [Message];
        }
    }
}
