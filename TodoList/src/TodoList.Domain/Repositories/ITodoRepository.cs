using TodoList.Domain.Entities;

namespace TodoList.Domain.Repositories
{
    public interface ITodoRepository
    {
        Task Add(Todo todo);
        Task Update(Todo todo);
        Task<Todo?> GetById(int id);
        Task<List<Todo>> GetAll();
        Task<List<Todo>> GetFiltered(string? title, string sort, string order, int page, int pageSize);
        Task<int> GetCountByFilter(string? title);
        Task<int> CountIncompleteByUser(int userId);
        Task<bool> ExistsById(int id);
    }
}
