using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Project_NotesDeFrais.Models.Reposirery
{
    public class TvasRepositery
    {

        NotesDeFraisEntities e;
        public TvasRepositery()
        {
            e = new NotesDeFraisEntities();

        }

        public void AddTva(Tvas tva)
        {
            using (new NotesDeFraisEntities())
            {
                e.Tvas.Add(tva);
                e.SaveChanges();

            }


        }

        public IQueryable<Tvas> allTvas()
        {
            var tvas = e.Tvas.OrderBy(r => r.TVA_ID);

            return tvas;
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