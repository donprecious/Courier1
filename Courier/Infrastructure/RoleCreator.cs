using Courier.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Courier.Infrastructure
{
    public class RoleCreator
    {
        private void createRoleDefaultUser1()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            // In Startup create a super admin   
            string superRole = "SuperAdmin";
            string regRole = "Admin";
            if (!roleManager.RoleExists("SuperAdmin"))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = superRole;
                roleManager.Create(role);


            }

            // creating Creating Create Reqular Admin  
            if (!roleManager.RoleExists(regRole))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = regRole;

                roleManager.Create(role);

            }
            //create and add user to role

            //Here we create a Admin super user who will maintain the website                  

            var user = new ApplicationUser();
            user.UserName = "Iyeritufu@gmail.com";
            user.Email = "Iyeritufu@gmail.com";
            string password = "precious0don";

            //find user{
            var findUser = UserManager.FindByEmail(user.Email);
            if (findUser == null)
            {
                //GO AHEAD AND cREATE THE USER
                var chkUser = UserManager.Create(user, password);
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, superRole);

                }
            }

            else
            {
                //GET USER id
                var userID = findUser.Id;
                var result1 = UserManager.AddToRole(userID, superRole);
            }

        }
    }

   
}