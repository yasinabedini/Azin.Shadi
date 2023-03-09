using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Azin.Shadi.Models;

namespace Azin.Shadi.Controllers
{
    public class ManageController : Controller
    {
        AzinShadiDbContext db = new AzinShadiDbContext();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Mobile, string Password)
        {
            if (Mobile == null || Password == null)
            {
                return View();
            }
            if (db.Admins.Any(t => t.Mobile == Mobile))
            {
                var adminFind = db.Admins.First(t => t.Mobile == Mobile);
                
                if (adminFind.Password != Password)
                {
                    return View();
                }

                return View("Index");
            }
            return View();
        }
    }
}