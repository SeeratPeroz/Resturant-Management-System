using Online_Food_Ordering_System.Models;
using Online_Food_Ordering_System.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Online_Food_Ordering_System.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        DBConnect Db = new DBConnect();
        public ActionResult Index()
        {

            // creating list of model to pass in view
            //List<Category> catList = new List<Category>();
            //List<FoodOffer> offerList = new List<FoodOffer>();

            //HomeModelll HM = new HomeModelll();
            //HM.catModel = catList; 
            //HM.offerModel = offerList;
            var catList = Db.Categories.ToList();
            //return View(HM);
            var offerList = Db.FoodOffers.ToList();
            ViewData["catModel"] = catList;
            ViewData["offerModel"] = offerList;
            return View();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return RedirectToAction("About","Home");
        }

        public ActionResult OurTeam()
        {
            return View();
        }

        [Authorize]

        // Displaying Category items FOOD Menu
        public ActionResult MenuItems(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            var mod = Db.Products.Where(x => x.catID == id || x.catID == 0).ToList();
            return View(mod);
        }


        public ActionResult Menu()
        {
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
            return View(Db.Categories.ToList());
        }
    }
}