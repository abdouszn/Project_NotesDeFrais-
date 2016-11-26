﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Project_NotesDeFrais.Models.Reposirery
{
    public class PolesRepository
    {

        NotesDeFraisEntities e;
        public PolesRepository()
        {
            e = new NotesDeFraisEntities();

        }

        public void AddPoles(Poles pole)
        {
            using (new NotesDeFraisEntities())
            {
                e.Poles.Add(pole);
                e.SaveChanges();

            }


        }

        public IQueryable<Poles> allPoles()
        {
            var poles = e.Poles.OrderBy(r => r.Pole_ID);
            return poles;
        }


        public IQueryable<Poles> getSerachingPoles(String query)
        {
            var poles = (from s in e.Poles where s.Name.Contains(query) select s).OrderBy(r => r.Pole_ID);
            return poles;
        }


        public Poles GetById(Guid id)
        {
            Poles pole = (from p in e.Poles where p.Pole_ID == id select p).FirstOrDefault();
            return pole;
        }

        public Poles GetByIdManager(Guid id)
        {

            Poles pole = (from p in e.Poles where p.Manager_ID == id select p).FirstOrDefault();
            return pole;
        }

        public Guid maxIdEmployer() {
            var id = (from s in e.Employees select s.Employee_ID).FirstOrDefault();
            return id;
            
        }
        /* public List<SimulationModel> AllByUser(String user) {
         var simulations = (from s in e.Simulations where s.NomClient == user select s);
           return (List < SimulationModel >) simulations;
    }*/


        /* public List<SimulationModel> AllByUserAboveToTreshold(String user , int Capital){
             var simulations = (from s in e.Simulations where (s.NomClient == user && s.Capital>Capital)  select s);
             return (List<SimulationModel>)simulations;
         }*/


        public void Delete(Poles p)
        {

            /*var ProjectCostumer = (from pr in e.Projects where (p. == c.Customer_ID) select p);
            foreach (var prc in ProjectCostumer.ToList())
            {
                e.Projects.Remove(prc);
                e.SaveChanges();
            }*/

            e.Poles.Remove(p);
            Save();
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