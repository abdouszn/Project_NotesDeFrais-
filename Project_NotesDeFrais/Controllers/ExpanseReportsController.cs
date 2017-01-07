using Project_NotesDeFrais.Models;
using Project_NotesDeFrais.Models.Reposirery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Project_NotesDeFrais.Controllers
{
    [Authorize]
    public class ExpanseReportsController : Controller
    {
        // GET: Expanses
        public PartialViewResult Index(String userName)
        { 
            var userId = User.Identity.GetUserId();
            EmployesRepositery empRepository = new EmployesRepositery();
            ViewData["employer"] = "false";
            if (empRepository.GetByIdUser(userId) == null)
            {
                ViewData["employer"] = "true";
            }

            ViewData["userName"] = userName;
            ExpanseReportsModel model = new ExpanseReportsModel();
            model.Year = DateTime.Now.Year;
            return PartialView("_MonthYear", model);
        }

   
        public ActionResult createExpanseReportsDateDay(String userName)
        {
            ViewData["userName"] = userName;
            ViewData["month"] = Convert.ToInt32(Request.Form["Month"]);
            ViewData["year"] = Convert.ToInt32(Request.Form["Year"]);
            int mois = Convert.ToInt32(Request.Form["Month"]);
            var userId = User.Identity.GetUserId();

            ExpanseRepportRepositery expRepRepo = new ExpanseRepportRepositery();
            EmployesRepositery empRepository = new EmployesRepositery();
            ExpanseReports exp = new ExpanseReports();
            var idEmployer = empRepository.GetByIdUser(userId).Employee_ID;
            var actor_id = idEmployer;
            exp.ExpanseReport_ID = Guid.NewGuid();
            exp.CreationDate = DateTime.Now;
            exp.Year = Convert.ToInt32(Request.Form["Year"]);

            exp.Month = mois;
            exp.StatusCode = Convert.ToInt32(00);
            exp.ManagerValidationDate = Convert.ToDateTime(Request.Form["CreationDate"]);
            exp.ManagerComment = Convert.ToString(" ");
            exp.AccountingValidatationDate = Convert.ToDateTime(Request.Form["CreationDate"]);
            exp.AccountingComment = Convert.ToString(" ");
            exp.Total_HT = Convert.ToDouble(0);
            exp.Total_TTC = Convert.ToDouble(0);
            exp.Total_TVA = Convert.ToDouble(0);
            exp.Employee_ID = idEmployer;
            exp.Author_ID = actor_id;
            expRepRepo.AddExpansesReports(exp);
            return RedirectToAction("AllExpanses", "Expanses", new { idExpanseReport = exp.ExpanseReport_ID });

        }

        public ActionResult createExpanseReports(ExpanseReports exp, Guid? auther_id)
        {
            return null;
        }


        public ActionResult AllExpansesReports(int? pageIndex)
        {
            var userId = User.Identity.GetUserId();
            EmployesRepositery empRepository = new EmployesRepositery();
            if (empRepository.GetByIdUser(userId) == null)
            {
                ViewData["erreurMessage"] = "Vouns êtes pas encore Employer";
                return View("ErrorEmptyList");
            }
            var idEmployer = empRepository.GetByIdUser(userId).Employee_ID;
            ExpanseRepportRepositery expRepRepo = new ExpanseRepportRepositery();
            var countElementPage = 10;
            var expansesReports = expRepRepo.allExpanseReports(idEmployer);
            List<ExpanseReportsModel> expanseReportModelList = new List<ExpanseReportsModel>();

            if (expansesReports.Count()==0)
            {
                ViewData["erreurMessage"] = "Vouns n'avez aucune note de frais";
                ViewData["element"] = "ExpanseReports";
                ViewData["create"] = "false";
                return View("ErrorEmptyList");
            }

            foreach (var exp in expansesReports)
            {
                ExpanseReportsModel expReportModel = new ExpanseReportsModel();
                EmployeesModel employer = new EmployeesModel();
                expReportModel.ExpanseReport_ID = exp.ExpanseReport_ID;
                employer.FirstName = expRepRepo.GetByIdEmployes(exp.Employee_ID).FirstName;
                expReportModel.Employees = employer;
                expReportModel.CreationDate = exp.CreationDate;
                expReportModel.Year = exp.Year;
                expReportModel.Month = exp.Month;
                expReportModel.StatusCode = exp.StatusCode;
                expReportModel.ManagerValidationDate = exp.ManagerValidationDate;
                expReportModel.ManagerComment = exp.ManagerComment;
                expReportModel.AccountingValidatationDate = exp.AccountingValidatationDate;
                expReportModel.AccountingComment = exp.AccountingComment;
                expReportModel.Total_HT = exp.Total_HT;
                expReportModel.Total_TTC = exp.Total_TTC;
                expReportModel.Total_TVA =exp.Total_TVA;
                expanseReportModelList.Add(expReportModel);
            }
            IQueryable<ExpanseReportsModel> listExpanseReports = expanseReportModelList.AsQueryable();
            PaginatedList<ExpanseReportsModel> lst = new PaginatedList<ExpanseReportsModel>(listExpanseReports, pageIndex, countElementPage);
            return View("MyExpanseReports", lst);
        }

        public ActionResult Searche(String query, int? pageIndex)
        {
            var countElementPage = 10;
            ExpanseRepportRepositery expRep = new ExpanseRepportRepositery();
            var expanse = expRep.getSerachingExpanseReports(query);
            List<ExpanseReportsModel> expanseReportModelList = new List<ExpanseReportsModel>();

            foreach (var exp in expanse)
            {
                ExpanseReportsModel expReportModel = new ExpanseReportsModel();
                EmployeesModel employer = new EmployeesModel();
                
                expReportModel.ExpanseReport_ID = exp.ExpanseReport_ID;
                employer.FirstName = expRep.GetByIdEmployes(exp.Employee_ID).FirstName;
                expReportModel.Employees = employer;
                expReportModel.CreationDate = exp.CreationDate;
                expReportModel.Year = exp.Year;
                expReportModel.Month = exp.Month;
                expReportModel.StatusCode = exp.StatusCode;
                expReportModel.ManagerValidationDate = exp.ManagerValidationDate;
                expReportModel.ManagerComment = exp.ManagerComment;
                expReportModel.AccountingValidatationDate = exp.AccountingValidatationDate;
                expReportModel.AccountingComment = exp.AccountingComment;
                expReportModel.Total_HT = exp.Total_HT;
                expReportModel.Total_TTC = exp.Total_TTC;
                expReportModel.Total_TVA = exp.Total_TVA;

                expanseReportModelList.Add(expReportModel);
            }
            IQueryable<ExpanseReportsModel> listCust = expanseReportModelList.AsQueryable();
            PaginatedList<ExpanseReportsModel> lst = new PaginatedList<ExpanseReportsModel>(listCust, pageIndex, countElementPage);
            return View("MyExpanseReports", lst);
        }

       
        public ActionResult validateExpanseReport(Guid id) {
            ExpanseRepportRepositery expRep = new ExpanseRepportRepositery();
            ExpanseReports expReport = expRep.GetById(id);
            int StatusCode = 10;
            String managerComment = "no comment";
            String comtableComment = "no comment";
            expRep.updateStatus(expReport, StatusCode , managerComment, comtableComment);
            return RedirectToAction("AllExpansesReports");
        }

        [Authorize(Roles = "Comptable , Manager")]
        public ActionResult validateExpanseReportByAdminOrManager(Guid id) {
            ExpanseRepportRepositery expRep = new ExpanseRepportRepositery();
            String managerComment = "no comment";
            String comtableComment = "no comment";
            int StatusCode = 10;
            ExpanseReports expReport = expRep.GetById(id);
            if (User.IsInRole("Manager"))
            {
                StatusCode = 20;
                expRep.updateStatus(expReport, StatusCode, managerComment, comtableComment);
                return RedirectToAction("AllExpansesReportsToValid");
            }
            else if (User.IsInRole("Comptable"))
            {
                StatusCode = 30;
                expRep.updateStatus(expReport, StatusCode, managerComment, comtableComment);
                return RedirectToAction("AllExpansesReportsToValid");
            }
            expRep.updateStatus(expReport, StatusCode, managerComment, comtableComment);
            return RedirectToAction("AllExpansesReportsToValid");
        }

        
        public ActionResult Delete(Guid id)
        {
            ExpanseRepportRepositery expRep = new ExpanseRepportRepositery();
            ExpanseReports expReport = expRep.GetById(id);
            expRep.Delete(expReport);
            expRep.Save();
            return RedirectToAction("AllExpansesReports");
        }


        [Authorize(Roles = "Comptable , Manager")]
        public ActionResult modifExpanseReports(Guid idExpanseReport) {
            ExpanseRepportRepositery expRepRep = new ExpanseRepportRepositery();
            ExpanseReports expRep = expRepRep.GetById(idExpanseReport);
            ExpanseReportsModel expRepModel = new ExpanseReportsModel();
            expRepModel.ExpanseReport_ID = expRep.ExpanseReport_ID;
            expRepModel.Author_ID = expRep.Author_ID;
            expRepModel.ManagerComment = expRep.ManagerComment;
            
            return PartialView("_modifExpanseReports" , expRepModel);
        }


        [Authorize(Roles = "Comptable , Manager")]
        public ActionResult modifCommentExpanseReports(Guid idExpanseReport) {
           
            ExpanseRepportRepositery expRepRep = new ExpanseRepportRepositery();
            ExpanseReports expRep = expRepRep.GetById(idExpanseReport);
            String managerComment ="no comment";
            String comtableComment = "no comment";
            int StatusCode = 15;
            comtableComment = Convert.ToString(Request.Form["ManagerComment"]);
            if (User.IsInRole("Comptable")) {
                StatusCode = 25;
                comtableComment = Convert.ToString(Request.Form["AccountingComment"]);
            }
            expRepRep.updateStatus(expRep, StatusCode , managerComment, comtableComment);
            return RedirectToAction("AllExpansesReportsToValid");

        }

        public ActionResult annulExpanseReports(Guid idExpanseReport) {
            ExpanseRepportRepositery expRepRep = new ExpanseRepportRepositery();
            ExpanseReports expRep = expRepRep.GetById(idExpanseReport);
            String managerComment = "no comment";
            String comtableComment = "no comment";
            int StatusCode = 35;
            expRepRep.updateStatus(expRep, StatusCode, managerComment, comtableComment);
            return RedirectToAction("AllExpansesReports");
        }

        [Authorize(Roles = "Comptable , Manager")]
        public ActionResult AllExpansesReportsToValid(int? pageIndex)
        {
            var userId = User.Identity.GetUserId();
            ExpanseRepportRepositery expRepRepo = new ExpanseRepportRepositery();
            var countElementPage = 10;
            var expansesReports = expRepRepo.allExpanseReportsToValid();
            if (User.IsInRole("Comptable"))
            {
                expansesReports = expRepRepo.allExpanseReportsToValidComptable();
            } 
            List<ExpanseReportsModel> expanseReportModelList = new List<ExpanseReportsModel>();


            if (expansesReports.Count() == 0)
            {
                ViewData["erreurMessage"] = "Vouns n'avez aucune note de frais à valider";
                ViewData["create"] = "false";
                return View("ErrorEmptyList");
            }


            foreach (var exp in expansesReports)
            {
                ExpanseReportsModel expReportModel = new ExpanseReportsModel();
                EmployeesModel employer = new EmployeesModel();
                expReportModel.ExpanseReport_ID = exp.ExpanseReport_ID;
                employer.FirstName = expRepRepo.GetByIdEmployes(exp.Employee_ID).FirstName;
                expReportModel.Employees = employer;
                expReportModel.CreationDate = exp.CreationDate;
                expReportModel.Year = exp.Year;
                expReportModel.Month = exp.Month;
                expReportModel.StatusCode = exp.StatusCode;
                expReportModel.ManagerValidationDate = exp.ManagerValidationDate;
                expReportModel.ManagerComment = exp.ManagerComment;
                expReportModel.AccountingValidatationDate = exp.AccountingValidatationDate;
                expReportModel.AccountingComment = exp.AccountingComment;
                expReportModel.Total_HT = exp.Total_HT;
                expReportModel.Total_TTC = exp.Total_TTC;
                expReportModel.Total_TVA = exp.Total_TVA;
                expanseReportModelList.Add(expReportModel);
            }
            IQueryable<ExpanseReportsModel> listExpanseReports = expanseReportModelList.AsQueryable();
            PaginatedList<ExpanseReportsModel> lst = new PaginatedList<ExpanseReportsModel>(listExpanseReports, pageIndex, countElementPage);
            return View("ToValid", lst);
        }
    }
}