using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Project_NotesDeFrais.Models.Reposirery
{
    public class ExpanseTypesRepositery
    {

        NotesDeFraisEntities e;
        public ExpanseTypesRepositery()
        {
            e = new NotesDeFraisEntities();

        }

        public void AddExpanseType(ExpanseTypes expanseType)
        {
            using (new NotesDeFraisEntities())
            {
                e.ExpanseTypes.Add(expanseType);
                e.SaveChanges();
            }
        }

        public IQueryable<ExpanseTypes> allExpanseTypes()
        {
            var expanseTypes = e.ExpanseTypes.OrderBy(r => r.ExpenseType_ID);
            return expanseTypes;
        }

        public IQueryable<ExpanseTypes> getSerachingExpanses(String query)
        {
            var expanseType = (from s in e.ExpanseTypes where s.Name.Contains(query) select s).OrderBy(r => r.ExpenseType_ID);
            return expanseType;
        }

        public Guid maxIdTva()
        {
            var id = (from s in e.Tvas select s.TVA_ID).FirstOrDefault();
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