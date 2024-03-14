using Microsoft.EntityFrameworkCore;
using ToDoList.Core.Models;

namespace ToDoList.Core
{
    public interface IToDoListDbContext : IDisposable
    {
        DbSet<ToDoItem> ToDoItem { get; set; }
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}