using Courier.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Courier.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class SuperAdminController : Controller
    {
        // GET: SuperAdmin
        string superRole = "SuperAdmin";
        string regRole = "Admin";
        ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult Index()
        {
            var da = new Courier.Models.DbModel.CourierEntities();
            var q = from p in da.Users select p;
           return View(q);
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        public ActionResult DeleteUser()
        {
            return View();
        }

        public ActionResult AddUserToAdminRole(string id)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var da = new Courier.Models.DbModel.CourierEntities();
            var findUser = da.Users.Find(id);

            if(findUser != null)
            {
                //check if role exit
                UserManager.AddToRole(findUser.Id, regRole);
                TempData["view"] = 1;
            }
            return RedirectToAction("Index");
        }

        public ActionResult RemoveUserFromRole()
        {
            return View();
        }
    }
}