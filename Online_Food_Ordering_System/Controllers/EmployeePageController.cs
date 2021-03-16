using Microsoft.AspNet.Identity;
using Online_Food_Ordering_System.Models;
using Online_Food_Ordering_System.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Online_Food_Ordering_System.Controllers
{
    [Authorize]
    public class EmployeePageController : Controller
    {
        DBConnect db = new DBConnect();
        // GET: Employee View
        [Authorize (Roles ="Admin, Employee")]
        public ActionResult EmployeeIndex()
        {
           
            return View(db.Orders.Where(x => x.paid.Equals(false)).ToList());

        }


        [Authorize(Roles = "Admin, Employee")]
        public ActionResult Details(int? id)
        {
            TempData["FU"] = id;
            return View(db.OrderDetails.Where(x => x.ordID ==id || id == null).ToList());
        }

        [Log]

        [Authorize(Roles = "Admin, Employee")]
        public ActionResult Dispatch (int? id)
        {
            if (ModelState.IsValid)
            {
                Order ordResult = db.Orders.Find(id);
                ordResult.paid = true;
                ordResult.empName = User.Identity.GetUserName();
                db.Entry(ordResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EmployeeIndex");
            }
            return RedirectToAction("EmployeeIndex");
        }



        // Admin Views
        [Authorize (Roles = "Admin")]
        public ActionResult AdminACC()
        {
            return View(db.Categories.ToList());
        }

    }
}