using Project_NotesDeFrais.Models;
using Project_NotesDeFrais.Models.Reposirery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_NotesDeFrais.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View("CostumerFormulaire");
        }

        public ActionResult createCustomer(CustomersModel customerModel) {
            if (!ModelState.IsValidField("Name") || !ModelState.IsValidField("Code")) {
                return View("CostumerFormulaire", customerModel);
            }
            Customers customer = new Customers();
            CustomerRepositery costRep = new CustomerRepositery();
            customer.Customer_ID = Guid.NewGuid();
            customer.Name= Convert.ToString(Request.Form["Name"]);
            customer.Code= Convert.ToString(Request.Form["Code"]);
            costRep.AddCostumers(customer);
            return RedirectToAction("AllCustomer");
        }

        public ActionResult Edit(Guid id)
        {
            CustomerRepositery custRep = new CustomerRepositery();
            CustomersModel custModel = new CustomersModel();
            Customers customer = custRep.GetById(id);
            custModel.Code = customer.Code;
            custModel.Customer_ID = customer.Customer_ID;
            custModel.Name = customer.Name;
            return View("EditCustomer", custModel);
        }

        public ActionResult updateCustomers(Guid id)
        {
            CustomerRepositery custRep = new CustomerRepositery();
            Customers customer = custRep.GetById(id);
            if (!ModelState.IsValidField("Name") || !ModelState.IsValidField("Code"))
            {
                CustomersModel custModel = new CustomersModel();
                custModel.Code = customer.Code;
                custModel.Customer_ID = customer.Customer_ID;
                custModel.Name = customer.Name;
                return View("EditCustomer", custModel);
            }
            String name = Convert.ToString(Request.Form["Name"]);
            String code= Convert.ToString(Request.Form["Code"]);
            
            custRep.updateCustomers(customer, name , code);
            return RedirectToAction("AllCustomer");
        }

        public ActionResult AllCustomer(int? pageIndex)
        {
            CustomerRepositery costRep = new CustomerRepositery();

            var countElementPage = 10;
            var costumers = costRep.allCustomers();

            if (costumers.Count() == 0)
            {
                ViewData["erreurMessage"] = "aucun customer !";
                ViewData["element"] = "Customer";
                ViewData["create"] = "true";
                return View("ErrorEmptyList");
            }
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

        public ActionResult Delete(Guid id) {
            ProjetController prjtControleur = new ProjetController();
            CustomerRepositery cutoRepo = new CustomerRepositery();
            Customers cutomer = cutoRepo.GetById(id);
            ProjetRepositery prjtRepo = new ProjetRepositery();
            List<Projects> projets = prjtRepo.GetByCustomerId(id).ToList();
            foreach (var pro in projets) {
                prjtControleur.Delete(pro.Project_ID);
            }
            prjtRepo.Save();
            cutoRepo.Delete(cutomer);
            cutoRepo.Save();
            return RedirectToAction("AllCustomer");
        }
    }
}