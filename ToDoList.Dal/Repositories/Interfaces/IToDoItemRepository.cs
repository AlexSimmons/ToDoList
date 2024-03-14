using ToDoList.Core.Models;

namespace ToDoList.Core.Repositories.Interfaces
{
    public interface IToDoItemRepository
    {
        Task AddAsync(ToDoItem entity);
        void Delete(ToDoItem entity);
        Task<ToDoItem?> FindAsync(Guid id);
        Task<IEnumerable<ToDoItem>> GetAllByUserIdAsync(Guid guid);
        Task<IEnumerable<ToDoItem>> GetAllCompletedByUserIdAsync(Guid guid);
        Task<IEnumerable<ToDoItem>> GetAllUnCompletedByUserIdAsync(Guid guid);
        void Update(ToDoItem entity);
    }
}