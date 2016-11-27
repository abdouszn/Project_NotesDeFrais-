using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Project_NotesDeFrais.Models.Reposirery
{
    public class ExpanseRepportRepositery
    {

        NotesDeFraisEntities e;
        public ExpanseRepportRepositery()
        {
            e = new NotesDeFraisEntities();

        }

        public void AddExpansesReports(ExpanseReports exp)
        {
            using (new NotesDeFraisEntities())
            {
                e.ExpanseReports.Add(exp);
                e.SaveChanges();
            }
        }

        public IQueryable<ExpanseReports> allExpanseReports()
        {
            var expansesRepport = e.ExpanseReports.OrderBy(r => r.ExpanseReport_ID);
            return expansesRepport;
        }

      

        public IQueryable<ExpanseReports> getSerachingExpanseReports(String query)
        {
            var expanseReport = (from s in e.ExpanseReports where s.Employees.FirstName.Contains(query) select s).OrderBy(r => r.ExpanseReport_ID);
            return expanseReport;
        }


        public ExpanseReports GetById(Guid id)
        {
            ExpanseReports expanseRepport = (from ex in e.ExpanseReports where ex.ExpanseReport_ID == id select ex).FirstOrDefault();
            return expanseRepport;
        }

        /* public List<SimulationModel> AllByUser(String user) {
         var simulations = (from s in e.Simulations where s.NomClient == user select s);
           return (List < SimulationModel >) simulations;
    }*/


        /* public List<SimulationModel> AllByUserAboveToTreshold(String user , int Capital){
             var simulations = (from s in e.Simulations where (s.NomClient == user && s.Capital>Capital)  select s);
             return (List<SimulationModel>)simulations;
         }*/


        public void Delete(ExpanseReports expRep)
        {

            e.ExpanseReports.Remove(expRep);
        }

        public Guid maxIdEmployee()
        {
            var id = (from s in e.Employees select s.Employee_ID).FirstOrDefault();
            return id;

        }

        public Employees GetByIdEmployes(Guid id)
        {
            Employees employer = (from p in e.Employees where p.Employee_ID == id select p).FirstOrDefault();
            return employer;
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