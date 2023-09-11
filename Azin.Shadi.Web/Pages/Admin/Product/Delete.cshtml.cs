using Azin.Shadi.Core.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Azin.Shadi.Web.Pages.Admin.Product;

[PermissionChecker(13)]
public class DeleteModel : PageModel
{
    public void OnGet()
    {
    }
}
