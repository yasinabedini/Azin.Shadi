using Azin.Shadi.Core.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Azin.Shadi.Web.Pages.Admin.ProductGroup;

[PermissionChecker(21)]
public class EditModel : PageModel
{
    public void OnGet()
    {
    }
}
