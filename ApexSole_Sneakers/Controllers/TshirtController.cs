using ApexSole_Sneakers.Data;
using ApexSole_Sneakers.Interfaces;
using ApexSole_Sneakers.Models;
using ApexSole_Sneakers.Repository;
using ApexSole_Sneakers.Services;
using ApexSole_Sneakers.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApexSole_Sneakers.Controllers
{
    public class TshirtController : Controller
    {
        private readonly ITshirtRepository _tshirtRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContext;

        public TshirtController(ITshirtRepository tshirtRepository,IPhotoService photoService, IHttpContextAccessor httpContext)
        {
            _tshirtRepository = tshirtRepository;
            _photoService = photoService;
            _httpContext = httpContext;
        }
        public async Task<IActionResult> Index(string searchName)
        {
            var tshirt = await _tshirtRepository.GetAll();
            if (!String.IsNullOrEmpty(searchName))
            {
                tshirt = tshirt.Where(s => s.TshirtName.Contains(searchName) || s.TshirtBrand.Contains(searchName)).ToList();
            }
            return View(tshirt);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Tshirt tshirt = await _tshirtRepository.GetByIdAsync(id);
            return View(tshirt);
        }
        public IActionResult Create()
        {
            var admin = _httpContext.HttpContext?.User.GetUserId();
            var createVM = new CreateTshirtViewModel { AppUserId = admin };
            return View(createVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTshirtViewModel tshirtVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(tshirtVM.Image);
                var tshirt = new Tshirt
                {
                    TshirtName = tshirtVM.TshirtName,
                    TshirtDescription = tshirtVM.TshirtDescription,
                    TshirtPrice = tshirtVM.TshirtPrice,
                    TshirtBrand = tshirtVM.TshirtBrand,
                    AppUserId = tshirtVM.AppUserId,
                    TshirtColor = tshirtVM.TshirtColor,
                    TshirtSize = tshirtVM.TshirtSize,
                    TshirtType = tshirtVM.TshirtType,
                    Gender = tshirtVM.Gender,
                    Image = result.Url.ToString()
                };
                _tshirtRepository.Add(tshirt);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Upload failed");
            }
            return View(tshirtVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var tshirt = await _tshirtRepository.GetByIdAsync(id);
            if (tshirt == null) return View("Error");
            var tshirtVM = new EditTshirtViewModel
            {
                TshirtName = tshirt.TshirtName,
                TshirtDescription = tshirt.TshirtDescription,
                TshirtPrice = tshirt.TshirtPrice,
                TshirtBrand = tshirt.TshirtBrand,
                TshirtColor = tshirt.TshirtColor,
                TshirtSize = tshirt.TshirtSize,
                TshirtType = tshirt.TshirtType,
                Gender = tshirt.Gender,
                URL = tshirt.Image
            };
            return View(tshirtVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditTshirtViewModel tshirtVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error");
                return View("Error");
            }
            var tshirt = await _tshirtRepository.GetByIdEditAsync(id);
            if (tshirt != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(tshirt.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error photo");
                    return View(tshirtVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(tshirtVM.Image);
                var tshirtNew = new Tshirt
                {
                    Id = id,
                    TshirtName = tshirtVM.TshirtName,
                    TshirtDescription = tshirtVM.TshirtDescription,
                    TshirtPrice = tshirtVM.TshirtPrice,
                    TshirtBrand = tshirtVM.TshirtBrand,
                    TshirtColor = tshirtVM.TshirtColor,
                    TshirtSize = tshirtVM.TshirtSize,
                    TshirtType = tshirtVM.TshirtType,
                    Gender = tshirtVM.Gender,
                    Image = photoResult.Url.ToString(),
                };
                _tshirtRepository.Update(tshirtNew);
                return RedirectToAction("Index");
            }
            else
            {
                return View(tshirtVM);
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            var tshirt = await _tshirtRepository.GetByIdAsync(id);
            if (tshirt == null) { return View("Error"); }
            return View(tshirt);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteTshirt(int id)
        {
            var tshirt = await _tshirtRepository.GetByIdAsync(id);
            if (tshirt == null) { return View("Error"); }
            _tshirtRepository.Delete(tshirt);
            return RedirectToAction("Index");
        }









    }
}
