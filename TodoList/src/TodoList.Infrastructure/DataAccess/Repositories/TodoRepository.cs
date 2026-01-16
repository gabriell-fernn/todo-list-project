using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using TodoList.Domain.Repositories;

namespace TodoList.Infrastructure.DataAccess.Repositories
{
    internal class TodoRepository : ITodoRepository
    {
        private readonly TodoListDbContext _dbContext;

        public TodoRepository(TodoListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Todo todo)
        {
            await _dbContext.Todos.AddAsync(todo);
        }

        public async Task Update(Todo todo)
        {
            _dbContext.Todos.Update(todo);
        }

        public async Task<Todo?> GetById(int id)
        {
            return await _dbContext.Todos.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<Todo>> GetAll()
        {
            return await _dbContext.Todos.ToListAsync();
        }

        public async Task<List<Todo>> GetFiltered(string? title, string sort, string order, int page, int pageSize)
        {
            IQueryable<Todo> query = _dbContext.Todos;

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(t => t.Title.ToLower().Contains(title.ToLower()));

            query = ApplySorting(query, sort, order);

            int skip = (page - 1) * pageSize;
            return await query.Skip(skip).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetCountByFilter(string? title)
        {
            IQueryable<Todo> query = _dbContext.Todos;

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(t => t.Title.Contains(title));

            return await query.CountAsync();
        }

        public async Task<int> CountIncompleteByUser(int userId)
        {
            return await _dbContext.Todos
                .CountAsync(t => t.UserId == userId && !t.Completed);
        }

        public async Task<bool> ExistsById(int id)
        {
            return await _dbContext.Todos.AnyAsync(t => t.Id == id);
        }

        private IQueryable<Todo> ApplySorting(IQueryable<Todo> query, string sort, string order)
        {
            var isDescending = order.ToLower() == "desc";

            return sort.ToLower() switch
            {
                "title" => isDescending
                    ? query.OrderByDescending(t => t.Title)
                    : query.OrderBy(t => t.Title),
                "userid" => isDescending
                    ? query.OrderByDescending(t => t.UserId)
                    : query.OrderBy(t => t.UserId),
                _ => isDescending
                    ? query.OrderByDescending(t => t.Id)
                    : query.OrderBy(t => t.Id)
            };
        }
    }
}
