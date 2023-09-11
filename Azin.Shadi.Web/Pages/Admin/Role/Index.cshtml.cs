using Azin.Shadi.DAL.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Azin.Shadi.DAL.Entities;
using Azin.Shadi.Core.Services.Interfaces;
using Azin.Shadi.Core.Security;

namespace Azin.Shadi.Web.Pages.Admin.Role;

[PermissionChecker(6)]
public class IndexModel : PageModel
{
    private readonly IPermissionService _permision;

    public IndexModel(IPermissionService permision)
    {
        _permision = permision;
    }
    public List<DAL.Entities.User.Role> Roles { get; set; }

    public void OnGet()
    {
        Roles = _permision.GetRoles();
    }
}
