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
        private IEnumerable<object> costumers;

        // GET: Expanses
        public ActionResult Index()
        {
            return View("ExpansesFormulaire");
        }

        public void createExpanses(Expanses exp, Guid? expanseReport_ID)
        {
            ExpanseRepositery expRepo = new ExpanseRepositery();
            var idCustomer = Request.Form["customerSelect"];
            var idexpanseType = Request.Form["typeSelect"];
            var idprojet = Request.Form["projectSelect"];
            var idexpanseRepor = expRepo.maxIdExpanseReports();
            exp.Expanse_ID = Guid.NewGuid();
            exp.Amount_HT = Convert.ToInt32(Request.Form["Amount_HT"]);
            exp.Amount_TTC = Convert.ToInt32(Request.Form["Amount_TTC"]);
            exp.Amount_TVA = Convert.ToInt32(Request.Form["Amount_TVA"]);
            exp.Day = Convert.ToInt32(Request.Form["Day"]);
            exp.ExpanseReport_ID = idexpanseRepor;
            exp.Customer_ID = new Guid(idCustomer);
            exp.ExpanseType_ID = new Guid(idexpanseType);
            exp.Project_ID = new Guid(idprojet);
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
                projet.Name = expRepo.GetByIdProjects(exp.Project_ID).Name;
                expType.Name = expRepo.GetByIdExpanseTypes(exp.ExpanseType_ID).Name;
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

        [HttpGet]
        public PartialViewResult Popup()
        {
            CustomerRepositery cstRepo = new CustomerRepositery();
            ProjetRepositery prjtRepo = new ProjetRepositery();
            ExpanseTypesRepositery expTypRepo = new ExpanseTypesRepositery();
            List<CustomersModel> customersModel = new List<CustomersModel>();
            List<ProjectsModel> projectsListModel = new List<ProjectsModel>();
            List<ExpanseTypesModel> expansesTypeListModel = new List<ExpanseTypesModel>();

            IQueryable<Customers> costumers = cstRepo.allCustomers();

            foreach (var cust in costumers)
            {
                CustomersModel custModel = new CustomersModel();
                custModel.Customer_ID = cust.Customer_ID;
                custModel.Code = cust.Code;
                custModel.Name = cust.Name;
                customersModel.Add(custModel);
            }

            IQueryable<Projects> projectsList = prjtRepo.allProjects();
            foreach (var prjt in projectsList)
            {
                ProjectsModel prjtModel = new ProjectsModel();
                CustomersModel Customer = new CustomersModel();
                prjtModel.Project_ID = prjt.Project_ID;
                prjtModel.Pole_ID = prjt.Pole_ID;
                prjtModel.Description = prjt.Description;
                prjtModel.Budget = prjt.Budget;
                prjtModel.Name = prjt.Name;
                Customer.Name = prjtRepo.GetByIdCutomer(prjt.Customer_ID).Name;
                prjtModel.Customers = Customer;
                projectsListModel.Add(prjtModel);
            }

            IQueryable<ExpanseTypes> expanseTypes = expTypRepo.allExpanseTypes();

            foreach (var expTpe in expanseTypes)
            {
                ExpanseTypesModel expenseTypeModel = new ExpanseTypesModel();
                expenseTypeModel.ExpenseType_ID = expTpe.ExpenseType_ID;
                expenseTypeModel.Name = expTpe.Name;
                expenseTypeModel.Ceiling = expTpe.Ceiling;
                expenseTypeModel.Fixed = expTpe.Fixed;
                expenseTypeModel.OnlyManagers = expTpe.OnlyManagers;
                expenseTypeModel.Tva_ID = expTpe.Tva_ID;
                expansesTypeListModel.Add(expenseTypeModel);
            }

            var expanseViewModel = new ExpansesModel
            {
                CustomersList = customersModel,
                ProjectsList = projectsListModel,
                ExpanseTypesList = expansesTypeListModel
            };

            return PartialView("_AddType", expanseViewModel);
        }

        public PartialViewResult ListProject(Guid customerId)
        {
            ProjetRepositery prjtRepo = new ProjetRepositery();
            IQueryable<Projects> projectsList = prjtRepo.GetProjectsByIdCutomer(customerId);
            List<ProjectsModel> projectsListModel = new List<ProjectsModel>();

            foreach (var prjt in projectsList)
            {
                ProjectsModel prjtModel = new ProjectsModel();
                CustomersModel Customer = new CustomersModel();
                prjtModel.Project_ID = prjt.Project_ID;
                prjtModel.Pole_ID = prjt.Pole_ID;
                prjtModel.Description = prjt.Description;
                prjtModel.Budget = prjt.Budget;
                prjtModel.Name = prjt.Name;
                Customer.Name = prjtRepo.GetByIdCutomer(prjt.Customer_ID).Name;
                prjtModel.Customers = Customer;
                projectsListModel.Add(prjtModel);
            }


           

            var expanseViewModel = new ExpansesModel
            {
               
                ProjectsList = projectsListModel
               
            };
            return PartialView("_ProjectItem", expanseViewModel);
        }
    }
}