using Project_NotesDeFrais.Models;
using Project_NotesDeFrais.Models.Reposirery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_NotesDeFrais.Controllers
{
    public class ExpansesController : Controller
    {
        // GET: Expanses
        public ActionResult Index()
        {
            return View("ExpansesFormulaire");
        }

        public void createExpanses(Expanses exp , Guid? expanseReport_ID, Guid? expanseType_ID , Guid? Customers_ID,
            Guid? project_ID)
        {
            ExpanseRepositery expRepo = new ExpanseRepositery();
            var idCustomer = Customers_ID != null ? (Guid)Customers_ID : expRepo.maxIdCustomers();
            var idexpanseRepor = expanseReport_ID != null ? (Guid)Customers_ID : expRepo.maxIdExpanseReports();
            var idexpanseType = expanseType_ID != null ? (Guid)Customers_ID : expRepo.maxIdExpanseType();
            var idprojet = project_ID != null ? (Guid)Customers_ID : expRepo.maxIdProject();
            exp.Expanse_ID = Guid.NewGuid();
            exp.Amount_HT = Convert.ToInt32(Request.Form["Amount_HT"]);
            exp.Amount_TTC= Convert.ToInt32(Request.Form["Amount_TTC"]);
            exp.Amount_TVA= Convert.ToInt32(Request.Form["Amount_TVA"]);
            exp.Day= Convert.ToInt32(Request.Form["Day"]);
            exp.ExpanseReport_ID = idexpanseRepor;
            exp.Customer_ID= idCustomer;
            exp.ExpanseType_ID= idexpanseType;
            exp.Project_ID= idprojet;
            expRepo.AddExpanses(exp);
        }

        public ActionResult AllExpanses(int? pageIndex)
        {
            ExpanseRepositery expRepo = new ExpanseRepositery();

            var countElementPage = 10;
            var expanses = expRepo.allExpanses();
            List<ExpansesModel> expanseModel = new List<ExpansesModel>();

            foreach (var exp in expanses)
            {
                ExpansesModel expanse = new ExpansesModel();
                CustomersModel customer = new CustomersModel();
                ExpanseTypesModel expType = new ExpanseTypesModel();
                ExpanseReportsModel expanseRapport = new ExpanseReportsModel();
                ProjectsModel projet = new ProjectsModel();

                expanse.Expanse_ID = exp.Expanse_ID;
                expanse.Amount_HT = exp.Amount_HT;
                expanse.Amount_TTC = exp.Amount_TTC;
                expanse.Amount_TVA = exp.Amount_TVA;
                customer.Name = expRepo.GetByIdCutomer(exp.Customer_ID).Name;
                projet.Name= expRepo.GetByIdProjects(exp.Project_ID).Name;
                expType.Name= expRepo.GetByIdExpanseTypes(exp.ExpanseType_ID).Name;
                expanseRapport.Year = expRepo.GetByIdExpansesRepport(exp.ExpanseReport_ID).Year;
                expanse.Customers = customer;
                expanse.Projects = projet;
                expanse.ExpanseReports = expanseRapport;
                expanse.ExpanseTypes = expType;
                expanseModel.Add(expanse);
            }
            IQueryable<ExpansesModel> listExpanse = expanseModel.AsQueryable();
            PaginatedList<ExpansesModel> lst = new PaginatedList<ExpansesModel>(listExpanse, pageIndex, countElementPage);
            return View("AllExpanses", lst);
        }

        public ActionResult Searche(String query, int? pageIndex)
        {
            var countElementPage = 10;
            ExpanseRepositery expRepo = new ExpanseRepositery();
            var expanses = expRepo.getSerachingExpanses(query);
            List<ExpansesModel> expanseModel = new List<ExpansesModel>();

            foreach (var exp in expanses)
            {
                ExpansesModel expanse = new ExpansesModel();
                CustomersModel customer = new CustomersModel();
                ExpanseTypesModel expType = new ExpanseTypesModel();
                ExpanseReportsModel expanseRapport = new ExpanseReportsModel();
                ProjectsModel projet = new ProjectsModel();

                expanse.Expanse_ID = exp.Expanse_ID;
                expanse.Amount_HT = exp.Amount_HT;
                expanse.Amount_TTC = exp.Amount_TTC;
                expanse.Amount_TVA = exp.Amount_TVA;
                customer.Name = expRepo.GetByIdCutomer(exp.Customer_ID).Name;
                projet.Name = expRepo.GetByIdProjects(exp.Project_ID).Name;
                expType.Name = expRepo.GetByIdExpanseTypes(exp.ExpanseType_ID).Name;
                expanseRapport.Year = expRepo.GetByIdExpansesRepport(exp.ExpanseReport_ID).Year;
                expanse.Customers = customer;
                expanse.Projects = projet;
                expanse.ExpanseReports = expanseRapport;
                expanse.ExpanseTypes = expType;
                expanseModel.Add(expanse);
            }
            IQueryable<ExpansesModel> listCust = expanseModel.AsQueryable();
            PaginatedList<ExpansesModel> lst = new PaginatedList<ExpansesModel>(listCust, pageIndex, countElementPage);
            return View("AllExpanses", lst);
        }
    }
}