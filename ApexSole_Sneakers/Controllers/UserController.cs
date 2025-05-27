using ApexSole_Sneakers.Data;
using ApexSole_Sneakers.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApexSole_Sneakers.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;

        }
        [HttpGet]
        public IActionResult Index()
        {
            var userList = _context.Users.ToList();
            var userRoles = _context.UserRoles.ToList();
            var roles = _context.Roles.ToList();
            foreach (var user in userList)
            {
                var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
            }
            return View(userList);
        }

        [HttpPost]
        public IActionResult LockUnlock(string id)
        {
            var userEdit = _context.Users.FirstOrDefault(u => u.Id == id);
            if (userEdit == null)
            {
                TempData["Error"] = "Користувача не знайдено";
                return RedirectToAction("Index");
            }

            if (userEdit.LockoutEnd != null && userEdit.LockoutEnd>DateTime.Now)
            {
                userEdit.LockoutEnd = DateTime.Now;
            }
            else
            {
                userEdit.LockoutEnd = DateTime.Now.AddYears(10);
            }

            _context.SaveChanges();
            TempData["Success"] = "Статус користувача змінено";
            return RedirectToAction("Index");
        }
    }
}
