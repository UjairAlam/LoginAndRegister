using LoginAndRegisterEFCORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace LoginAndRegisterEFCORE.Controllers
{
    public class LoginController : Controller
    {
        private readonly TestContext _testContext;
        public LoginController(TestContext testContext)
        {
            _testContext= testContext;
        }
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }
        public IActionResult Register()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(TblUser user)
        {
            if (ModelState.IsValid) { 
                await _testContext.TblUsers.AddAsync(user);
                await _testContext.SaveChangesAsync();
                TempData["Success"] = "Registered Successfully..";
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(TblUser user)
        {
            
               var MyUser=_testContext.TblUsers.Where(x=>x.Email==user.Email && x.Password==user.Password).FirstOrDefault();
            if (MyUser != null) {
                HttpContext.Session.SetString("Username", MyUser.Email);
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.Message = "Login Failed..";
            }
            
            return View();
        }
        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetString("Username") !=null)
            {
                ViewBag.Username = HttpContext.Session.GetString("Username");
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                HttpContext.Session.Remove("Username");
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}
