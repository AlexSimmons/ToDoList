using ToDoList.Core.Models;

namespace ToDoList.Core.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User entity);
        void Delete(User entity);
        Task<User?> FindAsync(Guid id);
        Task<User?> GetByEmail(string email);
        void Update(User entity);
    }
}