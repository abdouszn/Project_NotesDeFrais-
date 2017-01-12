using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Project_NotesDeFrais.Models.Reposirery
{
    public class PolesRepository
    {

        NotesDeFraisEntities e;
        public PolesRepository()
        {
            e = new NotesDeFraisEntities();

        }

        public List<Employees> getAllManager() {
            using (e)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var userIds = roleManager.FindByName("Manager").Users.Select(e => e.UserId).ToList();
                var managers = userManager.Users.Where(e => userIds.Contains(e.Id));
                List<Employees> employerManager = new List<Employees>();
                foreach (var manage in managers)
                {
                    Employees user = (from man in e.Employees where man.User_ID == manage.Id select man).FirstOrDefault();
                    employerManager.Add(user);
                }
                return employerManager;
            }
        }
        public void AddPoles(Poles pole)
        {
            using (e)
            {
                e.Poles.Add(pole);
                e.SaveChanges();
            }
        }

        public void updatePole(Poles poles , String name)
        {
            using (e)
            {
                poles.Name = name;
                e.SaveChanges();
            }
        }

        public IQueryable<Poles> allPoles()
        {
            using (e)
            {
                var poles = e.Poles.OrderBy(r => r.Pole_ID);
                return poles;
            }
        }


        public IQueryable<Poles> getSerachingPoles(String query)
        {
            using (e)
            {
                var poles = (from s in e.Poles where s.Name.Contains(query) select s).OrderBy(r => r.Pole_ID);
                return poles;
            }
        }


        public Poles GetById(Guid id)
        {
            using (e)
            {
                Poles pole = (from p in e.Poles where p.Pole_ID == id select p).FirstOrDefault();
                return pole;
            }
        }

        public Poles GetByIdManager(Guid id)
        {
            using (e)
            {

                Poles pole = (from p in e.Poles where p.Manager_ID == id select p).FirstOrDefault();
                return pole;
            }
        }

        public Guid maxIdEmployer() {
            using (e)
            {
                var id = (from s in e.Employees select s.Employee_ID).FirstOrDefault();
                return id;
            }
            
        }
       

        public void Delete(Poles p)
        {
            using (e)
            {
                e.Poles.Remove(p);
            }
        }


        public void Save()
        {
            try
            {
                e.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    // Get entry

                    DbEntityEntry entry = item.Entry;
                    string entityTypeName = entry.Entity.GetType().Name;

                    // Display or log error messages

                    foreach (DbValidationError subItem in item.ValidationErrors)
                    {
                        string message = string.Format("Error '{0}' occurred in {1} at {2}",
                                 subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                        Console.WriteLine(message);
                    }
                }
            }
        }
    }
}