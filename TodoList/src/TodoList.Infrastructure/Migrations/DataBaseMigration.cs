using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Infrastructure.DataAccess;

namespace TodoList.Infrastructure.Migrations
{
    public static class DataBaseMigration
    {
        public async static Task MigrateDatabase(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<TodoListDbContext>();

            await dbContext.Database.MigrateAsync();
        }
    }
}
