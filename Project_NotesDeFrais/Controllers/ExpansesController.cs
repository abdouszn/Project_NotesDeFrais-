﻿using Project_NotesDeFrais.Models;
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

        [Authorize]
        // GET: Expanses
        public ActionResult Index()
        {
            return View("ExpansesFormulaire");
        }

        [Authorize]
        public ActionResult createExpanses(ExpansesModel expModel, Guid expanseReport_ID)
        {
           

            ExpanseRepositery expRepo = new ExpanseRepositery();
            Expanses exp = new Expanses();
            if (!ModelState.IsValid)
            {
                exp.Amount_HT = Convert.ToInt32(0);
                exp.Amount_TVA = Convert.ToInt32(0);
            }
            else {
                  exp.Amount_HT = Convert.ToInt32(Request.Form["Amount_HT"]);
            exp.Amount_TVA = Convert.ToInt32(Request.Form["Amount_TVA"]);
            }
            var idCustomer = Request.Form["customerSelect"];
            var idexpanseType = Request.Form["typeSelect"];
            var idprojet = Request.Form["projectSelect"];
            exp.Expanse_ID = Guid.NewGuid();
            exp.Amount_TTC = exp.Amount_HT * exp.Amount_TVA / 100 + exp.Amount_HT;
            exp.Day = Convert.ToInt16(Request.Form["monthSelect"]);
            exp.ExpanseReport_ID = expanseReport_ID;
            exp.Customer_ID = new Guid(idCustomer);
            exp.ExpanseType_ID = new Guid(idexpanseType);
            exp.Project_ID = new Guid(idprojet);
            expRepo.AddExpanses(exp);
            return RedirectToAction("AllExpanses",new {idExpanseReport= expanseReport_ID});
        }

        [Authorize]
        public ActionResult edit(Guid idExpanse) {
            ExpanseRepositery expRep = new ExpanseRepositery();
            ExpansesModel expModel = new ExpansesModel();
            Expanses exp = expRep.GetById(idExpanse);
            expModel.Expanse_ID = exp.Expanse_ID;
            expModel.Project_ID = exp.Project_ID;
            expModel.Customer_ID = exp.Customer_ID;
            expModel.ExpanseReport_ID = exp.ExpanseReport_ID;
            expModel.Amount_HT = exp.Amount_HT;
            expModel.Amount_TTC = exp.Amount_TTC;
            expModel.Amount_TVA = exp.Amount_TVA;
            expModel.Day = exp.Day;
            return View("EditExpanses" , expModel);
        }

        [Authorize]
        public ActionResult AllExpanses(int? pageIndex , Guid idExpanseReport)
        {
            ExpanseRepositery expRepo = new ExpanseRepositery();
            double ttc = 0;
            double tva = 0;
            double ht = 0;

            var countElementPage = 10;
            var expanses = expRepo.GetAllByIdExpansesRepport(idExpanseReport);
            ViewData["idExpanseReport"] = idExpanseReport;
            List<ExpansesModel> expanseModel = new List<ExpansesModel>();
            foreach (var exp in expanses)
            {
                tva = tva + exp.Amount_TVA;
                ht = ht + exp.Amount_HT;
                ttc = ttc + exp.Amount_TTC;
                ExpansesModel expanse = new ExpansesModel();
                CustomersModel customer = new CustomersModel();
                ExpanseTypesModel expType = new ExpanseTypesModel();
                ExpanseReportsModel expanseRapport = new ExpanseReportsModel();
                ProjectsModel projet = new ProjectsModel();
                expanseRapport.ExpanseReport_ID = idExpanseReport;
                expanse.ExpanseReport_ID = exp.Expanse_ID;
                expanse.Expanse_ID = exp.Expanse_ID;
                expanse.Amount_HT = exp.Amount_HT;
                expanse.Amount_TTC = exp.Amount_TTC;
                expanse.Amount_TVA = exp.Amount_TVA;
                expanse.Day = exp.Day;
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
            ExpanseRepportRepositery expRapRep = new ExpanseRepportRepositery();
            ExpanseReports expRap = expRapRep.GetById(idExpanseReport);
            expRapRep.updateExpanseReports(expRap, ttc, ht, tva);
            ViewData["idExpanseReport"] = idExpanseReport;
            IQueryable<ExpansesModel> listExpanse = expanseModel.AsQueryable();
            PaginatedList<ExpansesModel> lst = new PaginatedList<ExpansesModel>(listExpanse, pageIndex, countElementPage);
            return View("AllExpanses", lst);
        }

        [Authorize]
        public ActionResult Searche(String query, int? pageIndex ,Guid id)
        {
            var countElementPage = 10;
            ExpanseRepositery expRepo = new ExpanseRepositery();
            var expanses = expRepo.getSerachingExpanses(query , id);
            List<ExpansesModel> expanseModel = new List<ExpansesModel>();
            ViewData["idExpanseReport"] = id;
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
        [Authorize]
        public ActionResult Popup(Guid idExpanseReport)
        {
            CustomerRepositery cstRepo = new CustomerRepositery();
            ProjetRepositery prjtRepo = new ProjetRepositery();
            ExpanseTypesRepositery expTypRepo = new ExpanseTypesRepositery();
            List<CustomersModel> customersModel = new List<CustomersModel>();
            List<ProjectsModel> projectsListModel = new List<ProjectsModel>();
            List<ExpanseTypesModel> expansesTypeListModel = new List<ExpanseTypesModel>();
            ViewData["empty"] = "false";

            IQueryable<Customers> costumers = cstRepo.getlistCustomersHasProject();

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
            if (User.IsInRole("manager"))
            {
                expanseTypes = expTypRepo.allExpanseTypesManager();
            }

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

            if (customersModel.Count() == 0 || projectsListModel.Count()==0 || expansesTypeListModel.Count()==0)
            {
                ViewData["erreur"] = "Customers , Projets et des ExpanseType";
                ViewData["empty"] = "true";
            }
            var expanseViewModel = new ExpansesModel
            {
                CustomersList = customersModel,
                ProjectsList = projectsListModel,
                ExpanseTypesList = expansesTypeListModel
            };
            expanseViewModel.ExpanseReport_ID = idExpanseReport;

            return PartialView("_AddType", expanseViewModel);
        }

        [Authorize]
        public ActionResult confirmDelete(Guid id) {
            ViewData["confirmDelete"] = "/Expanses/Delete?id=" + id;
            return PartialView("_confirmDelet");
        }

        [Authorize]
        public ActionResult updateExpanse(Guid idExpanse) {
            ExpanseRepositery expRepo = new ExpanseRepositery();
            Expanses exp = expRepo.GetById(idExpanse);
            Guid expRapId = exp.ExpanseReport_ID;
            double ht = Convert.ToInt32(Request.Form["Amount_HT"]);
            double ttc = Convert.ToInt32(Request.Form["Amount_TTC"]);
            double tva = Convert.ToInt32(Request.Form["Amount_TVA"]);
            expRepo.updateExpanses(exp, ttc, ht, tva);
            return RedirectToAction("AllExpanses", new { idExpanseReport = expRapId });
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

        [Authorize]
        public ActionResult Delete(Guid id)
        {
            ExpanseRepositery expRepo = new ExpanseRepositery();
            ExpanseRepportRepositery expRapRep = new ExpanseRepportRepositery();
            Expanses exp = expRepo.GetById(id);
            Guid idExpRapo = exp.ExpanseReport_ID;
            ExpanseReports expReport = expRapRep.GetById(idExpRapo);
            double ttc = expReport.Total_TTC - exp.Amount_TTC;
            double ht = expReport.Total_HT - exp.Amount_HT;
            double tva = expReport.Total_TVA - exp.Amount_TVA;
            expRapRep.updateExpanseReports(expReport, ttc, ht, tva);
            expRepo.Delete(exp);
            expRepo.Save();
            return RedirectToAction("AllExpanses", new { idExpanseReport = idExpRapo });
        }
    }
}