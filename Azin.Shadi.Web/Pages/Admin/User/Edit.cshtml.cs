using Azin.Shadi.Core.DTOs;
using Azin.Shadi.Core.Generators;
using Azin.Shadi.Core.Security;
using Azin.Shadi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace Azin.Shadi.Web.Pages.Admin.User
{
    [PermissionChecker(5)]
    public class EditModel : PageModel
    {
        private readonly IUserService _service;
        private readonly IPermissionService _permision;

        public EditModel(IUserService service, IPermissionService permision)
        {
            _service = service;
            _permision = permision;
        }

        [BindProperty]
        public EditUserViewModel EditUser { get; set; }

        public void OnGet(int id)
        {
            EditUser = _service.GetInformationFoEditPanel(id);
            ViewData["Roles"] = _permision.GetRoles();
        }

        public IActionResult OnPost(List<int> selectedRoles)
        {
            ViewData["Roles"] = _permision.GetRoles();
            if (!ModelState.IsValid) return Page();

            EditUser.Roles = selectedRoles;            
            _permision.UpdateUserRoles(_service.GetUserById(EditUser.UserId).Username, selectedRoles);

            if (_service.EditUserByAdmin(EditUser, selectedRoles))
            {
                return RedirectToPage("Index");
            }
            ModelState.AddModelError("Username", "مشکلی رخ داده است!!");
            return Page();
        }
    }
}
