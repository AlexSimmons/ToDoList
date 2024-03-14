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
    public  class ToDoServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;

        private IToDoItemService _sut;

        public ToDoServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _sut = new ToDoItemService(_mockUnitOfWork.Object);
        }


        [Fact]
        public void AddToDoItemsToUser_AddsItemsCorrectly()
        {
            // Arrange
            var user = new User { ToDoItems = new List<ToDoItem>() };
            var toDoItems = new List<ToDoItem>
            {
                new ToDoItem { Title = "Sample Task 1", Description = "Sample Description 1" },
                new ToDoItem { Title = "Sample Task 2", Description = "Sample Description 2" }
            };

            // Act
            _sut.AddToDoItemsToUser(user, toDoItems);

            // Assert
            Assert.Equal(2, user.ToDoItems.Count);
            Assert.Contains(user.ToDoItems, item => item.Title == "Sample Task 1" && item.Description == "Sample Description 1");
            Assert.Contains(user.ToDoItems, item => item.Title == "Sample Task 2" && item.Description == "Sample Description 2");
        }


        [Fact]
        public void CreateInitialToDoItems_ReturnsExpectedItems()
        {
            // Act
            var result = _sut.CreateInitialToDoItems();

            // Assert
            Assert.Equal(5, result.Count);
        }

        #region DeleteToDoItem()

        [Fact]
        public async Task DeleteToDoItem_WhenItemDoesNotExist_ReturnsFalse()
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.ToDoItemRepository.FindAsync(It.IsAny<Guid>())).ReturnsAsync((ToDoItem)null);

            // Act
            var result = await _sut.DeleteToDoItem(Guid.NewGuid());

            // Assert
            Assert.False(result);
            _mockUnitOfWork.Verify(uow => uow.ToDoItemRepository.Delete(It.IsAny<ToDoItem>()), Times.Never);
            _mockUnitOfWork.Verify(uow => uow.SaveAsync(), Times.Never);
        }


        [Fact]
        public async Task DeleteToDoItem_WhenItemExists_ReturnsTrue()
        {
            // Arrange
            var toDoGuid = Guid.NewGuid();
            var toDoItem = new ToDoItem { Id = toDoGuid };

            _mockUnitOfWork.Setup(uow => uow.ToDoItemRepository.FindAsync(toDoGuid)).ReturnsAsync(toDoItem);

            // Act
            var result = await _sut.DeleteToDoItem(toDoGuid);

            // Assert
            Assert.True(result);
            _mockUnitOfWork.Verify(uow => uow.ToDoItemRepository.Delete(toDoItem), Times.Once);
            _mockUnitOfWork.Verify(uow => uow.SaveAsync(), Times.Once);
        }


        [Fact]
        public async Task DeleteToDoItem_WhenExceptionIsThrown_ReturnsFalse()
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.ToDoItemRepository.FindAsync(It.IsAny<Guid>())).ThrowsAsync(new Exception());

            // Act
            var result = await _sut.DeleteToDoItem(Guid.NewGuid());

            // Assert
            Assert.False(result);
            _mockUnitOfWork.Verify(uow => uow.ToDoItemRepository.Delete(It.IsAny<ToDoItem>()), Times.Never);
            _mockUnitOfWork.Verify(uow => uow.SaveAsync(), Times.Never);
        }

        #endregion

        #region  ToggleToDoItemCompleted()

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ToggleToDoItemCompleted_ChangesIsCompletedStatus(bool initialIsCompletedStatus)
        {
            // Arrange
            var guid = Guid.NewGuid();
            var toDoItem = new ToDoItem { Id = guid, IsCompleted = initialIsCompletedStatus };

            _mockUnitOfWork.Setup(x => x.ToDoItemRepository.FindAsync(guid)).ReturnsAsync(toDoItem);

            // Act
            var result = await _sut.ToggleToDoItemCompleted(guid);

            // Assert
            Assert.True(result);
            Assert.Equal(!initialIsCompletedStatus, toDoItem.IsCompleted);
            _mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Once); 
        }

        [Fact]
        public async Task ToggleToDoItemCompleted_WhenToDoItemDoesNotExist_ReturnsFalse()
        {
            // Arrange
            _mockUnitOfWork.Setup(x => x.ToDoItemRepository.FindAsync(It.IsAny<Guid>())).ReturnsAsync((ToDoItem)null);

            // Act
            var result = await _sut.ToggleToDoItemCompleted(Guid.NewGuid());

            // Assert
            Assert.False(result);
            _mockUnitOfWork.Verify(x => x.SaveAsync(), Times.Never); // Ensure SaveAsync is not called
        }

        #endregion
    }
}
