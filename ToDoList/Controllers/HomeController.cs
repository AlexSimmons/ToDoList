using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.Models;
using ToDoList.Web._services;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISessionService _sessionService;


        public HomeController(ILogger<HomeController> logger, ISessionService sessionService)
        {
            _logger = logger;

            _sessionService = sessionService;
        }

        public IActionResult Index()
        {
            if(!_sessionService.IsRegistered())
            {
                return RedirectToAction("Index", "Register");
            }

            return View();
        }

    }
}
