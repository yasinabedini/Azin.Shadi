using Azin.Shadi.Core.Security;
using Azin.Shadi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Azin.Shadi.Web.Pages.Admin.Order
{
    [PermissionChecker(17)]
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;

        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public List<DAL.Entities.Order.Order> Orders { get; set; }

        public void OnGet()
        {
            Orders = _orderService.GetAllOrder();            
        }
    }
}
