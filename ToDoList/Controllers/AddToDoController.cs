using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using ToDoList.Core.Models;
using ToDoList.Core.Services.Interfaces;
using ToDoList.Core.UnitOfWork;
using ToDoList.Web._services;

namespace ToDoList.Web.Controllers
{
    public class AddToDoController : Controller
    {
        private readonly ISessionService _sessionService;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IToDoItemService _toDoItemService;

        public AddToDoController(ISessionService sessionService,
                                 IUnitOfWork unitOfWork,
                                 IToDoItemService toDoItemService)
        {
            _sessionService = sessionService;
            _unitOfWork = unitOfWork;
            _toDoItemService = toDoItemService;
        }

        public async Task<IActionResult> Index()
        {
            return View(new ToDoItem());
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(ToDoItem newToDo)
        {

            ModelState.Remove("User");//The user is null as its a partial view post

            if (ModelState.IsValid)
            {
                var guid = _sessionService.GetUserId();
                if (await _toDoItemService.AddNewToDo(guid, newToDo))
                {
                    return Json(new { success = true, message = "Item added successfully." });
                }

                return Json(new { success = false, message = "Item not added." });
            }

            return View("Index", new ToDoItem());
        }
    }
}
