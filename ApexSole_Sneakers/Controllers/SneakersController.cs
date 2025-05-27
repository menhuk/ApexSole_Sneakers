using System.Security.Claims;
using ApexSole_Sneakers.Data;
using ApexSole_Sneakers.Interfaces;
using ApexSole_Sneakers.Models;
using ApexSole_Sneakers.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApexSole_Sneakers.Controllers
{
    public class SneakersController : Controller
    {
        private readonly ISneakersRepository _sneakersRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public SneakersController(ISneakersRepository sneakersRepository,IPhotoService photoService, IHttpContextAccessor httpContext, IShoppingCartRepository shoppingCartRepository)
        {
            _sneakersRepository = sneakersRepository;
            _photoService = photoService;
            _httpContext = httpContext;
            _shoppingCartRepository = shoppingCartRepository;
        }
        public async Task<IActionResult> Index(string searchName)
        {
            var sneakers = await _sneakersRepository.GetAll();
            if (!String.IsNullOrEmpty(searchName))
            {
                sneakers = sneakers.Where(s=>s.SneakersName.Contains(searchName)||s.SneakersBrand.Contains(searchName)).ToList();
            }
            return View(sneakers);
        }
        public async Task<IActionResult> Detail(int id) 
        {
            ShoppingCart cart = new()
            {
                Count = 1,
                Sneakers = await _sneakersRepository.GetByIdAsync(id),
                ProductId = id
            };
            return View(cart);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Detail(ShoppingCart? shoppingCart, int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var newCart = new ShoppingCart
            {
                ProductId = shoppingCart.ProductId,
                Count = shoppingCart.Count,
                AppUserId = userId
            };
            var existingCart = await _shoppingCartRepository.GetByIdEditAsync(userId, shoppingCart.ProductId);

            if (existingCart != null)
            {
                existingCart.Count += newCart.Count;
                _shoppingCartRepository.Update(existingCart);
            }
            else
            {
                _shoppingCartRepository.Add(newCart);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            var admin = _httpContext.HttpContext?.User.GetUserId();
            var createVM = new CreateSneakersViewModel {AppUserId = admin};
            return View(createVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSneakersViewModel sneakersVM)
        {
            if (ModelState.IsValid) 
            {
                var result = await _photoService.AddPhotoAsync(sneakersVM.Image);
                var sneakers = new Sneakers
                {
                    SneakersName = sneakersVM.SneakersName,
                    SneakersDescription = sneakersVM.SneakersDescription,
                    SneakersPrice = sneakersVM.SneakersPrice,
                    SneakersBrand = sneakersVM.SneakersBrand,
                    AppUserId = sneakersVM.AppUserId,
                    SneakersColor = sneakersVM.SneakersColor,
                    SneakersSize = sneakersVM.SneakersSize,
                    SneakersType = sneakersVM.SneakersType,
                    Gender = sneakersVM.Gender,
                    Image = result.Url.ToString()
                };
                _sneakersRepository.Add(sneakers);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Upload failed");
            }
            return View(sneakersVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var sneakers = await _sneakersRepository.GetByIdAsync(id);
            if (sneakers == null) return View("Error");
            var sneakersVM = new EditSneakersViewModel
            {
                SneakersName = sneakers.SneakersName,
                SneakersDescription = sneakers.SneakersDescription,
                SneakersPrice = sneakers.SneakersPrice,
                SneakersBrand = sneakers.SneakersBrand,
                SneakersColor = sneakers.SneakersColor,
                SneakersSize = sneakers.SneakersSize,
                SneakersType = sneakers.SneakersType,
                Gender = sneakers.Gender,
                URL = sneakers.Image
            };
            return View(sneakersVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,EditSneakersViewModel sneakersVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error"); 
                return View("Error");
            }
            var sneakers = await _sneakersRepository.GetByIdEditAsync(id);
            if (sneakers !=null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(sneakers.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error photo");
                    return View(sneakersVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(sneakersVM.Image);
                var sneakersNew = new Sneakers
                {
                    Id = id,
                    SneakersName = sneakersVM.SneakersName,
                    SneakersDescription = sneakersVM.SneakersDescription,
                    SneakersPrice = sneakersVM.SneakersPrice,
                    SneakersBrand = sneakersVM.SneakersBrand,
                    SneakersColor = sneakersVM.SneakersColor,
                    SneakersSize = sneakersVM.SneakersSize,
                    SneakersType = sneakersVM.SneakersType,
                    Gender = sneakersVM.Gender,
                    Image = photoResult.Url.ToString(),
                };
                _sneakersRepository.Update(sneakersNew);
                return RedirectToAction("Index");
            }
            else
            {
                return View(sneakersVM);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var sneakers =await _sneakersRepository.GetByIdAsync(id);
            if (sneakers == null) {return View("Error");}
            return  View(sneakers);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteSneakers(int id)
        {
            var sneakers = await _sneakersRepository.GetByIdAsync(id);
            if (sneakers == null) { return View("Error"); }
            _sneakersRepository.Delete(sneakers);
            return RedirectToAction("Index");
        }


    }
}
