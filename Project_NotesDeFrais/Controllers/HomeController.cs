using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Project_NotesDeFrais.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_NotesDeFrais.Controllers
{
    public class HomeController : Controller
    {
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
                user.UserName = "admin@admin.com";
                user.Email = "admin@admin.com";
                string userPWD = "@AZERTY12";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }
            if (!roleManager.RoleExists("Comptable"))
            {
                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Comptable";
                roleManager.Create(role);
                //Here we create a Admin super user who will maintain the website                  
                var user = new ApplicationUser();
                user.UserName = "comptable@comptable.com";
                user.Email = "comptable@comptable.com";
                string userPWD = "@COMPTABLE12";
                var chkUser = UserManager.Create(user, userPWD);
                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Comptable");
                }
            }
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

            return View();
        }
    }
}