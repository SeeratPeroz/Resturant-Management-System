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
using Microsoft.AspNet.Identity;
using System.IO;

namespace Online_Food_Ordering_System.Controllers
{
    
    public class ProductsController : Controller
    {
        private DBConnect db = new DBConnect();


        // GET: Products
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            //Session["u_id"] = 2;
            if (TempData["cart"] != null)
            {
                int x = 0;
                List<cart> li2 = TempData["cart"] as List<cart>;
                foreach (var item in li2)
                {
                    x += item.bill;
                }
                TempData["total"] = x;
            }
            TempData.Keep();
            var products = db.Products.Include(p => p.catClass);
            return View(products.ToList());
        }





        // GET: Products/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }





        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.catID = new SelectList(db.Categories, "catID", "catName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "prdID,prdName,prdPrice,prdImage,prdDescription,catID")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Products.Add(product);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.catID = new SelectList(db.Categories, "catID", "catName", product.catID);
        //    return View(product);
        //}






        [Authorize(Roles = "Admin")]
        public ActionResult Create(Product im)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileName(im.ImageFile.FileName);
                im.ImagePath = "~/images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/images"), fileName);
                im.ImageFile.SaveAs(fileName);
                db.Products.Add(im);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(im);
        }








        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tbl_img = db.Products.Find(id);
            Session["imgPath1"] = tbl_img.ImagePath;

            if (tbl_img == null)
            {
                return HttpNotFound();
            }
            ViewBag.catID = new SelectList(db.Categories, "catID", "catName", tbl_img.catID);
            return View(tbl_img);
        }




        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(HttpPostedFileBase ImageFile, Product im)
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
                    string oldImgPath = Request.MapPath(Session["imgPath1"].ToString());
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
                    im.ImagePath = Session["imgPath1"].ToString();
                    db.Entry(im).State = EntityState.Modified;
                    if (db.SaveChanges() > 0)
                    {
                        return RedirectToAction("Index");
                    }

                }
            }
            return View();
        }





        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }



        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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

       
        
        // Searching Foods from Home Page
        public ActionResult SearchFood(string search)
        {
            if (search == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var mod = db.Products.Where(prd => prd.prdName.Contains(search) || search == null).ToList();
            return View(mod);
        }



        [Authorize (Roles = "Admin")]
        // Searching food in Admin Page
        public ActionResult SearchCat(string search)
        {
            var dos = db.Products.Where(x => x.prdName.Contains(search) || search == null);
            return View(dos);
        }



        [Authorize]
        [HttpGet]
        public ActionResult addtoCard (int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }



        List<cart> li = new List<cart>();
        [HttpPost]
        [Authorize]
        public ActionResult addtoCard(Product pi, string qty, int id)
        {

            Product prd = db.Products.Find(id);

            cart c = new cart();
            c.prdID = prd.prdID;
            c.prdName = prd.prdName;
            c.price = prd.prdPrice;
            c.qty = Convert.ToInt32(qty);
            c.bill = c.price * c.qty;

            li.Add(c);

            if (TempData["cart"] == null)
            {
                TempData["cart"] = li;
                TempData.Keep();
            }
            else
            {

                List<cart> li2 = TempData["cart"] as List<cart>;
                int flag = 0;
                foreach (var item in li2)
                {
                    if (item.prdID == c.prdID)
                    {
                        item.qty += c.qty;
                        item.bill += c.bill;
                        flag = 1;
                    }

                }
                float h = 0;
                foreach (var item in li2)
                {
                    h += item.bill;
                }
                TempData["total"] = h;

                if (flag == 0)
                {
                    li2.Add(c);
                }
                TempData["cart"] = li2;
            }

            return RedirectToAction("Menu","Home");
        }


        [Authorize]
        public ActionResult checkout()
        {
            TempData.Keep();

            return View();
        }


        [HttpPost]
        [Authorize]
        public ActionResult checkout(OrderDetails ordD, string loc)
        {
            List<cart> li = TempData["cart"] as List<cart>;
            Order ord = new Order();
            //ord.custID = Convert.ToInt32(Session["u_id"].ToString());
            ord.custName = User.Identity.GetUserName();
            ord.ordDate = System.DateTime.Now;
            ord.ordLocation = loc;
            ord.bill = Convert.ToInt32(TempData["total"]);
            db.Orders.Add(ord);
           
            db.SaveChanges();
            foreach (var item in li)
            {
                OrderDetails odd = new OrderDetails();
                odd.prdID = item.prdID;
                odd.ordID = ord.ordID;
                odd.Qty = item.qty;
                odd.Price = (int)item.price;
                db.OrderDetails.Add(odd);
                db.SaveChanges();
            }


            TempData.Remove("total");
            TempData.Remove("cart");

            TempData["msg"] = "Transaction Completed Order NO: " + ord.ordID;

            return RedirectToAction("Index","Home");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult remove(int? id)
        {
            List<cart> li2 = TempData["cart"] as List<cart>;
            cart c = li2.Where(x => x.prdID == id).SingleOrDefault();
            li2.Remove(c);
            float h = 0;
            foreach (var item in li2)
            {
                h += item.bill;
            }
            TempData["total"] = h;

            if (h == 0)
            {
                TempData.Remove("total");
                TempData.Remove("cart");
            }
            return RedirectToAction("checkout");
        }


        
        //[Authorize(Roles = "Admin")]
        //public ActionResult Chart()
        //{
        //    var data = db.OrderDetails.GroupBy(x => new { prd = x.prdID }).Select(a=> new { pri = a.Sum(f=>f.Price) }).ToList();
        //    ViewBag.d = data;
        //    // var order = db.OrderDetails.GroupBy(d => d.prdID).Select(g => new { key = g.Select(y => y.prdID), price = g.Sum(s => s.Price) }).ToList();
        //    //var order = db.OrderDetails.GroupBy(d => d.prdID).ToList();
        //    return View(data);
        //   // return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        //}

    }
}
