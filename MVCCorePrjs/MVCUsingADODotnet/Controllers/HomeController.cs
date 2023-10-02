using Microsoft.AspNetCore.Mvc;
using MVCUsingADODotnet.Models;
using System.Diagnostics;

namespace MVCUsingADODotnet.Controllers
{
    public class HomeController : Controller
    {
        AdoDisconnected dal;
        public HomeController()
        {
            this.dal = new AdoDisconnected();
        }
        public IActionResult Index()
        {
            var lstEmps = dal.GetEmps();
            return View(lstEmps);
        }
        public IActionResult Details(int id)
        {
            var emp=dal.GetEmployee(id);
            return View(emp);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            //insert the record using DAL layer
            dal.AddEmployee(emp);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            dal.DeleteEmployee(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //get the record and display for editing
            var record=dal.GetEmployee(id);
            return View(record);
        }

        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            dal.UpdateEmployee(emp);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult UpdateSalary(int id)
        {
            //get the record and display for editing salary
            var record = dal.GetEmployee(id);
            return View(record);
        }

        [HttpPost]
        public IActionResult UpdateSalary(Employee emp)
        {
            //dal.UpdateSalary(emp.Ecode,emp.Salary);
            return RedirectToAction("Index");
        }
    }
}