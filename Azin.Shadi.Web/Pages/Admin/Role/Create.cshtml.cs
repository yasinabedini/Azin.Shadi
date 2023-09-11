using Azin.Shadi.Core.Security;
using Azin.Shadi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Azin.Shadi.Web.Pages.Admin.Role;

[PermissionChecker(7)]
public class CreateModel : PageModel
{
    private readonly IPermissionService _permision;

    [BindProperty]
    public DAL.Entities.User.Role Role { get; set; }

    public CreateModel(IPermissionService permision)
    {
        _permision = permision;
    }
    public void OnGet()
    {
        ViewData["Permissions"] = _permision.GetPermission();
    }

    public IActionResult OnPost(List<int> permissionChecked)
    {
        _permision.CreateRole(Role);
        _permision.AddPermissionToRole(Role.RoleId, permissionChecked);
        return RedirectToPage("Index");
    }

}
