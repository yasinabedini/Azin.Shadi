using Azin.Shadi.Core.Convertors;
using Azin.Shadi.Core.DTOs;
using Azin.Shadi.Core.Generators;
using Azin.Shadi.Core.Security;
using Azin.Shadi.Core.Senders;
using Azin.Shadi.Core.Services.Interfaces;
using Azin.Shadi.DAL.Entities.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Azin.Shadi.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IViewRenderService _renderService;

        public AccountController(IUserService userService, IViewRenderService renderService)
        {
            this._userService = userService;
            this._renderService = renderService;
        }

        #region Register
        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(UserRegisterViewModel user)
        {
            if (!ModelState.IsValid) return View(user);

            if (_userService.IsEmailExist(user.Email))
            {
                ModelState.AddModelError("Email", "کاربری با این ایمیل ثبت نام شده است");
                return View(user);
            }

            _userService.AddUser(user);

            return RedirectToAction("login", new { newRegister = true });
        }
        #endregion

        #region Login
        [HttpGet]
        [Route("Login")]
        public IActionResult Login(bool newRegister = false)
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var newUser = _userService.LoginUser(user);
            if (newUser != null)
            {
                if (newUser.IsActive)
                {

                    var claims = new List<Claim>()
                    {
                       new Claim(ClaimTypes.NameIdentifier,newUser.Username.ToString()),
                       new Claim(ClaimTypes.Name, newUser.Name)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = user.RemmemberMe
                    };
                    HttpContext.SignInAsync(principal, properties);

                    ViewBag.IsSuccess = true;

                    return View();
                }

                else
                {
                    ModelState.AddModelError("Email", "حساب کاربری شما فعال نمی باشد!");
                    return View(user);
                }
            }
            else
            {
                ModelState.AddModelError("Email", "کاربری با این مشخصات یافت نشد!");
                return View(user);
            }
        }
        #endregion

        #region LogOut
        [Route("LogOut")]
        public IActionResult LogOut(int id)
        {
            HttpContext.SignOutAsync();
            return Redirect("/login");
        }
        #endregion

        #region ValidateEmail User
        public IActionResult SendValidationEmail(int id)
        {
            User user = _userService.GetUserById(id);
            string body = _renderService.RenderToStringAsync("_ValidationEmail", user);
            SendEmail.Send(user.Email, "اعتبار سنجی ایمیل", body);
            return View(user);
        }

        [HttpGet]
        public IActionResult ValidateEmail(string id)
        {
            if (_userService.ValidateEmail(id))
            {
                ViewBag.IsValidate = true;
                return View();
            }
            else
            {
                ViewBag.Error = true;
                return View();
            }
        }
        #endregion

        #region Forgot Password

        [Route("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgotPassword)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = _userService.GetUserByEmail(forgotPassword.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "کاربری با این ایمیل ثبت نام نشده است!");
                return View(forgotPassword);
            }
            string body = _renderService.RenderToStringAsync("_ForgotPassword", user);
            SendEmail.Send(user.Email, "بازیابی کلمه عبور", body);
            ViewBag.IsSuccess = true;
            return View();
        }
        #endregion

        #region Reset Password
        [HttpGet]
        public IActionResult ResetPassword(string id)
        {
            var user = _userService.GetUserByActivationCode(id);
            if (user == null) return NotFound();
            return View(new ResetPasswordViewModel
            {
                ActivationCode = id
            });
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = _userService.GetUserByActivationCode(resetPassword.ActivationCode);
            if (user == null) return NotFound();
            string password = PasswordHelper.EncodePasswordMd5(resetPassword.Password);
            user.Password = password;
            user.ActivationCode = Generator.CreateUniqueText();
            _userService.UpdateUser(user);
            ViewBag.IsSuccess = true;
            return View();
        }
        #endregion
    }
}
