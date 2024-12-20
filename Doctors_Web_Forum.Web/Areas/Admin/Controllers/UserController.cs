using Doctors_Web_Forum.BLL.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Doctors_Web_Forum.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) 
        {
            _userService = userService;        
        }


        public async Task<IActionResult> Index(int pg = 1, int pageSize = 5, string searchTerm = "")
        {
            var (users, pager) = await _userService.GetAllUsersAsync(pg, pageSize, searchTerm);

            // Truyền thông tin phân trang vào ViewBag
            ViewBag.Pager = pager;
            ViewBag.SearchTerm = searchTerm;

            return View(users);
        }



    }
}
