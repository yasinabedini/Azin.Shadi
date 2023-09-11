using Azin.Shadi.Core.Security;
using Azin.Shadi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Azin.Shadi.Web.Pages.Admin.Transaction;

[PermissionChecker(18)]
public class TodayTransactionsModel : PageModel
{
    private readonly IUserService _userService;

    public TodayTransactionsModel(IUserService userService)
    {
        _userService = userService;
    }

    public List<DAL.Entities.Transaction.Transaction> Transactions { get; set; }

    public void OnGet()
    {
        Transactions = _userService.GetAllTransactions().Where(t => t.PayDate.Year == DateTime.Now.Year && t.PayDate.Month == DateTime.Now.Month && t.PayDate.Day == DateTime.Now.Day).ToList();
    }
}
