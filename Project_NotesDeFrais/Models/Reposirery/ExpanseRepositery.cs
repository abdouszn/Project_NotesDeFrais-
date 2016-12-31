using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Project_NotesDeFrais.Models.Reposirery
{
    public class ExpanseRepositery
    {

        NotesDeFraisEntities e;
        public ExpanseRepositery()
        {
            e = new NotesDeFraisEntities();

        }

        public void AddExpanses(Expanses exp)
        {
            using (new NotesDeFraisEntities())
            {
                e.Expanses.Add(exp);
                e.SaveChanges();
            }
        }

        public void updateExpanses(Expanses exp , double ttc, double ht, double tva)
        {
            using (new NotesDeFraisEntities())
            {
                exp.Amount_TTC = ttc;
                exp.Amount_HT = ht;
                exp.Amount_TVA = tva;
                e.SaveChanges();
            }
        }
        public IQueryable<Expanses> allExpanses()
        {
            var expanses = e.Expanses.OrderBy(r => r.Expanse_ID);
            return expanses;
        }
        public IQueryable<Expanses> getSerachingExpanses(String query)
        {
            var expanseReport = (from s in e.Expanses where s.Customers.Name.Contains(query) select s).OrderBy(r => r.Day);
            return expanseReport;
        }


        public Expanses GetById(Guid id)
        {
            Expanses expanse = (from ex in e.Expanses where ex.Expanse_ID == id select ex).FirstOrDefault();
            return expanse;
        }

       

        public void Delete(Expanses exp)
        {
            using (new NotesDeFraisEntities())
            {
                e.Expanses.Remove(exp);
               
            }
        }

        public Guid maxIdCustomers()
        {
            var id = (from s in e.Customers select s.Customer_ID).FirstOrDefault();
            return id;

        }
        public Guid maxIdProject()
        {
            var id = (from s in e.Projects select s.Project_ID).FirstOrDefault();
            return id;

        }
        public Guid maxIdExpanseType()
        {
            var id = (from s in e.ExpanseTypes select s.ExpenseType_ID).FirstOrDefault();
            return id;

        }
        public Guid maxIdExpanseReports()
        {
            var id = (from s in e.ExpanseReports select s.ExpanseReport_ID).FirstOrDefault();
            return id;

        }

        public Customers GetByIdCutomer(Guid id)
        {
            Customers customer = (from p in e.Customers where p.Customer_ID == id select p).FirstOrDefault();
            return customer;
        }

        public Projects GetByIdProjects(Guid id)
        {
            Projects projet = (from p in e.Projects where p.Project_ID == id select p).FirstOrDefault();
            return projet;
        }

        public ExpanseReports GetByIdExpansesRepport(Guid id)
        {
            ExpanseReports expanseRepport = (from p in e.ExpanseReports where p.ExpanseReport_ID == id select p).FirstOrDefault();
            return expanseRepport;
        }

        public IQueryable<Expanses> GetByProject(Guid id)
        {
            IQueryable<Expanses> expanse = (from p in e.Expanses where p.Project_ID == id select p);
            return expanse;
        }

        public IQueryable<Expanses> GetAllByIdExpansesRepport(Guid id)
        {
            var expanse = (from p in e.Expanses where p.ExpanseReport_ID == id select p);
            return expanse;
        }

        public ExpanseTypes GetByIdExpanseTypes(Guid id)
        {
            ExpanseTypes expanseType = (from p in e.ExpanseTypes where p.ExpenseType_ID == id select p).FirstOrDefault();
            return expanseType;
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