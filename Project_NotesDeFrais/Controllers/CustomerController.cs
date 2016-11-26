using Project_NotesDeFrais.Models;
using Project_NotesDeFrais.Models.Reposirery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_NotesDeFrais.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View("CostumerFormulaire");
        }

        public void createCustomer(Customers customer) {
            CustomerRepositery costRep = new CustomerRepositery();
            customer.Customer_ID = Guid.NewGuid();
            customer.Name= Convert.ToString(Request.Form["Name"]);
            customer.Code= Convert.ToString(Request.Form["Code"]);
            costRep.AddCostumers(customer);
        }

        public ActionResult AllCustomer(int? pageIndex)
        {
            CustomerRepositery costRep = new CustomerRepositery();

            var countElementPage = 10;
            var costumers = costRep.allCustomers();
            List<CustomersModel> customersModel = new List<CustomersModel>();
            
            foreach (var cust in costumers)
            {
                CustomersModel custModel = new CustomersModel();
                custModel.Customer_ID = cust.Customer_ID;
                custModel.Code = cust.Code;
                custModel.Name = cust.Name;
                customersModel.Add(custModel);
            }
            IQueryable<CustomersModel> listCust = customersModel.AsQueryable();
            PaginatedList<CustomersModel> lst = new PaginatedList<CustomersModel>(listCust, pageIndex, countElementPage);
            return View("AllCustomers" , lst);
        }

        public ActionResult Searche(String query, int? pageIndex)
        {
            var countElementPage = 10;
            CustomerRepositery costRep = new CustomerRepositery();
            var customers = costRep.getSerachingCustomers(query);
            List<CustomersModel> customersModel = new List<CustomersModel>();

            foreach (var cust in customers)
            {
                CustomersModel custModel = new CustomersModel();
                custModel.Customer_ID = cust.Customer_ID;
                custModel.Code = cust.Code;
                custModel.Name = cust.Name;
                customersModel.Add(custModel);
            }
            IQueryable<CustomersModel> listCust = customersModel.AsQueryable();
            PaginatedList<CustomersModel> lst = new PaginatedList<CustomersModel>(listCust, pageIndex, countElementPage);
            return View("AllCustomers", lst);
        }

    }
}