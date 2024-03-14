using Microsoft.AspNetCore.Mvc;
using ToDoList.Core.Models;
using ToDoList.Core.Services.Interfaces;
using ToDoList.Core.UnitOfWork;
using ToDoList.Models;
using ToDoList.Web._services;

namespace ToDoList.Web.Controllers
{
    public class ToDoItemsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IToDoItemService _toDoItemService;

        private readonly ISessionService _sessionService;

        public ToDoItemsController(ISessionService sessionService,
                                   IUnitOfWork unitOfWork,
                                   IToDoItemService toDoItemService)
        {
            _sessionService = sessionService;

            _unitOfWork = unitOfWork;
            _toDoItemService = toDoItemService;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await _toDoItemService.DeleteToDoItem(id))
            {
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Failed to delete the item." });
        }


        [HttpPut]
        public async Task<IActionResult> ToggleComplete(Guid id)
        {
            if (await _toDoItemService.ToggleToDoItemCompleted(id))
            {
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Failed to change the item state." });
            
        }



        /// <summary>
        /// Get a list of uncompleted items for a user
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ToDoItemsUnComplete()
        {
            var guid = _sessionService.GetUserId();
            if (guid.HasValue)
            {
                var items = await _unitOfWork.ToDoItemRepository.GetAllUnCompletedByUserIdAsync(guid.Value);
                return PartialView("_ToDoList", items);
            }
            else
            {
                return PartialView("_ToDoList", new List<ToDoItem>());
            }
        }

        public async Task<IActionResult> ToDoItemsComplete()
        {
            var guid = _sessionService.GetUserId();
            if (guid.HasValue)
            {
                var items = await _unitOfWork.ToDoItemRepository.GetAllCompletedByUserIdAsync(guid.Value);
                return PartialView("_ToDoList", items);
            }
            else
            {
                return PartialView("_ToDoList", new List<ToDoItem>());
            }
        }
    }
}
