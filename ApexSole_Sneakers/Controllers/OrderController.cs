using ApexSole_Sneakers.Data;
using ApexSole_Sneakers.Interfaces;
using ApexSole_Sneakers.Models;
using ApexSole_Sneakers.Repository;
using ApexSole_Sneakers.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Climate;

namespace ApexSole_Sneakers.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderHeadRepository _orderHeadRepository;
        [BindProperty]
        public OrderVM OrderVm { get; set; }
        private readonly IOrderInfoRepository _orderInfoRepository;
        public OrderController(IOrderHeadRepository orderHeadRepository,IOrderInfoRepository orderInfoRepository)
        {
            _orderHeadRepository = orderHeadRepository;
            _orderInfoRepository = orderInfoRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index(string status)
        {
            IEnumerable<OrderHead> objOrderHeads = await _orderHeadRepository.GetAll();

            if (!string.IsNullOrEmpty(status) && status.ToLower() != "all")
            {
                objOrderHeads = objOrderHeads.Where(o => o.OrderStatus.ToLower() == status.ToLower());
            }

            return View(objOrderHeads);
        }
        public async Task<IActionResult> Details(int orderId)
        {
            var orderHead = await _orderHeadRepository.GetByIdAsync(orderId);
            var orderDetails = await _orderInfoRepository.GetAll(orderId);

            var orderVM = new OrderVM
            {
                OrderHead = orderHead,
                OrderInfo = orderDetails
            };

            return View(orderVM);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateOrderDetail()
        {
            var orderHeadFromDb = await _orderHeadRepository.GetByIdAsync(OrderVm.OrderHead.Id);
            orderHeadFromDb.Name = OrderVm.OrderHead.Name;
            orderHeadFromDb.PhoneNumber = OrderVm.OrderHead.PhoneNumber;
            orderHeadFromDb.StreetAddress = OrderVm.OrderHead.StreetAddress;
            orderHeadFromDb.City = OrderVm.OrderHead.City;
            orderHeadFromDb.State = OrderVm.OrderHead.State;
            orderHeadFromDb.PostalCode = OrderVm.OrderHead.PostalCode;
            if (!string.IsNullOrEmpty(OrderVm.OrderHead.Carrier))
            {
                orderHeadFromDb.Carrier = OrderVm.OrderHead.Carrier;
            }
            if (!string.IsNullOrEmpty(OrderVm.OrderHead.TrackingNumber))
            {
                orderHeadFromDb.Carrier = OrderVm.OrderHead.TrackingNumber;
            }

            _orderHeadRepository.Update(orderHeadFromDb);
            return RedirectToAction(nameof(Details),new { orderId = orderHeadFromDb.Id});
        }
        [HttpPost]
        public async Task<IActionResult> StartProcessing()
        {
            _orderHeadRepository.UpdateStatus(OrderVm.OrderHead.Id, StatusOfOrder.StatusInProcess);
            return RedirectToAction(nameof(Details), new { orderId = OrderVm.OrderHead.Id });
        }
        [HttpPost]
        public async Task<IActionResult> ShipOrder()
        {
            var orderHead = await _orderHeadRepository.GetByIdAsync(OrderVm.OrderHead.Id);
            orderHead.TrackingNumber = OrderVm.OrderHead.TrackingNumber;
            orderHead.Carrier = OrderVm.OrderHead.Carrier;
            orderHead.OrderStatus = StatusOfOrder.StatusShipped;
            orderHead.ShippingDate = DateTime.Now;
            _orderHeadRepository.Update(orderHead);
            _orderHeadRepository.UpdateStatus(OrderVm.OrderHead.Id, StatusOfOrder.StatusShipped);
            return RedirectToAction(nameof(Details), new { orderId = OrderVm.OrderHead.Id });
        }
        [HttpPost]
        public async Task<IActionResult> CancelOrder()
        {
            var orderHead = await _orderHeadRepository.GetByIdAsync(OrderVm.OrderHead.Id);
            if (orderHead.PaymentStatus == StatusOfOrder.PaymentStatusApproved)
            {
                var options = new RefundCreateOptions
                {
                    Reason = RefundReasons.RequestedByCustomer,
                    PaymentIntent = orderHead.PaymentIntentId
                };
                var service = new RefundService();
                Refund refund = service.Create(options);
                _orderHeadRepository.UpdateStatus(orderHead.Id, StatusOfOrder.StatusCancelled,
                    StatusOfOrder.StatusRefunded);
            }
            else
            {
                _orderHeadRepository.UpdateStatus(orderHead.Id, StatusOfOrder.StatusCancelled,
                    StatusOfOrder.StatusCancelled);
            }
            return RedirectToAction(nameof(Details), new { orderId = OrderVm.OrderHead.Id });
        }
    }
}
