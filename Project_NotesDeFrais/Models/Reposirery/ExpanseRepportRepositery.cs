﻿using System;
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

        public void updateExpanseReports(ExpanseReports expRep, double ttc, double ht, double tva) {
            expRep.Total_TTC = ttc;
            expRep.Total_HT = ht;
            expRep.Total_TVA = tva;
            Save();
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

        

        public void Delete(ExpanseReports expRep)
        {
            var expanse = (from exp in e.Expanses where (exp.ExpanseReport_ID == expRep.ExpanseReport_ID) select exp);
            foreach (var exp in expanse.ToList())
            {
                e.Expanses.Remove(exp);
                e.SaveChanges();
            }

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

        public void updateStatus(ExpanseReports expRep, int statut) {
            using (new NotesDeFraisEntities())
            {
                expRep.StatusCode = statut;
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