using Azin.Shadi.Core.Security;
using Azin.Shadi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Azin.Shadi.Web.Pages.Admin.Order
{
    [PermissionChecker(17)]
    public class PayedOrderModel : PageModel
    {

        private readonly IOrderService _orderService;

        public PayedOrderModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IEnumerable<DAL.Entities.Order.Order> Orders { get; set; }

        public void OnGet()
        {
            Orders = _orderService.GetAllOrder().Where(t => t.IsPay && t.Forward != null && !t.Forward.IsForward);            
        }
    }
}
