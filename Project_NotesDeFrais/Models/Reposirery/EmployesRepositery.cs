﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Project_NotesDeFrais.Models.Reposirery
{
    public class EmployesRepositery
    {
        NotesDeFraisEntities e;
        public EmployesRepositery()
        {
            e = new NotesDeFraisEntities();

        }

        public void AddEmployes(Employees emp)
        {
            using (new NotesDeFraisEntities())
            {
                e.Employees.Add(emp);
                e.SaveChanges();
            }
        }

        public IQueryable<Employees> allEmployees()
        {
            using (new NotesDeFraisEntities())
            {
                var employees = e.Employees.OrderBy(r => r.Employee_ID);
                return employees;
            }
        }



        public IQueryable<Employees> getSerachingemployees(String query)
        {
            using (new NotesDeFraisEntities())
            {
                var employer = (from s in e.Employees where s.FirstName.Contains(query) select s).OrderBy(r => r.Employee_ID);
                return employer;
            }
        }


        public Employees GetByIdUser(String id)
        {
            using (new NotesDeFraisEntities())
            {
                Employees employer = (from e in e.Employees where e.User_ID == id select e).FirstOrDefault();
                return employer;
            }
        }

        public IQueryable<AspNetUsers> getAllUsers() {
            var Users = e.AspNetUsers.OrderBy(r => r.Id);
            return Users;
        }

        public String getUserByMail(String email) {
            String userId = (from e in e.AspNetUsers where e.Email == email select e.Id).FirstOrDefault();
            return userId;
        }


        public IQueryable<Poles> getAllPoles()
        {
            var poles = e.Poles.OrderBy(r => r.Pole_ID);
            return poles;
        }

        public Guid getPoleByName(String name)
        {
            Guid poleId = (from e in e.Poles where e.Name == name select e.Pole_ID).FirstOrDefault();
            return poleId;
        }

        public void Delete(ExpanseReports expRep)
        {
            using (new NotesDeFraisEntities())
            {
                e.ExpanseReports.Remove(expRep);
            }
        }

        public String maxIdUser()
        {
            using (new NotesDeFraisEntities())
            {
                var id = (from s in e.AspNetUsers select s.Id).FirstOrDefault();
                return id;
            }

        }

        public AspNetUsers getUserById(String id)
        {
            using (new NotesDeFraisEntities())
            {
                AspNetUsers userId = (from u in e.AspNetUsers where u.Id==id select u).FirstOrDefault();
                return userId;
            }

        }

        public Guid maxIdPoles()
        {
            using (new NotesDeFraisEntities())
            {
                var id = (from s in e.Poles select s.Pole_ID).FirstOrDefault();
                return id;
            }

        }

        public Poles getPoleById(Guid? id)
        {
            using (new NotesDeFraisEntities())
            {
                Poles pole = (from p in e.Poles where p.Pole_ID == id select p).FirstOrDefault();
                return pole;
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