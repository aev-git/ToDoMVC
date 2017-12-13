using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToDoListWeb.Models;

namespace ToDoListWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "ToDoLists");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "ToDo List Web App";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Anthony Villa";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
