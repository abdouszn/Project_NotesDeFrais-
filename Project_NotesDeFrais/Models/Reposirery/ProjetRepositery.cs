using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Project_NotesDeFrais.Models.Reposirery
{
    public class ProjetRepositery
    {
        NotesDeFraisEntities e;
        public ProjetRepositery()
        {
            e = new NotesDeFraisEntities();

        }

        public void AddProjet(Projects projet)
        {
            using (new NotesDeFraisEntities())
            {
                e.Projects.Add(projet);
                e.SaveChanges();

            }


        }

        public IQueryable<Projects> allProjects()
        {
            var projects = e.Projects.OrderBy(r => r.Project_ID);
            return projects;
        }


        public IQueryable<Projects> getSerachingProjects(String query)
        {
            using (new NotesDeFraisEntities())
            {
                var projects = (from s in e.Projects where s.Name.Contains(query) select s).OrderBy(r => r.Project_ID);
                return projects;
            }
        }


        public Projects GetById(Guid id)
        {
            using (new NotesDeFraisEntities())
            {
                Projects projet = (from p in e.Projects where p.Project_ID == id select p).FirstOrDefault();
                return projet;
            }
        }

        public Customers GetByIdCutomer(Guid id)
        {
            Customers customer = (from p in e.Customers where p.Customer_ID == id select p).FirstOrDefault();
            return customer;
        }

        public IQueryable<Projects>  GetProjectsByIdCutomer(Guid id)
        {
            var  projets = (from p in e.Projects where p.Customer_ID == id select p);
            return projets;
        }

        public Guid GetIdByName(String name)
        {
            Guid projectId = (from c in e.Projects where c.Name == name select c.Project_ID).FirstOrDefault();
            return projectId;
        }

        /* public List<SimulationModel> AllByUser(String user) {
         var simulations = (from s in e.Simulations where s.NomClient == user select s);
           return (List < SimulationModel >) simulations;
    }*/


        /* public List<SimulationModel> AllByUserAboveToTreshold(String user , int Capital){
             var simulations = (from s in e.Simulations where (s.NomClient == user && s.Capital>Capital)  select s);
             return (List<SimulationModel>)simulations;
         }*/


        public void Delete(Projects p)
        {

            /*var ProjectCostumer = (from pr in e.Projects where (p. == c.Customer_ID) select p);
            foreach (var prc in ProjectCostumer.ToList())
            {
                e.Projects.Remove(prc);
                e.SaveChanges();
            }*/

            e.Projects.Remove(p);
            Save();
        }

        public Guid maxIdPoles()
        {
            var id = (from s in e.Poles select s.Pole_ID).FirstOrDefault();
            return id;

        }

        public Guid maxIdCustomers()
        {
            var id = (from s in e.Customers select s.Customer_ID).FirstOrDefault();
            return id;

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