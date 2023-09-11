using Azin.Shadi.Core.Security;
using Azin.Shadi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Azin.Shadi.Web.Pages.Admin.Role;

[PermissionChecker(6)]
public class DeleteRoleListModel : PageModel
{
    private readonly IPermissionService _permision;

    public DeleteRoleListModel(IPermissionService permisoin)
    {
        _permision = permisoin;
    }
    public List<DAL.Entities.User.Role> Roles { get; set; }

    public void OnGet()
    {
        Roles = _permision.GetDeletedRoles();
    }
}
