using Microsoft.AspNetCore.Mvc;
using ToDoList.Core.Services.Interfaces;
using ToDoList.Web._services;

namespace ToDoList.Web.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISessionService _sessionService;

        public RegisterController(IUserService userService, 
                                  ISessionService sessionService)
        {
            _userService = userService;
            _sessionService = sessionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string name, string email)
        {
            //Only chekcing its not null no actual login check.. 
            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(email))
            {
                var user = await _userService.CreateInitialUserWithToDoItems(name, email);

                if (user != null)
                {
                    _sessionService.SetRegisteredSession(user.Id);
                    return RedirectToAction("Index", "Home");

                }
            }

            // If credentials are not valid
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }
    }
}
