using TodoList.Communication.ExternalApis;

namespace TodoList.Application.Interfaces
{
    public interface IExternalTodoService
    {
        Task<List<TodoExternalJson>> FetchTodosFromExternalApi();
    }
}
