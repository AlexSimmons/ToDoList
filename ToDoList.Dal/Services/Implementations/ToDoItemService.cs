using ToDoList.Core.Models;
using ToDoList.Core.Services.Interfaces;
using ToDoList.Core.UnitOfWork;

namespace ToDoListServices.Core.Services.Implementations
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ToDoItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<ToDoItem> CreateInitialToDoItems()
        {
            return new List<ToDoItem>
            {
                new ToDoItem { Title = "First Task", Description = "Come up with great interview questions." },
                new ToDoItem { Title = "Second Task", Description = "Ask about using Dependency Injection." },
                new ToDoItem { Title = "Third Task", Description = "Discuss database patterns." },
                new ToDoItem { Title = "Unit Tests?", Description = "Got unit tests? Yes, indeed!" },
                new ToDoItem { Title = "UI", Description = "Is the UI Bootstrap-based? Yep!" },
                new ToDoItem { Title = "The Logo Question", Description = "Let's not talk about how long the logo took in Photoshop." }
            };
        }


        public void AddToDoItemsToUser(User user, List<ToDoItem> toDoItems)
        {
            foreach (var item in toDoItems)
            {
                user.ToDoItems.Add(item);
            }
        }

        public async Task<bool> DeleteToDoItem(Guid toDoGuid)
        {
            try
            {
                var toDoItem = await _unitOfWork.ToDoItemRepository.FindAsync(toDoGuid);
                if (toDoItem == null)
                {
                    return false;
                }

                _unitOfWork.ToDoItemRepository.Delete(toDoItem);
                await _unitOfWork.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                //Should probably add logging at this point
                return false;
            }
        }

        public async Task<bool> AddNewToDo(Guid? userGuid, ToDoItem toDoItem)
        {
            if (!userGuid.HasValue)
            {
                return false;
            }

            if (toDoItem == null)
            {
                return false;
            }

            try
            {

                var user = await _unitOfWork.UserRepository.FindAsync(userGuid.Value);

                if (user == null)
                {
                    return false;
                }

                toDoItem.User = user;
                toDoItem.UserId = user.Id;
                await _unitOfWork.ToDoItemRepository.AddAsync(toDoItem);
                await _unitOfWork.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                //Should probably add logging at this point
                return false;

            }
        }

        public async Task<bool> ToggleToDoItemCompleted(Guid guid)
        {
            try
            {
                var toDoItem = await _unitOfWork.ToDoItemRepository.FindAsync(guid);
                if (toDoItem == null)
                {
                    return false;
                }

                if (toDoItem.IsCompleted)
                {
                    toDoItem.IsCompleted = false;
                }
                else
                {
                    toDoItem.IsCompleted = true;
                }

                toDoItem.ModifiedAt = DateTime.Now;
                await _unitOfWork.SaveAsync();

                return true;
            }
            catch (Exception ex)
            {
                //Should add logging
                return false;
            }
        }
    }
}
