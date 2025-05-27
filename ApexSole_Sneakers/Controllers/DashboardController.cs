using ApexSole_Sneakers.Data;
using ApexSole_Sneakers.Interfaces;
using ApexSole_Sneakers.Models;
using ApexSole_Sneakers.ViewModels;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;

namespace ApexSole_Sneakers.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboard;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IPhotoService _photoService;
        

        public DashboardController(IDashboardService dashboard,IHttpContextAccessor httpContext,IPhotoService photoService)
        {
            _dashboard = dashboard;
            _httpContext = httpContext;
            _photoService = photoService;
        }
        public void MapUserEdit(AppUser appUser,EditUserViewModel editVM, ImageUploadResult imageResult)
        {
            appUser.Id = editVM.Id;
            appUser.Address = editVM.Address;
            appUser.PhoneNumber = editVM.PhoneNumber;
            appUser.ProfileImage = imageResult.Url.ToString();
        }
        public async Task<IActionResult> Index()
        {
            var adminS = await _dashboard.GetAllAdminS();
            var adminT = await _dashboard.GetAllAdminT();
            var dashboardVM = new DashboardViewModel()
            {
                Sneakers = adminS,
                Tshirts = adminT
            };
            return View(dashboardVM);
        }

        public async Task<IActionResult> EditProfile()
        {
            var current = _httpContext.HttpContext.User.GetUserId();
            var user = await _dashboard.GetUserbyId(current);
            if (user == null) { return View("Error"); };
            var editVM = new EditUserViewModel()
            {
                Id = current,
                PhoneNumber = user.PhoneNumber,
                ProfileImage = user.ProfileImage,
                Address = user.Address
            };
            return View(editVM);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditUserViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error");
                return View("EditProfile",editVM);
            }
            var user = await _dashboard.GetUserbyIdEdit(editVM.Id);
            if (user.ProfileImage==""||user.ProfileImage==null) 
            {
                var result = await _photoService.AddPhotoAsync(editVM.Image);
                MapUserEdit(user, editVM, result);
                _dashboard.Update(user);
                return RedirectToAction("Index","Home");
            }
            else
            {
                await _photoService.DeletePhotoAsync(user.ProfileImage);
            }
            var result2 = await _photoService.AddPhotoAsync(editVM.Image);
            MapUserEdit(user, editVM, result2);
            _dashboard.Update(user);
            return RedirectToAction("Index", "Home");
        }
    }
}
