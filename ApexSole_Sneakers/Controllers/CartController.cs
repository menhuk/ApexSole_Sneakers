using ApexSole_Sneakers.Interfaces;
using ApexSole_Sneakers.Repository;
using ApexSole_Sneakers.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ApexSole_Sneakers.ViewModels;
using ApexSole_Sneakers.Models;
using Microsoft.AspNetCore.Identity;
using Stripe.Checkout;

namespace ApexSole_Sneakers.Controllers
{
    public class CartController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IOrderHeadRepository _orderHeadRepository;
        private readonly IOrderInfoRepository _orderInfoRepository;
        public CartController( IShoppingCartRepository shoppingCartRepository, UserManager<AppUser> userManager, IOrderHeadRepository orderHeadRepository, IOrderInfoRepository orderInfoRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userManager = userManager;
            _orderHeadRepository = orderHeadRepository;
            _orderInfoRepository = orderInfoRepository;
        }
        public async Task<IActionResult> Index()
        {
            var shoppingCarts = await _shoppingCartRepository.GetAll();
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCartVM = new()
            {
                ShoppingCartList = shoppingCarts.Where(s => s.AppUserId == userId),
                OrderHead = new()
            };
            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = cart.Sneakers.SneakersPrice;
                ShoppingCartVM.OrderHead.total += (cart.Price * cart.Count);
            }
           
            return View(ShoppingCartVM);
        }

        public async Task<IActionResult> Summary()
        {
            var shoppingCarts = await _shoppingCartRepository.GetAll();
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCartVM = new()
            {
                ShoppingCartList = shoppingCarts.Where(s => s.AppUserId == userId),
                OrderHead = new()
            };
            var user = await _userManager.FindByIdAsync(userId);
            ShoppingCartVM.OrderHead.AppUser = user;
            ShoppingCartVM.OrderHead.Name = ShoppingCartVM.OrderHead.AppUser.UserName;
            ShoppingCartVM.OrderHead.PhoneNumber = ShoppingCartVM.OrderHead.AppUser.PhoneNumber;
            ShoppingCartVM.OrderHead.StreetAddress = ShoppingCartVM.OrderHead.AppUser.Address;
            ShoppingCartVM.OrderHead.City = ShoppingCartVM.OrderHead.AppUser.Address;
            ShoppingCartVM.OrderHead.State = ShoppingCartVM.OrderHead.AppUser.Address;
            ShoppingCartVM.OrderHead.PostalCode = ShoppingCartVM.OrderHead.AppUser.Address;

            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = cart.Sneakers.SneakersPrice;
                ShoppingCartVM.OrderHead.total += (cart.Price * cart.Count);
            }
            return View(ShoppingCartVM);
        }
        [HttpPost]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPOST()
        {
            var shoppingCarts = await _shoppingCartRepository.GetAll();
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCartVM.ShoppingCartList = shoppingCarts.Where(s => s.AppUserId == userId);
            ShoppingCartVM.OrderHead.OrderDate = System.DateTime.Now;
            ShoppingCartVM.OrderHead.AppUserId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            AppUser appUser = user;
            
            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = cart.Sneakers.SneakersPrice;
                ShoppingCartVM.OrderHead.total += (cart.Price * cart.Count);
            }

            ShoppingCartVM.OrderHead.PaymentStatus = Data.StatusOfOrder.PaymentStatusPending;
            ShoppingCartVM.OrderHead.OrderStatus = Data.StatusOfOrder.StatusPending;
            _orderHeadRepository.Add(ShoppingCartVM.OrderHead);
            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                OrderInfo orderInfo = new()
                {
                    ProductId = cart.ProductId,
                    OrderHeadId = ShoppingCartVM.OrderHead.Id,
                    Price = cart.Price,
                    Count = cart.Count
                };
                _orderInfoRepository.Add(orderInfo);
            }

            var domain = "https://localhost:7081/";
            var options = new Stripe.Checkout.SessionCreateOptions
            {
                SuccessUrl = domain+ $"cart/OrderConfirmation?id={ShoppingCartVM.OrderHead.Id}",
                CancelUrl = domain + "cart/index",
                LineItems = new List<Stripe.Checkout.SessionLineItemOptions>(),
                Mode = "payment",
            };
            foreach (var item in ShoppingCartVM.ShoppingCartList)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Price * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Sneakers.SneakersName
                        }
                    },
                    Quantity = item.Count
                };
                options.LineItems.Add(sessionLineItem);
            }
            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Create(options);
            _orderHeadRepository.UpdateStripePaymentId(ShoppingCartVM.OrderHead.Id,session.Id,session.PaymentIntentId);
            Response.Headers.Add("Location",session.Url);
            return new StatusCodeResult(303);
        }

        public async Task<IActionResult> OrderConfirmation(int id)
        {
            OrderHead orderHead = await _orderHeadRepository.GetByIdAsync(id);
            if (orderHead.PaymentStatus != Data.StatusOfOrder.PaymentStatusDelayedPayment)
            {
                var service = new SessionService();
                Session session = service.Get(orderHead.SessionId);
                if (session.PaymentStatus.ToLower()=="paid" )
                {
                    _orderHeadRepository.UpdateStripePaymentId(id, session.Id, session.PaymentIntentId);
                    _orderHeadRepository.UpdateStatus(id, Data.StatusOfOrder.StatusApproved,
                        Data.StatusOfOrder.PaymentStatusApproved);
                }
            }

            List<ShoppingCart> shoppingCarts = (await _shoppingCartRepository
                .GetAll()).Where(u => u.AppUserId == orderHead.AppUserId).ToList();
            foreach (var cart in shoppingCarts)
            {
                _shoppingCartRepository.Delete(cart);
            }
            return View(id);
        }
        public async Task<IActionResult> Plus(int cartId)
        {
            var cartformDb = await _shoppingCartRepository.GetByIdAsync(cartId);
            cartformDb.Count += 1;
            _shoppingCartRepository.Update(cartformDb);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Minus(int cartId)
        {
            var cartformDb = await _shoppingCartRepository.GetByIdAsync(cartId);
            if (cartformDb.Count <= 1)
            {
                _shoppingCartRepository.Delete(cartformDb);
            }
            else
            {
                cartformDb.Count -= 1;
                _shoppingCartRepository.Update(cartformDb);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Remove(int cartId)
        {
            var cartformDb = await _shoppingCartRepository.GetByIdAsync(cartId);
            _shoppingCartRepository.Delete(cartformDb);
            return RedirectToAction("Index");
        }
    }
}
