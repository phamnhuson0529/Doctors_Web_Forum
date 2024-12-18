using Doctors_Web_Forum.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Doctors_Web_Forum.Web.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager , SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User { UserName = user.UserName , Email = user.Email};
                IdentityResult result = await _userManager.CreateAsync(newUser);
                if (result.Succeeded)
                {
                    return Redirect("/admin");
                }
                foreach (IdentityError error  in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
            }
            return View();
        }
    }
}
