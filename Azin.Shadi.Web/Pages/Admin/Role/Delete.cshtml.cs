using Azin.Shadi.Core.Security;
using Azin.Shadi.Core.Services.Interfaces;
using Azin.Shadi.DAL.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Azin.Shadi.Web.Pages.Admin.Role;

[PermissionChecker(8)]
public class DeleteModel : PageModel
{
    private readonly IPermissionService _permision;

    [BindProperty]
    public DAL.Entities.User.Role Role { get; set; }

    public DeleteModel(IPermissionService permision)
    {
        _permision = permision;
    }
    public void OnGet(int id)
    {
        Role = _permision.GetRoleById(id);
    }

    public IActionResult OnPost()
    {
        _permision.DeleteRole(Role);
        return RedirectToPage("Index");
    }
}
