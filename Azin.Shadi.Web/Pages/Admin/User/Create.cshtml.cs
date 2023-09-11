using Azin.Shadi.Core.DTOs;
using Azin.Shadi.Core.Security;
using Azin.Shadi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Azin.Shadi.Web.Pages.Admin.User;

[PermissionChecker(3)]
public class CreateModel : PageModel
{
    private readonly IPermissionService _permision;
    private readonly IUserService _Service;

    [BindProperty]
    public CreateUserViewModel User { get; set; }
    public CreateModel(IPermissionService permision, IUserService Service)
    {
        _permision = permision;
        _Service = Service;
    }
    public void OnGet()
    {
        ViewData["Roles"] = _permision.GetRoles();
    }

    public IActionResult OnPost(List<int> SelectedRoles)
    {
        ViewData["Roles"] = _permision.GetRoles();
        if (!ModelState.IsValid) return Page();
        int userId = _Service.AddUserForAdminPanel(User);

        foreach (var roleId in SelectedRoles)
        {
            _permision.AddRoleToUserRole(roleId, userId);
            
        }

        return RedirectToPage("Index");
    }
}