using TodoList.Communication.Requests;
using TodoList.Communication.Responses;

namespace TodoList.Application.UseCases.Todos.Sync
{
    public interface ISyncTodosUseCase
    {
        Task<ResponseSyncTodosJson> Execute(RequestSyncTodosJson request);
    }
}
