using ToDoList.Core.Models;

namespace ToDoList.Core.Services.Interfaces
{
    public interface IToDoItemService
    {
        Task<bool> AddNewToDo(Guid? userGuid, ToDoItem toDoItem);
        void AddToDoItemsToUser(User user, List<ToDoItem> toDoItems);
        List<ToDoItem> CreateInitialToDoItems();
        Task<bool> DeleteToDoItem(Guid toDoGuid);
        Task<bool> ToggleToDoItemCompleted(Guid guid);
    }
}