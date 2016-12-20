using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Project_NotesDeFrais.Models.Reposirery
{
    public class CustomerRepositery
    {
        NotesDeFraisEntities e;
        public CustomerRepositery()
        {
            e = new NotesDeFraisEntities();

        }

        public void AddCostumers(Customers cust)
        {
            using (new NotesDeFraisEntities())
            {
                e.Customers.Add(cust);
                e.SaveChanges();
            }
        }

        public void updateCustomers(Customers customer, String name, String code)
        {
            customer.Name = name;
            customer.Code = code;
            e.SaveChanges();
        }

        public IQueryable<Customers> allCustomers()
        {
            var customers = e.Customers.OrderBy(r => r.Customer_ID);
            return customers;
        }

        public IQueryable<Customers> getSerachingCustomers(String query)
        {
            var customer = (from s in e.Customers where s.Name.Contains(query) select s).OrderBy(r => r.Customer_ID);
            return customer;
        }


        public Customers GetById(Guid id)
        {
            Customers customer = (from c in e.Customers where c.Customer_ID == id select c).FirstOrDefault();
            return customer;
        }

        public Guid GetIdByName(String name)
        {
            Guid customerId = (from c in e.Customers where c.Name == name select c.Customer_ID).FirstOrDefault();
            return customerId;
        }

        public void Delete(Customers c)
        {

            var ProjectCostumer = (from p in e.Projects where (p.Customer_ID == c.Customer_ID) select p);
            foreach (var prc in ProjectCostumer.ToList())
            {
                e.Projects.Remove(prc);
                e.SaveChanges();
            }
            e.Customers.Remove(c);
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