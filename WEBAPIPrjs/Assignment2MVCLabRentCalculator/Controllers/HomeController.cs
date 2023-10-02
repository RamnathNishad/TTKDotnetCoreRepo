using Assignment2MVCLabRentCalculator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Assignment2MVCLabRentCalculator.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate(Hall hall)
        {
            
            if(hall.StartDate>hall.EndDate)
            {
                ModelState.AddModelError("StartDate", "start date must be lesser than the end date");
            }

            if (ModelState.IsValid)
            {
                //calculate the rent based on startdate and enddate
                int rent = 0;
                int noOfDays = (hall.EndDate - hall.StartDate).Days+1;
                rent = hall.CostPerDay * noOfDays;


                ViewData.Add("totalRent", rent);
                return View("Calculate", hall);
            }
            else
            {
                return View("Index");
            }
        }
    }
}