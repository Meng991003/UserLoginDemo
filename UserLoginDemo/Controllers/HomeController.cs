using Microsoft.AspNetCore.Mvc;
using UserLoginDemo.Models;
using UserLoginDemo.Repository;
using System.Web;
using Newtonsoft.Json.Linq;

namespace UserLoginDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserLogin _loginUser;

        public HomeController(IUserLogin user)
        {
            _loginUser = user;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string passcode)
        {
            var loginUser = new UserLoginInfo( username, passcode);
            var issuccess = _loginUser.AuthenticateUser(loginUser);

            //return View();


            if (issuccess.Result == "")
            {
                ViewBag.msg = string.Format("Successfully logged-in", username);
                return RedirectToAction("Index", "Layout", new { username = username });
            }
            else
            {
                ViewBag.msg = string.Format("Authentication Failed", username);
                return View();
            }
        }
    }
}