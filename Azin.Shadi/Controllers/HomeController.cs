using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Azin.Shadi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignupSeller()
        {
            return View();
        }

        public ActionResult SignInUserPanel()
        {
            return View();
        }

        public ActionResult SignUpUserPanel()
        {
            return View();
        }
        public ActionResult BestSeller()
        {
            return PartialView();
        }


        public ActionResult Balloondecoration()
        {
            return View();
        }

        public ActionResult BalloonGallery()
        {
            return PartialView();
        }

        public ActionResult TestAction()
        {
            return View();

        }

    }
}