using Microsoft.AspNetCore.Mvc;
using MVCCoreEFCodeFirstDemo.Models;
using System.Diagnostics;

namespace MVCCoreEFCodeFirstDemo.Controllers
{
    public class HomeController : Controller
    {
        ProductDbContext dbCtx;
        public HomeController(ProductDbContext dbCtx)
        {
            this.dbCtx = dbCtx; 
        }
        public IActionResult Index()
        {
            var lstProducts = dbCtx.Products.ToList();
            
            return View(lstProducts);
        }
    }
}