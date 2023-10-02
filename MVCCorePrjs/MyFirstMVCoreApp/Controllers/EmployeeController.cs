using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyFirstMVCoreApp.Models;

namespace MyFirstMVCoreApp.Controllers
{
    //[Route("TTK/MyApp")]
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmpDataAccess dal;
        public EmployeeController(IEmpDataAccess dal)
        {
            this.dal = dal;
        }


        //[Route("HomePage")]
        //[Route("Account/FirstPage")]
        //[Authorize]
        public IActionResult Index()
        {
            //TempData.Add("dataMsg", "Welcome to Bangalore");

            ViewData.Add("msg", "Welcome to India");

            var customer = new Customer
            {
                Id=1001,
                Name="John",
                City="Delhi"
            };

            ViewData.Add("custObj", customer);

            ViewBag.Title = "www.ttk-prestige.com";


            return View();
        }

        public IActionResult Home()
        {
            var emp = new Employee
            {
                Ecode=101,
                Ename="Ramnath",
                Salary=1111,
                Deptid=201
            };

            return View(emp);
        }
        public IActionResult Second()
        {
            return View();
        }

        public IActionResult Third()
        {
            return View();
        }

        [Route("DisplayEmps")]
        public IActionResult DisplayEmps()
        {
            var lstEmps = dal.GetAllEmps();
            return View(lstEmps);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            //server-side validations
            //check whether ecode already existing or not
            var record = dal.GetEmpById(emp.Ecode);
            if(record != null)
            {
                ModelState.AddModelError("Ecode", "employee already present,enter different");
            }


            if (ModelState.IsValid)
            {
                //DAL to insert record in Database
                dal.AddEmployee(emp);
                //redirect to Display all action
                return RedirectToAction("DisplayEmps");
            }
            else
            {
                return View();
            }

            //ViewData.Add("msg", "Record inserted for ecode:" + emp.Ecode);
            //return View();
        }

        public IActionResult Delete(int id)
        {
            dal.DeleteEmployee(id);
            return RedirectToAction("DisplayEmps");
        }

        [Route("GetDetails/{id:int}")]
        public IActionResult Details(int id)
        {
            //get the record from DAL layer
            var emp=dal.GetEmpById(id);
            return View(emp);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //find the employeed record from DAL layer
            var emp = dal.GetEmpById(id);
            return View(emp);
        }

        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            //update the record using DAL layer
            dal.UpdateEmployee(emp);
            //redirect to Display All action
            return RedirectToAction("DisplayEmps");
        }


        [HttpGet]
        public IActionResult STHtmlHelperDemo()
        {
            var cities = new List<SelectListItem>
            {
                new SelectListItem{Text="Bangalore",Value="BLR"},
                new SelectListItem{Text="Mysore",Value="MYS"},
                new SelectListItem{Text="Chennai",Value="CHN"},
                new SelectListItem{Text="Hyderabad",Value="HYD"},
                new SelectListItem{Text="Delhi",Value="DLI"}
            };
            var hobbiesForSelection = new Dictionary<string, string>
            {
                {"Singing","singing" },
                {"Dancing","dancing" },
                {"Painting","painitng" }
            };

            var register = new Register
            {
                cities=cities,
                hobbiesForSelection=hobbiesForSelection
            };


            return View(register);
        }


        [HttpGet]
        public IActionResult HtmlHelperDemo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitData(Register register)
        {
            var selectedHobbies = register.hobbies.Where(o => o != "false")
                                                  .ToList();
            register.hobbies.Clear();
            register.hobbies = selectedHobbies;

            return View(register);
        }
    }
}
