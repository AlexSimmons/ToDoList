using ToDoList.Core.Models;
using ToDoList.Core.Services.Interfaces;
using ToDoList.Core.UnitOfWork;

namespace ToDoListServices.Core.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IToDoItemService _toDoItemService;


        public UserService(IUnitOfWork unitOfWork, IToDoItemService toDoItemService)
        {
            _unitOfWork = unitOfWork;
            _toDoItemService = toDoItemService;
        }

        public async Task<User?> GetUserIfRegistered(string email)
        {
            return await _unitOfWork.UserRepository.GetByEmail(email);
        }

        public async Task<User> CreateInitialUserWithToDoItems(string name, string email)
        {
            var registerUser = await GetUserIfRegistered(email);
            if (registerUser != null)
            {
                return registerUser;
            }

            var user = new User
            {
                Name = name,
                Email = email
            };

            var toDoItems = _toDoItemService.CreateInitialToDoItems();

            if (toDoItems != null)
            {
                _toDoItemService.AddToDoItemsToUser(user, toDoItems);
            }

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();

            return user;
        }
    }
}
