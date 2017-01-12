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
            using (e)
            {
                e.ExpanseTypes.Add(expanseType);
                e.SaveChanges();
            }
        }

        public void updateExpanseType(ExpanseTypes expType, String name, double celling, Boolean fix, Boolean only) {
            using (e)
            {
                expType.Name = name;
                expType.Ceiling = celling;
                expType.Fixed = fix;
                expType.OnlyManagers = only;
                e.SaveChanges();
            }
        }

        public IQueryable<ExpanseTypes> allExpanseTypes()
        {
            using (e)
            {
                var expanseTypes = e.ExpanseTypes.OrderBy(r => r.ExpenseType_ID);
                return expanseTypes;
            }
        }

        public IQueryable<ExpanseTypes> allExpanseTypesManager()
        {
            using (e)
            {
                var  expanseTypes = (from s in e.ExpanseTypes where s.OnlyManagers==true select s).OrderBy(r => r.ExpenseType_ID);
                return expanseTypes;
            }
        }
        public IQueryable<ExpanseTypes> getSerachingExpanses(String query)
        {
            using (e)
            {
                var expanseType = (from s in e.ExpanseTypes where s.Name.Contains(query) select s).OrderBy(r => r.ExpenseType_ID);
                return expanseType;
            }
        }

        public Guid maxIdTva()
        {
            using (e)
            {
                var id = (from s in e.Tvas select s.TVA_ID).FirstOrDefault();
                return id;
            }

        }

        public Guid GetIdByName(String name)
        {
            using (e)
            {
                Guid typeId = (from c in e.ExpanseTypes where c.Name == name select c.ExpenseType_ID).FirstOrDefault();
                return typeId;
            }
        }

        public ExpanseTypes getById(Guid id)
        {
            using (e)
            {
                ExpanseTypes expType = (from c in e.ExpanseTypes where c.ExpenseType_ID == id select c).FirstOrDefault();
                return expType;
            }
        }

        public  IQueryable<ExpanseTypes> getByTvaId(Guid tvaId) {
            using (e)
            {
                IQueryable<ExpanseTypes> expType = (from c in e.ExpanseTypes where c.Tva_ID == tvaId select c);
                return expType;
            }
        }
        public void delete(ExpanseTypes expType) {
            using (e)
            {
                e.ExpanseTypes.Remove(expType);
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