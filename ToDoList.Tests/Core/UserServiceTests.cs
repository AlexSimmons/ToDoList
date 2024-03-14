using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.Models;
using ToDoList.Core.Services.Interfaces;
using ToDoList.Core.UnitOfWork;
using ToDoListServices.Core.Services.Implementations;

namespace ToDoList.Tests.Core
{
    public class UserServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IToDoItemService> _mockToDoItemService;
        private readonly UserService _sut;

        public UserServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockToDoItemService = new Mock<IToDoItemService>();
            _sut = new UserService(_mockUnitOfWork.Object, _mockToDoItemService.Object);
        }

        [Fact]
        public async Task GetUserIfRegistered_UserFound_ReturnsUser()
        {
            // Arrange
            var email = "test@example.com";
            var expectedUser = new User { Email = email };
            _mockUnitOfWork.Setup(uow => uow.UserRepository.GetByEmail(email)).ReturnsAsync(expectedUser);

            // Act
            var result = await _sut.GetUserIfRegistered(email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(email, result.Email);
        }

        [Fact]
        public async Task GetUserIfRegistered_UserNotFound_ReturnsNull()
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.UserRepository.GetByEmail(It.IsAny<string>())).ReturnsAsync((User)null);

            // Act
            var result = await _sut.GetUserIfRegistered("nonexistent@example.com");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateInitialUserWithToDoItems_UserDoesNotExist_CreatesUserWithToDoItems()
        {
            // Arrange
            var name = "New User";
            var email = "new@example.com";
            _mockUnitOfWork.Setup(uow => uow.UserRepository.GetByEmail(email)).ReturnsAsync((User)null);
            _mockToDoItemService.Setup(s => s.CreateInitialToDoItems()).Returns(new List<ToDoItem>());
            _mockUnitOfWork.Setup(uow => uow.UserRepository.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

            // Act
            var result = await _sut.CreateInitialUserWithToDoItems(name, email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(name, result.Name);
            Assert.Equal(email, result.Email);
            _mockToDoItemService.Verify(s => s.AddToDoItemsToUser(It.IsAny<User>(), It.IsAny<List<ToDoItem>>()), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveAsync(), Times.Once);
        }
    }
}
