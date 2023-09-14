using Azin.Shadi.Core.Services;
using Azin.Shadi.Core.Services.Interfaces;
using Azin.Shadi.Core.Tools;
using Azin.Shadi.DAL.Entities.Forward;
using Azin.Shadi.DAL.Entities.Order;
using Azin.Shadi.DAL.Entities.Transaction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Configuration;

namespace Azin.Shadi.Web.Controllers
{    
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProductService _ProductService;
        private readonly IOrderService _orderService;

        public HomeController(IUserService userService, IProductService productService, IOrderService orderService)
        {
            _userService = userService;
            _ProductService = productService;
            _orderService = orderService;
        }
        
        public IActionResult Index()
        {
            ViewBag.ProductGroups = _ProductService.GetProductGroups();
            ViewBag.popularProduct = _ProductService.GetPopularProducts();
            return View(_ProductService.GetProducts(orderByCreateDate:true));
        }

        [Route("OnlinePayment/{id}")]
        public IActionResult OnlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" && HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" && HttpContext.Request.Query["Authority"] != "")
            {
                string autority = HttpContext.Request.Query["Authority"];

                string orderId = HttpContext.Request.Query["orderId"];
                string username = HttpContext.Request.Query["username"];
                string address = HttpContext.Request.Query["address"];

                Transaction transaction = _userService.GetTransactionById(id);

                var payment = new ZarinpalSandbox.Payment(transaction.Amount);
                var res = payment.Verification(autority).Result;
                if (res.Status == 100)
                {
                    ViewBag.Code = res.RefId;
                    ViewBag.IsSuccess = true;

                    transaction.IsComplete = true;
                    _userService.UpdateTransaction(transaction);

                    if (!string.IsNullOrEmpty(orderId) && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(address))
                    {
                        bool success = _orderService.PayOrder(username, int.Parse(orderId), address);
                        if (!success)
                        {
                            return NotFound();
                        }
                        return Redirect($"https://Azinshadi.ir/showMyOrder/{orderId}?paySuccess=true");
                    }

                }
            }
            return View();
        }
    }
}
