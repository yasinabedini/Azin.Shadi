using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Azin.Shadi.Models;
using Azin.Shadi.Models.ViewModels;
using System.Net;
using System.IO;
using Azin.Shadi.App_Start;
using Azin.Shadi.MyClasses;



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
        public ActionResult Register([Bind(Include = "Name,Family,Username,Password,PasswordConfirm,Email,Mobile,Department")] RegisterAdminViewModel admin, HttpPostedFileBase imageUpload)
        {
            if (ModelState.IsValid)
            {
                string imageName = "defaultprofile.png";
                if (imageUpload != null)
                {
                    if (imageUpload.ContentType != "image/jpeg" && imageUpload.ContentType != "image/png")
                    {
                        ModelState.AddModelError("ImageName", "فرمت عکس شما فقط باید jpeg یا jpg یا png باشد!");
                        return View();
                    }
                    if (imageUpload.ContentLength > 500000)
                    {
                        ModelState.AddModelError("ImageName", "حداکثر حجم عکس آپلودی شما باید 500 کلوبایت باشد!");
                        return View();
                    }
                    imageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imageUpload.FileName);
                    imageUpload.SaveAs(Server.MapPath(@"Images\AdminProfilePicture\") + imageName);

                }
                Admin newAdmin = new Admin()
                {
                    ImageName = imageName,
                    IsActive = true,
                    RegisterDate = DateTime.Now,
                    Department = "admin"

                };
                AutoMapperConfig.mapper.Map(admin, newAdmin);
                db.Admins.Add(newAdmin);
                db.SaveChanges();

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