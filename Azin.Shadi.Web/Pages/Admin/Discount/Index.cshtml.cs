using Azin.Shadi.Core.Security;
using Azin.Shadi.Core.Services.Interfaces;
using Azin.Shadi.DAL.Entities.Discount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Azin.Shadi.Web.Pages.Admin.Discount
{
    [PermissionChecker(14)]
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;

        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public List<DAL.Entities.Discount.Discount> Discounts { get; set; }

        public void OnGet()
        {
            Discounts = _orderService.GetAllDiscount();
        }
    }
}
