using Azin.Shadi.Core.DTOs;
using Azin.Shadi.Core.Security;
using Azin.Shadi.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Azin.Shadi.Web.Pages.User
{
    [PermissionChecker(2)]
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public ShowUserForAdminViewModel ShowUserForAdmin { get; set; }

        public void OnGet(int pageId = 1, string emailFilter = "", string nameFilter = "")
        {
            ShowUserForAdmin = _userService.GetUsers(pageId,emailFilter,nameFilter);
        }
    }
}
