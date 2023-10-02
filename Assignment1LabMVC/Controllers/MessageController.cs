using Microsoft.AspNetCore.Mvc;

namespace Assignment1LabMVC.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DisplayName(string name)
        {
            ViewData.Add("name", name);
            return View();
        }
    }
}
