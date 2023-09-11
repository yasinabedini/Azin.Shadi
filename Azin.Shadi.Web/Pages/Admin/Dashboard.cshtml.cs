using Azin.Shadi.Core.Security;
using Azin.Shadi.Core.Services.Interfaces;
using Azin.Shadi.DAL.Entities.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Azin.Shadi.Web.Pages.Admin
{

    [PermissionChecker(1)]
    public class DashboardModel : PageModel
    {
        private readonly IUserService _userService;

        public DashboardModel(IUserService userService)
        {
            _userService = userService;
        }

        public List<DAL.Entities.Transaction.Transaction> Transactions { get; set; }

        public void OnGet()
        {
            Transactions = _userService.GetAllTransactions().OrderByDescending(t=>t.PayDate).Take(10).ToList();
        }
    }
}
