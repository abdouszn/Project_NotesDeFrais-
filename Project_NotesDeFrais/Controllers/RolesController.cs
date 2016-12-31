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
    [Authorize(Roles = "Comptable , Admin")]
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
            return View("CreateRoles");
        }

        public ActionResult createRole( )
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            String nameRole= Convert.ToString(Request.Form["roleSelected"]);
            if (!roleManager.RoleExists(nameRole))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = nameRole;
                roleManager.Create(role);
            }
            return RedirectToAction("allRoles");
        }

        public AspNetRoles getRole(String name)
        {
            RolesRepositery rolesRep = new RolesRepositery();
            return rolesRep.getRole(name);

        }

        public ActionResult allRoles(int? pageIndex) {
            RolesRepositery rolRep = new RolesRepositery();
            var listRole = rolRep.allRoles();
            var countElementPage = 10;
            PaginatedList<AspNetRoles> lst = new PaginatedList<AspNetRoles>(listRole, pageIndex, countElementPage);
            return View("AllRoles", lst);
        }

        public ActionResult RolesUsers() {
           
            RolesRepositery rolRep = new RolesRepositery();
            EmployesRepositery empRp = new EmployesRepositery(); 
            var listRole = rolRep.allRoles();
            if (listRole.Count() == 0)
            {
                ViewData["erreur"] = "Roles";
                return View("ErrorEmptyElement");
            }
            foreach (var rl in listRole) {
                rl.AspNetUsersList= empRp.getAllUsers().ToList();
            }
            return View("AddUserRole", listRole.ToList());

        }

        [Authorize]
        public ActionResult addRoleToUser() {
            ApplicationDbContext context = new ApplicationDbContext();
            RolesRepositery rolRep = new RolesRepositery();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            String nameRole = Convert.ToString(Request.Form["roleSelected"]);
            String idUser = Convert.ToString(Request.Form["userSelected"]);
            var result1 = UserManager.AddToRole(idUser, nameRole);
            ViewBag.ResultMessage = "Role created successfully !";
            return RedirectToAction("RolesUsers");
        }
    }
}