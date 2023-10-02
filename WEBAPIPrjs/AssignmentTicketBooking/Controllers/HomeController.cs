using AssignmentTicketBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace AssignmentTicketBooking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISeatcingSectionDAO seatcingSectionDAO;
        public HomeController(ISeatcingSectionDAO seatcingSectionDAO)
        {

            this.seatcingSectionDAO = seatcingSectionDAO;

        }
        public IActionResult Index(string? msg = "")
        {
            //get the seating section list
            var lstSeatingSection = seatcingSectionDAO.GetAllSeatingSection();
            ViewData.Add("msg", msg);
            return View(lstSeatingSection);

        }

        [HttpGet]
        public IActionResult Update()
        {
            var lstSeatingType = seatcingSectionDAO.GetAllSeatingSection()
                                                      .Select(o=>new SelectListItem
                                                      { 
                                                        Text=o.SeatingType,
                                                        Value=o.Id.ToString()
                                                      });;


            ViewData.Add("lstSeatingType", lstSeatingType);
            return View();
        }

        [HttpPost]
        public IActionResult Update(SeatingSection seatingSection)
        {
            //dal layer to update database for ticket book
            var status = seatcingSectionDAO.BookTicket(seatingSection.SeatingType, seatingSection.NoOfTickets);

            var msg = "";
            if(status==true)
            {
                msg = "Ticket Booked Successfully";
            }
            else
            {
                msg = "Requested number of tickets not available";
            }

            
            return RedirectToAction("Index",new {msg=msg});
        }
    }
}