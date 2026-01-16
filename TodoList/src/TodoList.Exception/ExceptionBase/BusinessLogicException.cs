using System.Net;

namespace TodoList.Exception.ExceptionBase
{
    public class BusinessLogicException : TodoListException
    {
        public BusinessLogicException(string message) : base(message)
        {
        }
        public override int StatusCode => (int)HttpStatusCode.BadRequest;

        public override List<string> GetErrors()
        {
            return [Message];
        }
    }
}
