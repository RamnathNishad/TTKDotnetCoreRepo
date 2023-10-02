using Assignment3MVCLabHallBudget.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Assignment3MVCLabHallBudget.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(int hallBudget)
        {
            HallDAO hallDAO = new HallDAO();
            var lstHalls = hallDAO.GetHall(hallBudget);

            return View(lstHalls);  
        }
    }
}