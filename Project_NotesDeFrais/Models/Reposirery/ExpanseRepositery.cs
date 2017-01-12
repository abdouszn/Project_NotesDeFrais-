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
            using (e)
            {
                e.Expanses.Add(exp);
                e.SaveChanges();
            }
        }

        public void updateExpanses(Expanses exp , double ttc, double ht, double tva)
        {
            using (e)
            {
                exp.Amount_TTC = ttc;
                exp.Amount_HT = ht;
                exp.Amount_TVA = tva;
                e.SaveChanges();
            }
        }
        public IQueryable<Expanses> allExpanses()
        {
            using (e)
            {
                var expanses = e.Expanses.OrderBy(r => r.Expanse_ID);
                return expanses;
            }
        }
        public IQueryable<Expanses> getSerachingExpanses(String query , Guid id)
        {
            using (e)
            {
                var expanseReport = (from s in e.Expanses where s.Customers.Name.Contains(query) && s.ExpanseReport_ID == id select s).OrderBy(r => r.Day);
                return expanseReport;
            }
        }


        public Expanses GetById(Guid id)
        {
            using (e)
            {
                Expanses expanse = (from ex in e.Expanses where ex.Expanse_ID == id select ex).FirstOrDefault();
                return expanse;
            }
        }

       

        public void Delete(Expanses exp)
        {
            using (e)
            {
                e.Expanses.Remove(exp);
               
            }
        }

        public Guid maxIdCustomers()
        {
            using (e)
            {
                var id = (from s in e.Customers select s.Customer_ID).FirstOrDefault();
                return id;
            }

        }
        public Guid maxIdProject()
        {
            using (e)
            {
                var id = (from s in e.Projects select s.Project_ID).FirstOrDefault();
                return id;
            }

        }
        public Guid maxIdExpanseType()
        {
            using (e)
            {
                var id = (from s in e.ExpanseTypes select s.ExpenseType_ID).FirstOrDefault();
                return id;
            }

        }
        public Guid maxIdExpanseReports()
        {
            using (e)
            {
                var id = (from s in e.ExpanseReports select s.ExpanseReport_ID).FirstOrDefault();
                return id;
            }

        }

        public Customers GetByIdCutomer(Guid id)
        {
            using (e)
            {
                Customers customer = (from p in e.Customers where p.Customer_ID == id select p).FirstOrDefault();
                return customer;
            }
        }

        public Projects GetByIdProjects(Guid id)
        {
            using (e)
            {
                Projects projet = (from p in e.Projects where p.Project_ID == id select p).FirstOrDefault();
                return projet;
            }
        }

        public ExpanseReports GetByIdExpansesRepport(Guid id)
        {
            using (e)
            {
                ExpanseReports expanseRepport = (from p in e.ExpanseReports where p.ExpanseReport_ID == id select p).FirstOrDefault();
                return expanseRepport;
            }
        }

        public IQueryable<Expanses> GetByProject(Guid id)
        {
            using (e)
            {
                IQueryable<Expanses> expanse = (from p in e.Expanses where p.Project_ID == id select p);
                return expanse;
            }
        }

        public IQueryable<Expanses> GetAllByIdExpansesRepport(Guid id)
        {
            using (e)
            {
                var expanse = (from p in e.Expanses where p.ExpanseReport_ID == id select p);
                return expanse;
            }
        }

        public ExpanseTypes GetByIdExpanseTypes(Guid id)
        {
            using (e)
            {
                ExpanseTypes expanseType = (from p in e.ExpanseTypes where p.ExpenseType_ID == id select p).FirstOrDefault();
                return expanseType;
            }
        }

        public IQueryable<Expanses> GetExpansesByIdExpanseTypes(Guid id)
        {
            using (e)
            {
                IQueryable<Expanses> expanses = (from p in e.Expanses where p.ExpanseType_ID == id select p);
                return expanses;
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