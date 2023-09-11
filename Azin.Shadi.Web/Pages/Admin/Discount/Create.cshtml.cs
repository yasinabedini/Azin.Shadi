using Microsoft.AspNetCore.Mvc.RazorPages;
using Azin.Shadi.Core.Services.Interfaces;
using Azin.Shadi.DAL.Entities.Discount;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Azin.Shadi.Core.Security;

namespace Azin.Shadi.Web.Pages.Admin.Discount
{
    [PermissionChecker(15)]
    public class CreateModel : PageModel
    {
        private readonly IOrderService _orderService;

        public CreateModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [BindProperty]
        public DAL.Entities.Discount.Discount Discount { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost(string? stDate, string? edDate)
        {
            if (!ModelState.IsValid) return Page();

            if (!string.IsNullOrEmpty(stDate))
            {
                string[] sd = stDate.Split('/');
                Discount.StartDate = new DateTime(int.Parse(sd[0]), int.Parse(sd[1]), int.Parse(sd[2]), new PersianCalendar());
            }

            if (!string.IsNullOrEmpty(edDate))
            {
                string[] ed = edDate.Split('/');
                Discount.EndDate = new DateTime(int.Parse(ed[0]), int.Parse(ed[1]), int.Parse(ed[2]), new PersianCalendar());
            }
           
            _orderService.AddDiscount(Discount);

            return RedirectToPage("index");
        }
    }
}
