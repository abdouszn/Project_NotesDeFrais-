using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Project_NotesDeFrais.Models;
using Project_NotesDeFrais.Models.NoteDeFraisModel;
using Project_NotesDeFrais.Models.Reposirery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_NotesDeFrais.Controllers
{
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {
                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
                //Here we create a Admin super user who will maintain the website                  
                var user = new ApplicationUser();
                user.UserName = "admin";
                user.Email = "admin@admin.com";
                string userPWD = "@zerty2016";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            return View("CreateRoles");
        }

        public ActionResult createRole( AspNetRoles role)

        { 
            RolesRepositery rolesRep = new RolesRepositery();
            role.Id = Convert.ToString(Request.Form["roleSelected"]);
            role.Name = Convert.ToString(Request.Form["roleSelected"]);
            rolesRep.addRoles(role);
            return RedirectToAction("Index");
        }

        public AspNetRoles getRole(String name)
        {
            RolesRepositery rolesRep = new RolesRepositery();
            return rolesRep.getRole(name);

        }
    }
}