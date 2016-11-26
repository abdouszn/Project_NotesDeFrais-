﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Project_NotesDeFrais.Models
{
    public class NotesDeFraisRepositery : DbContext
    {
        NotesDeFraisEntities e;
        public NotesDeFraisRepositery()
        {
            e = new NotesDeFraisEntities();


        }

        public IQueryable<ProjectsModel> all()
        {
            using (var e = new NotesDeFraisEntities())
            {
                var project = (IQueryable<ProjectsModel>)e.Projects.ToList();
                return project;
            }
            
        }

        public void Add( Employees epl)
        {
            using (new NotesDeFraisEntities())
            {
                e.Employees.Add(epl);
                e.SaveChanges();

            }
               
               
        }

        public IQueryable<Employees> allEmployers()
        {
            var employers = e.Employees.OrderBy(r => r.Employee_ID);
            return employers;
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


    