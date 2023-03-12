using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Azin.Shadi.Models;
using Azin.Shadi.Models.ViewModels;
using System.Net;
using System.IO;

namespace Azin.Shadi.Controllers
{
    public class AdminController : Controller
    {
        AzinShadiDbContext db = new AzinShadiDbContext();


        #region Dashboard
        [HttpGet]
        public ActionResult Dashboard(Admin admin)
        {
            if (admin.Mobile == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }
        #endregion

        #region Index
        public ActionResult Index()
        {
            var adminList = db.Admins;
            return View(adminList);
        }
        #endregion

        #region Create

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind(Include = "")] RegisterAdminViewModel admin, HttpPostedFileBase profileImage)
        {
            if (ModelState.IsValid)
            {
                string imageName = "defaultprofile.png";
                if (profileImage != null)
                {
                    if (profileImage.ContentType != "image/jpeg" && profileImage.ContentType != "image/png")
                    {
                        ModelState.AddModelError("","فقط عکس های با فرمت Png و jpeg قابل قبول است. ");
                    }


                    imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(profileImage.FileName);
                }


            }

            return View();
        }

        #endregion

        #region Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "Mobile,Password")] AdminLoginViewModel admin)
        {
            if (ModelState.IsValid)
            {
                if (!db.Admins.Any(t => t.Mobile == admin.Mobile))
                {
                    ModelState.AddModelError("Mobile", "هیچ ادمینی با این شماره موبایل ثبت نشده است");
                    return View();
                }
                var logedAdmin = db.Admins.First(t => t.Mobile == admin.Mobile);
                if (logedAdmin.Password == admin.Password)
                {
                    return RedirectToAction("DashBoard", logedAdmin);
                }
                ModelState.AddModelError("Password", "رمز عبور وارد نشده نادرست است!");
                return View();
            }

            return View();
        }
        #endregion

    }
}