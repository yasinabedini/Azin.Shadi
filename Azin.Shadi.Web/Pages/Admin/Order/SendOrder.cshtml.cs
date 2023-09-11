using Azin.Shadi.Core.Security;
using Azin.Shadi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Azin.Shadi.Web.Pages.Admin.Order
{
    [PermissionChecker(17)]
    public class SendOrderModel : PageModel
    {
        private readonly IOrderService _orderService;

        public SendOrderModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [BindProperty]
        public DAL.Entities.Order.Order Order { get; set; }

        public void OnGet(int id)
        {
            Order = _orderService.GetOrderById(id);
        }

        public IActionResult OnPost(string trackingCode)
        {
            _orderService.SendOrder(Order.Id,trackingCode);
            return RedirectToPage("payedOrder");
        }
    }
}
