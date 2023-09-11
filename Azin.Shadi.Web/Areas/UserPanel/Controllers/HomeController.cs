using Azin.Shadi.Core.DTOs;
using Azin.Shadi.Core.Services;
using Azin.Shadi.Core.Services.Interfaces;
using Azin.Shadi.DAL.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Azin.Shadi.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;

        public HomeController(IUserService userService, IPermissionService permissionService)
        {
            this._userService = userService;
            _permissionService = permissionService;
        }

        #region Profile
        [Route("UserPanel/Profile")]
        public IActionResult Profile()
        {
            string username = _permissionService.GetAuthonticatedUserUsername(Request.HttpContext);
            var user = _userService.GetUserByUsername(username);

            return View(new UserPanelInformationViewModel
            {
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                RegisterDate = user.RegisterDate,
                Wallet = _userService.GetWalletBalance(username)
            });
        }
        #endregion

        #region Edit Profile
        [Route("/Userpanel/EditProfile")]
        public IActionResult EditProfile()
        {
            string username = _permissionService.GetAuthonticatedUserUsername(Request.HttpContext);
            var user = _userService.GetUserByUsername(username);

            return View(_userService.GetUserDataForEditProfile(user));
        }

        [HttpPost]
        [Route("/Userpanel/EditProfile")]
        public IActionResult EditProfile(EditProfileViewModel user)
        {            
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            string username = _permissionService.GetAuthonticatedUserUsername(Request.HttpContext);

            if (_userService.EditUserProfile(username, user))
            {
                ViewBag.IsSuccess = true;
                return View(user);
            }

            return View();
        }
        #endregion

        #region Change Password

        [Route("/Userpanel/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Route("/Userpanel/ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordViewModel password)
        {
            if (!ModelState.IsValid) return View(password);

            string username = _permissionService.GetAuthonticatedUserUsername(Request.HttpContext);

            if (!_userService.UserExistValidation(username,password.OldPassword))
            {
                ModelState.AddModelError("OldPassword", "کلمه عبور فعلی را به درستی وارد گنید !");
                return View(password);
            }
                    
            if (_userService.ChangeUserPassword(username,password.NewPassword))
            {
                ViewBag.IsSuccess = true;
                
                return View();
            }
                       
            return View();
        }

        #endregion

    }
}
