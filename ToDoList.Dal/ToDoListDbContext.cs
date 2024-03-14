using Microsoft.EntityFrameworkCore;
using ToDoList.Core.Models;

namespace ToDoList.Core
{
    public class ToDoListDbContext : DbContext, IToDoListDbContext
    {
        public DbSet<ToDoItem> ToDoItem { get; set; } // Assuming TaskItem is your task entity
        public DbSet<User> Users { get; set; } // Adding the User entity to the DbContext

        public ToDoListDbContext(DbContextOptions<ToDoListDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=sql-lite-todolist.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
