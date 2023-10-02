using Microsoft.AspNetCore.Mvc;
using MVCCoreAPIClient.Models;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MVCCoreAPIClient.Controllers
{
    public class HomeController : Controller
    {       
       public IActionResult Index()
       {
            //show the result into View
            var lstEmps=ApiConsumer.GetEmps();
            return View(lstEmps);
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            //call api to get emp by id
            var emp=ApiConsumer.GetEmpById(id);
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
            //insert the record using WEB API
            var status = ApiConsumer.AddEmployee(emp);
            if (status)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            //delete the record usinhg WEB API
            var status=ApiConsumer.DeleteEmployee(id);
            if (status)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewData.Add("errMsg", "Record could be deleted");
                return View("MyErrorPage");
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //get the record from WEB API for edit form
            var emp = ApiConsumer.GetEmpById(id);
            if (emp != null)
            {
                return View(emp);
            }
            else
            {
                ViewData.Add("errMsg", "Record not found");
                return View("MyErrorPage");
            }
        }

        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            //save the record using WEB API
            var status = ApiConsumer.UpdateEmployee(emp);
            if (status)
            {
                return RedirectToAction("index");
            }
            else
            {
                ViewData.Add("errMsg", "could not update the record");
                return View("MyErrorPage");
            }
        }
    }
}