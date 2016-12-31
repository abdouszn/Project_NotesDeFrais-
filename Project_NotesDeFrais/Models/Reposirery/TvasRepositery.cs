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

        public void updateTvas(Tvas tva, String name, double value) {
            tva.Name = name;
            tva.Value = value;
            e.SaveChanges();
        }
        public IQueryable<Tvas> allTvas()
        {
            var tvas = e.Tvas.OrderBy(r => r.TVA_ID);

            return tvas;
        }

        public Tvas tvasById(Guid Id)
        {
            var tvas = (from t in e.Tvas where t.TVA_ID == Id select t).FirstOrDefault(); ;

            return tvas;
        }

        public IQueryable<Tvas> getSerachingTvas(String query)
        {
            using (new NotesDeFraisEntities())
            {
                var tvas = (from s in e.Tvas where s.Name.Contains(query) select s).OrderBy(r => r.TVA_ID);
                return tvas;
            }
        }

        public void delete(Tvas tva) {
            using (new NotesDeFraisEntities())
            {
                e.Tvas.Remove(tva);
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