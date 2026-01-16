using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TodoList.Communication.Responses;
using TodoList.Exception.ExceptionBase;

namespace TodoList.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is TodoListException)
            {
                HandleProjectException(context);
            }
            else
            {
                ThrowUnknownError(context);
            }
        }

        private void HandleProjectException(ExceptionContext context)
        {
            var todoListException = (TodoListException)context.Exception;
            var errorResponse = new ResponseErrorJson(todoListException.GetErrors());

            context.HttpContext.Response.StatusCode = todoListException.StatusCode;
            context.Result = new ObjectResult(errorResponse);
        }

        private void ThrowUnknownError(ExceptionContext context)
        {
            var errorResponse = new ResponseErrorJson("UNKNOWN ERROR");

            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorResponse);
        }
    }
}
