using Azin.Shadi.Core.Security;
using Azin.Shadi.Core.Services.Interfaces;
using Azin.Shadi.DAL.Entities.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Azin.Shadi.Web.Pages.Admin.Order
{
    [PermissionChecker(17)]
    public class ShowOrderModel : PageModel
    {
        private readonly IOrderService _orderService;

        public ShowOrderModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public DAL.Entities.Order.Order Order { get; set; }

        public void OnGet(int id)
        {
            Order = _orderService.GetOrderById(id);           
        }
    }
}
