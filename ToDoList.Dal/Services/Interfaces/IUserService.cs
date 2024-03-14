using ToDoList.Core.Models;

namespace ToDoList.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateInitialUserWithToDoItems(string name, string email);
        Task<User?> GetUserIfRegistered(string email);
    }
}