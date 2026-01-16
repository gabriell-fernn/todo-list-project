using TodoList.Communication.Requests;

namespace TodoList.Application.UseCases.Todos.Update
{
    public interface IUpdateTodoUseCase
    {
        Task Execute(int id, RequestUpdateTodoJson request);
    }
}
