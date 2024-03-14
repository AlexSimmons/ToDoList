using ToDoList.Core.Repositories.Interfaces;

namespace ToDoList.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        IToDoItemRepository ToDoItemRepository { get; }
        IUserRepository UserRepository { get; }

        ValueTask DisposeAsync();
        Task SaveAsync();
    }
}