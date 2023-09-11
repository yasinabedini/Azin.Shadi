using Azin.Shadi.Core.DTOs;
using Azin.Shadi.Core.DTOs.Enums;
using Azin.Shadi.Core.Services;
using Azin.Shadi.Core.Services.Interfaces;
using Azin.Shadi.DAL.Entities.Forward;
using Azin.Shadi.DAL.Entities.Order;
using Azin.Shadi.DAL.Entities.Transaction;
using Azin.Shadi.DAL.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace Azin.Shadi.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _service;
        private readonly IPermissionService _permissionService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public OrderController(IOrderService service, IPermissionService permissionService, IUserService userService, IProductService productService)
        {
            _service = service;
            _permissionService = permissionService;
            _userService = userService;
            _productService = productService;
        }


        #region Show Order
        [Route("Orders")]
        public IActionResult Index()
        {
            return View(_service.GetUserOrders(_permissionService.GetAuthonticatedUserUsername(Request.HttpContext)));
        }

        [Route("ShowMyOrder/{id}")]
        public IActionResult ShowMyOrder(int id, bool paySuccess = false, bool showMyCurrentCart = false)
        {
            string username = _permissionService.GetAuthonticatedUserUsername(Request.HttpContext);
            Order order = _service.GetUserOrderById(username, id);
            if (order == null && showMyCurrentCart == true)
            {
                ViewBag.NullCart = true;
                return View(order);
            }
            if (order == null)
            {
                return NotFound();
            }

            if (order.ForwardId != null)
            {
                ViewBag.TrackingCode = _service.GetForwardById(order.ForwardId.Value).TrackingCode;
            }

            return View(order);
        }

        [Authorize]
        [Route("MyCart")]
        public IActionResult MyCart()
        {
            string username = _permissionService.GetAuthonticatedUserUsername(Request.HttpContext);
            var myCurrentCart = _service.GetCurrentOrder(username) ?? new Order();
            return RedirectToAction("ShowMyOrder", new { id = myCurrentCart.Id, showMyCurrentCart = true });
        }
        #endregion

        #region Submit Order
        [Route("SubmitOrder/{id}")]
        public IActionResult SubmitOrder(int id)
        {
            string username = _permissionService.GetAuthonticatedUserUsername(Request.HttpContext);
            Order order = _service.GetUserOrderById(username, id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [Route("SubmitOrder")]
        [HttpPost]
        public IActionResult SubmitOrder(int id, string address)
        {
            string username = _permissionService.GetAuthonticatedUserUsername(Request.HttpContext);
            Order order = _service.GetUserOrderById(username, id);

            if (!string.IsNullOrEmpty(address))
            {
                User user = order.User;
                user.Address = address;
                _userService.UpdateUser(user);
            }

            order.IsFinaly = true;
            _service.UpdateOrder(order);

            return RedirectToAction("PayOrder", new { id = id, address = address });
        }
        #endregion

        #region Pay Order
        [Route("PayOrder/{id}")]
        [Authorize]
        public IActionResult PayOrder(int id, string discountStatus)
        {
            string username = _permissionService.GetAuthonticatedUserUsername(Request.HttpContext);

            Order order = _service.GetUserOrderById(username, id);
            if (order == null || order.IsFinaly == false || order.IsPay == true)
            {
                return NotFound();
            }

            PayOrderViewModel orderViewModel = new PayOrderViewModel()
            {
                Id = order.Id,
                ProductCount = order.OrderLines.Count,
                SumPrice = order.SumPrice,
                UserWalletBalance = _userService.GetWalletBalance(username)
            };

            return View(orderViewModel);
        }

        [Route("PayOrder")]
        [HttpPost]
        public IActionResult PayOrder(PayOrderViewModel order)
        {
            if (!ModelState.IsValid) return View(order);

            string username = _permissionService.GetAuthonticatedUserUsername(Request.HttpContext);
            order.Address = _userService.GetUserByUsername(username).Address;

            //Online Payment
            if (order.PaymentMethod == 1)
            {
                int transactionId = _userService.AddTransaction(username, order.SumPrice, "پرداخت آنلاین سبد خرید", 1);

                var payment = new ZarinpalSandbox.Payment(order.SumPrice);

                var res = payment.PaymentRequest(_userService.GetTransactionById(transactionId).Description, "https://Azinshadi.ir/onlinepayment/" + transactionId + "/?orderId=" + order.Id + "&username=" + username + "&address=" + order.Address, "yasinabedini1384@gmail.com", "09106966244");

                if (res.Result.Status == 100)
                {
                    Response.Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
                }
            }

            //Wallet Payment
            if (order.PaymentMethod == 2)
            {
                DAL.Entities.User.User user = _userService.GetUserByUsername(username);
                int transactionId = _userService.AddTransaction(user.Username, order.SumPrice, $"برداشت کیف پول پرداخت سبد خرید با کد {order.Id}", 2);
                Transaction transaction = _userService.GetTransactionById(transactionId);
                transaction.IsComplete = true;
                _userService.UpdateTransaction(transaction);

                _service.PayOrder(username, order.Id, order.Address);

                ViewBag.PaySuccess = true;

                Response.Cookies.Delete("address");

                return RedirectToAction("ShowMyOrder", new { id = order.Id, paySuccess = true });
            }

            return View(order);
        }
        #endregion


        #region Affect Discount
        [Route("AffectDiscount/{id}")]
        public IActionResult AffectDiscount(int id, string address, string code)
        {
            string username = _permissionService.GetAuthonticatedUserUsername(Request.HttpContext);
            User user = _userService.GetUserByUsername(username);
            if (string.IsNullOrEmpty(address))
            {
                address = user.Address;
            }
            Order order = _service.GetUserOrderById(username, id);
            var discountStatus = _service.CheckDiscount(user.UserId, code);

            if (discountStatus != DiscountStatus.Success)
            {
                return RedirectToAction("PayOrder", new { id = order.Id, address = address, discountStatus = discountStatus.ToString() });
            }

            order = _service.EffectDiscount(user.UserId, id, code);

            return RedirectToAction("PayOrder", new { id = order.Id, address = address, discountStatus = discountStatus.ToString() });
        }
        #endregion                    
    }
}
