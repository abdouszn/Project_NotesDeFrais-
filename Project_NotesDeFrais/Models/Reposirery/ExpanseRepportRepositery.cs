using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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

        public void updateExpanseReports(ExpanseReports expRep, double ttc, double ht, double tva) {
            expRep.Total_TTC = ttc;
            expRep.Total_HT = ht;
            expRep.Total_TVA = tva;
            Save();
        }

        public IQueryable<ExpanseReports> allExpanseReports(Guid idEmployer)
        {
            var expansesRepport = (from s in e.ExpanseReports where s.Employee_ID== idEmployer select s).OrderBy(r => r.CreationDate.Month);
            return expansesRepport;
        }

        public IQueryable<ExpanseReports> allExpanseReportsToValid()
        {
            var expansesRepport = (from s in e.ExpanseReports where s.StatusCode == 10 select s).OrderBy(r => r.CreationDate.Month);
            return expansesRepport;
        }

        public IQueryable<ExpanseReports> allExpanseReportsToValidComptable()
        {
            var expansesRepport = (from s in e.ExpanseReports where (s.StatusCode == 10 || s.StatusCode == 20) select s).OrderBy(r => r.CreationDate.Month);
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

        

        public void Delete(ExpanseReports expRep)
        {
            using (new NotesDeFraisEntities())
            {
                var expanse = (from exp in e.Expanses where (exp.ExpanseReport_ID == expRep.ExpanseReport_ID) select exp);
                foreach (var exp in expanse.ToList())
                {
                    e.Expanses.Remove(exp);

                }

                e.ExpanseReports.Remove(expRep);
            }
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

        public IQueryable<ExpanseReports> GetByEmployesId(Guid idEmployer) {
            IQueryable<ExpanseReports>  expanseRepport = (from ex in e.ExpanseReports where ex.Employee_ID == idEmployer select ex);
            return expanseRepport;
        }
        public void updateStatus(ExpanseReports expRep, int statut , String managerComment, String comptableComment) {
            using (new NotesDeFraisEntities())
            {
                expRep.StatusCode = statut;
                expRep.ManagerComment = managerComment;
                expRep.AccountingComment = comptableComment;
                e.SaveChanges();
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