using TodoList.Communication.Responses;

namespace TodoList.Application.UseCases.Todos.GetById
{
    public interface IGetTodoByIdUseCase
    {
        Task<ResponseTodoJson> Execute(int id);
    }
}
