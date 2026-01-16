using TodoList.Domain.Repositories;

namespace TodoList.Infrastructure.DataAccess
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly TodoListDbContext _dbContext;

        public UnitOfWork(TodoListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
