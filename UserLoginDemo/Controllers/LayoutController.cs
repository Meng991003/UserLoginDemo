using Microsoft.AspNetCore.Mvc;
using UserLoginDemo.Models;
using UserLoginDemo.Repository;

namespace UserLoginDemo.Controllers
{
    public class LayoutController : Controller
    {
        private readonly IUserLogin _loginUser;
        public LayoutController(IUserLogin user)
        {
            _loginUser = user;
        }
        public IActionResult Index(string username)
        {
            ViewBag.username = username;
            List<UserInfo> userInfo = _loginUser.GetUser();
            return View(userInfo);
        }

        [HttpPost]
        public IActionResult Logout(string username)
        {
            _loginUser.Logout(username);
            return RedirectToAction("Index", "Home");
        }
    }
}
