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

namespace Online_Food_Ordering_System.Controllers
{
    [Authorize (Roles = "Admin")]
    public class FoodOffersController : Controller
    {
        private DBConnect db = new DBConnect();

        // GET: FoodOffers
        public ActionResult Index()
        {
            var foodOffers = db.FoodOffers.Include(f => f.prdClass);
            return View(foodOffers.ToList());
        }

        // GET: FoodOffers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodOffer foodOffer = db.FoodOffers.Find(id);
            if (foodOffer == null)
            {
                return HttpNotFound();
            }
            return View(foodOffer);
        }

        // GET: FoodOffers/Create
        public ActionResult Create()
        {
            ViewBag.prdID = new SelectList(db.Products, "prdID", "prdName");
            return View();
        }

        // POST: FoodOffers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fofID,prdID,fofPrice,fofDesc")] FoodOffer foodOffer)
        {
            if (ModelState.IsValid)
            {
                db.FoodOffers.Add(foodOffer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.prdID = new SelectList(db.Products, "prdID", "prdName", foodOffer.prdID);
            return View(foodOffer);
        }

        // GET: FoodOffers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodOffer foodOffer = db.FoodOffers.Find(id);
            if (foodOffer == null)
            {
                return HttpNotFound();
            }
            ViewBag.prdID = new SelectList(db.Products, "prdID", "prdName", foodOffer.prdID);
            return View(foodOffer);
        }

        // POST: FoodOffers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fofID,prdID,fofPrice,fofDesc")] FoodOffer foodOffer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodOffer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.prdID = new SelectList(db.Products, "prdID", "prdName", foodOffer.prdID);
            return View(foodOffer);
        }

        // GET: FoodOffers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodOffer foodOffer = db.FoodOffers.Find(id);
            if (foodOffer == null)
            {
                return HttpNotFound();
            }
            return View(foodOffer);
        }

        // POST: FoodOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodOffer foodOffer = db.FoodOffers.Find(id);
            db.FoodOffers.Remove(foodOffer);
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
    }
}
