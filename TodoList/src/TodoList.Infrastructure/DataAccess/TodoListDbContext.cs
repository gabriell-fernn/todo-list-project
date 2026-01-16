using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;

namespace TodoList.Infrastructure.DataAccess
{
    public class TodoListDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        public TodoListDbContext(DbContextOptions<TodoListDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Todo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(500);
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.Completed).HasDefaultValue(false);

                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.Completed);
            });
        }
    }
}
