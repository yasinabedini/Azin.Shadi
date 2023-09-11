using Azin.Shadi.Core.Security;
using Azin.Shadi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Azin.Shadi.Web.Pages.Admin.Transaction;

[PermissionChecker(18)]
public class IndexModel : PageModel
{
    private readonly IUserService _userService;

    public IndexModel(IUserService userService)
    {
        _userService = userService;
    }

    public List<DAL.Entities.Transaction.Transaction> Transactions { get; set; }

    public void OnGet()
    {
        Transactions = _userService.GetAllTransactions();
    }
}
