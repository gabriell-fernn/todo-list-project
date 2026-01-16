namespace TodoList.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
