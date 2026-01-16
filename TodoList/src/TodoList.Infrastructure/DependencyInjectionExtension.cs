using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Domain.Repositories;
using TodoList.Infrastructure.DataAccess;
using TodoList.Infrastructure.DataAccess.Repositories;
using TodoList.Infrastructure.Extensions;

namespace TodoList.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            AddRepositories(service);

            if (configuration.IsTestEnvironment() == false)
            {
                AddDbContext(service, configuration);
            }
        }

        private static void AddRepositories(IServiceCollection service)
        {
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<ITodoRepository, TodoRepository>();
        }

        private static void AddDbContext(IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<TodoListDbContext>(config => config.UseSqlite("Data Source=todo.db"));
        }
    }
}
