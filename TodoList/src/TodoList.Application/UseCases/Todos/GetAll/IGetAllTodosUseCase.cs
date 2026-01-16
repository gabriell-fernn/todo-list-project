using TodoList.Communication.Requests;
using TodoList.Communication.Responses;

namespace TodoList.Application.UseCases.Todos.GetAll
{
    public interface IGetAllTodosUseCase
    {
        Task<ResponseGetTodosJson> Execute(RequestGetTodosJson request);
    }
}
