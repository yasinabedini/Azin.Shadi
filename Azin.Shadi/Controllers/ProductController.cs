using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Azin.Shadi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Net;
using System.Data.Entity;

namespace Azin.Shadi.Controllers
{
    public class ProductController : Controller
    {
        AzinShadiDbContext db = new AzinShadiDbContext();

        #region index
        [HttpGet]
        public ActionResult Index()
        {
            var products = db.Products;
            return View(products);
        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Category,Brand,Price,Inventory")] Product product, HttpPostedFileBase imageName)
        {
            if (ModelState.IsValid)
            {
                string imageNameInfo = "defaultImage.png";

                if (imageName != null)
                {
                    if (imageName.ContentType != "image/jpeg" && imageName.ContentType != "image/png")
                    {
                        ModelState.AddModelError("ImageName", "فرمت عکس شما فقط باید jpeg یا jpg یا png باشد!");
                        return View();
                    }

                    if (imageName.ContentLength > 500000)
                    {
                        ModelState.AddModelError("ImageName", "حداکثر حجم عکس آپلودی شما باید 500 کلوبایت باشد!");
                        return View();
                    }

                    imageNameInfo = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imageName.FileName);
                    imageName.SaveAs(Server.MapPath("/Images/ProductImage/" + imageNameInfo));
                }
                product.ImageName = imageNameInfo;


                if (product.Brand == "" || product.Brand == null)
                {
                    product.Brand = "";
                }
                if (product.Inventory > 0)
                {
                    product.InventoryStatus = true;
                }
                else
                {
                    product.InventoryStatus = false;
                }
                product.RegisterDate = DateTime.Now;
                product.IsActive = true;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("index");
            }

            return View();
        }
        #endregion

        #region Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!db.Products.Any(t => t.id == id))
            {
                return HttpNotFound();
            }

            var product = db.Products.Find(id);
            return View(product);
        }
        #endregion

        #region Edit
        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!db.Products.Any(t => t.id == Id))
            {
                return HttpNotFound();
            }

            return View(db.Products.Find(Id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id, Name, Category, Brand, Price, IsActive,RegisterDate,Inventory,InventoryStatus, ImageName")] Product product, HttpPostedFileBase imageUpload)
        {
            if (ModelState.IsValid)
            {
                if (imageUpload != null)
                {
                    if (imageUpload.ContentType != "image/jpeg" && imageUpload.ContentType != "image/jpg" && imageUpload.ContentType != "image/png")
                    {
                        ModelState.AddModelError("ImageName", "شما فقط باید عکس با فرمت Jpeg, png یا jpg آپلود کنید!");
                        return View(product);
                    }
                    if (imageUpload.ContentLength > 500000)
                    {
                        ModelState.AddModelError("ImageName", "حداکثر حجم عکس آپلود شده باید 500 کیلوبایت باشد !");
                        return View(product);
                    }
                    if (product.ImageName != "defaultImage.png")
                    {
                        if (System.IO.File.Exists(Server.MapPath("/images/productimage/") + product.ImageName))
                        {
                            System.IO.File.Delete(Server.MapPath("/images/productimage/") + product.ImageName);
                        }
                    }
                    string newImageName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(imageUpload.FileName);
                    imageUpload.SaveAs(Server.MapPath("/Images/ProductImage/" + newImageName));
                    product.ImageName = newImageName;
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            return View(product);
        }
        #endregion

        #region Delete

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!db.Products.Any(t => t.id == id))
            {
                return HttpNotFound();
            }

            return View(db.Products.Find(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProduct(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!db.Products.Any(t => t.id == id))
            {
                return HttpNotFound();
            }
            var product = db.Products.Find(id);
            if (product != null)
            {
                if (product.ImageName != "defaultImage.png")
                {
                    if (System.IO.File.Exists(Server.MapPath("/images/productimage/") + product.ImageName))
                    {
                        System.IO.File.Delete(Server.MapPath("/images/productimage/") + product.ImageName);
                    }
                }
                db.Products.Remove(product);
                db.SaveChanges();
                db.Entry(product).State = EntityState.Modified;
                return RedirectToAction("Index");
            }
            return View();
        }
        #endregion

        #region Search

        [HttpGet]
        public ActionResult Search(string Search)
        {            
            if (!db.Products.Any(t => t.Name.TrimEnd().Contains(Search.Trim())))
            {
                return HttpNotFound();
            }

            var productsFind = db.Products.Where(t => t.Name.Contains(Search.Trim()));
            return View("index", productsFind);
        }

        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

    }
}