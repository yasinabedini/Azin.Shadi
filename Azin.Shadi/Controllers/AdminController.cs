using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Azin.Shadi.Models;

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

        
    }
}