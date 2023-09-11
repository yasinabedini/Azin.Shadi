using Azin.Shadi.Core.Security;
using Azin.Shadi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Azin.Shadi.Web.Pages.Admin.Role;

[PermissionChecker(9)]
public class EditModel : PageModel
{
    private readonly IPermissionService _permision;

    [BindProperty]
    public DAL.Entities.User.Role Role { get; set; }

    public EditModel(IPermissionService permision)
    {
        _permision = permision;
    }
    public void OnGet(int id)
    {
        ViewData["Permissions"] = _permision.GetPermission();
        ViewData["CurrentPermission"] = _permision.GetPermissionByRole(id);
        Role = _permision.GetRoleById(id);
    }

    public IActionResult OnPost(List<int> permissionChecked)
    {
        _permision.UpdateRole(Role);
        _permision.UpdateRolePermissions(Role.RoleId, permissionChecked);
        return RedirectToPage("Index");
    }
}
