using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Azin.Shadi.Models;
using Azin.Shadi.Models.ViewModels;

namespace Azin.Shadi.Controllers
{
    public class AdminController : Controller
    {
        AzinShadiDbContext db = new AzinShadiDbContext();

        public ActionResult Index()
        {
            var adminList = db.Admins;
            return View(adminList);
        }


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
                    return View("DashBoard");
                }
                ModelState.AddModelError("Password", "رمز عبور وارد نشده نادرست است!");
                return View();
            }

            return View();
        }


    }
}