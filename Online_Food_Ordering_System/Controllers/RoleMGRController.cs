using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Online_Food_Ordering_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Online_Food_Ordering_System.Controllers
{
   [Authorize( Roles = "Admin")]
    public class RoleMGRController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        private readonly RoleManager<IdentityRole> rollemanager;

        // GET: Roll
        [Authorize(Roles = "Admin")]

        public ActionResult RoleList()
        {
            var rolelist = _db.Roles.ToList();
            return View(rolelist);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult CreateRole()
        {
            var role = new IdentityRole();
            return View(role);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateRole(IdentityRole identity)
        {
            _db.Roles.Add(identity);
            _db.SaveChanges();
            return RedirectToAction("RoleList");
        }

        //public ActionResult Delete(I id,IdentityRole DS)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    IdentityRole identity = _db.Roles.Find(id);
        //    if (identity == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    _db.Roles.Remove(identity);
        //    _db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

       [Authorize(Roles ="Admin")]
        public ActionResult DeleteRole (string id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }

            var role =  rollemanager.FindById(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            else
            {
                var result =  rollemanager.Delete(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }

            }
            return View();
        }

    }
}