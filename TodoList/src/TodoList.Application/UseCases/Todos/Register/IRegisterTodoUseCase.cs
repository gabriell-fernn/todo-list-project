using TodoList.Communication.Requests;
using TodoList.Communication.Responses;

namespace TodoList.Application.UseCases.Todos.Register
{
    public interface IRegisterTodoUseCase
    {
        Task<ResponseRegisterTodoJson> Execute(RequestRegisterTodoJson request);
    }
}
