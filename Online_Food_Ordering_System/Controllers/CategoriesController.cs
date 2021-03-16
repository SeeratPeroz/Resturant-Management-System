using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Online_Food_Ordering_System.Models;
using Online_Food_Ordering_System.Models.Entity;
using System.IO;

namespace Online_Food_Ordering_System.Controllers
{
    [Authorize (Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private DBConnect db = new DBConnect();

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "catID,catName,catDesc,catMinPrice,catViews,catDelivery")] Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Categories.Add(category);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(category);
        //}
        public ActionResult Create(Category im)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileName(im.ImageFile.FileName);
                im.ImagePath = "~/images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/images"), fileName);
                im.ImageFile.SaveAs(fileName);
                db.Categories.Add(im);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(im);
        }






        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tbl_img = db.Categories.Find(id);
            Session["imgPath"] = tbl_img.ImagePath;

            if (tbl_img == null)
            {
                return HttpNotFound();
            }
            return View(tbl_img);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "catID,catName,catDesc,catMinPrice,catViews,catDelivery")] Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(category).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(category);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase ImageFile, Category im)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    string fileName = Path.GetFileName(im.ImageFile.FileName);
                    im.ImagePath = "~/images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/images"), fileName);
                    im.ImageFile.SaveAs(fileName);
                    db.Entry(im).State = EntityState.Modified;
                    string oldImgPath = Request.MapPath(Session["imgPath"].ToString());
                    if (db.SaveChanges() > 0)
                    {
                        if (System.IO.File.Exists(oldImgPath))
                        {
                            System.IO.File.Delete(oldImgPath);
                        }

                    }
                    // db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    im.ImagePath = Session["imgPath"].ToString();
                    db.Entry(im).State = EntityState.Modified;
                    if (db.SaveChanges() > 0)
                    {
                        return RedirectToAction("Index");
                    }

                }
            }
            return View();
        }





        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        // Searching Categories
        public ActionResult SearchCat(string search)
        {
            var dos = db.Categories.Where(x => x.catName.Contains(search) || search == null);
            return View(dos);
        }





    }
}
