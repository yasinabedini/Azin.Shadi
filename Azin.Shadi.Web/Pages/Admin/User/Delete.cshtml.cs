using Azin.Shadi.Core.DTOs;
using Azin.Shadi.Core.Security;
using Azin.Shadi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Azin.Shadi.Web.Pages.Admin.User
{
    [PermissionChecker(4)]
    public class DeleteModel : PageModel
    {
        private readonly IUserService _service;

        [BindProperty]
        public EditUserViewModel DeleteUser { get; set; }

        public DeleteModel(IUserService service)
        {
            _service = service;
        }
        public void OnGet(int id)
        {
            DeleteUser = _service.GetInformationFoEditPanel(id);
        }

        public IActionResult OnPost(int id)
        {
            if (_service.DeleteUser(id))
            {
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
