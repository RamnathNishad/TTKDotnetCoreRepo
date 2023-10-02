using Microsoft.AspNetCore.Mvc;
using MyFirstMVCoreApp.Models;

namespace MyFirstMVCoreApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            ViewData.Add("ReturnUrl", ReturnUrl);
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserDetail userDetail,string returnUrl)
        {
            if(userDetail.Uname!="admin")// && userDetail.Password=="admin123")
            {
                ModelState.AddModelError("Uname", "username does not exist");
                //redirect to Register
                return Redirect("/Account/Register");
            }
            else if(userDetail.Password!="admin123")
            {
                ModelState.AddModelError("Password", "invalid password!!!");
            }

            if(ModelState.IsValid)
            {
                //redirect to the requested url
                //store the user is valid in session
                HttpContext.Session.SetString("IsAuthenticated", "true");
                return Redirect(returnUrl);
            }
            else
            {
                return View();
            }
            
        }
    }
}
