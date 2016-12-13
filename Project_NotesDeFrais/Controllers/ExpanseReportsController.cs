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
    public class ExpanseReportsController : Controller
    {
        // GET: Expanses
        public ActionResult Index(String userName)
        {
            ViewData["userName"] = userName;
            if (Convert.ToInt32(Request.Form["Month"]) > DateTime.Now.Month) {
                ModelState.AddModelError("error.error", "adfdghdghgdhgdhdgda");
                return PartialView("_MonthYear");
            }
            ViewData["month"] = Convert.ToInt32(Request.Form["Month"]);
            ViewData["year"] = Convert.ToInt32(Request.Form["Year"]);
            var userId = User.Identity.GetUserId();
            ExpanseRepportRepositery expRepRepo = new ExpanseRepportRepositery();
            EmployesRepositery empRepository = new EmployesRepositery();
            ExpanseReports exp = new ExpanseReports();
            var idEmployer = empRepository.GetByIdUser(userId).Employee_ID;
            var actor_id = idEmployer;
            exp.ExpanseReport_ID = Guid.NewGuid();
            exp.CreationDate = Convert.ToDateTime(Request.Form["CreationDate"]);
            exp.Year = Convert.ToInt32(Request.Form["Year"]);
            exp.Month = Convert.ToInt32(Request.Form["Month"]);
            exp.StatusCode = Convert.ToInt32(0);
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
            return RedirectToAction("AllExpanses", "Expanses", new { idExpanseReport = exp.ExpanseReport_ID});
        }

        public PartialViewResult createExpanseReportsDateDay(String userName)
        {
            ViewData["userName"] = userName;
            return PartialView("_MonthYear");
        }


        public ActionResult createExpanseReports(ExpanseReports exp, Guid? auther_id)
        {
            return null;
        }

        public ActionResult AllExpansesReports(int? pageIndex)
        {
            ExpanseRepportRepositery expRepRepo = new ExpanseRepportRepositery();
            var countElementPage = 10;
            var expansesReports = expRepRepo.allExpanseReports();
            List<ExpanseReportsModel> expanseReportModelList = new List<ExpanseReportsModel>();

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
            return View("AllExpanseReports", lst);
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
            return View("AllExpanseReports", lst);
        }

        public ActionResult validateExpanseReport(Guid id) {
            ExpanseRepportRepositery expRep = new ExpanseRepportRepositery();
            ExpanseReports expReport = expRep.GetById(id);
            int StatusCode = 10;
            expRep.updateStatus(expReport, StatusCode);
            return RedirectToAction("AllExpanses", "Expanses", new { idExpanseReport = id });
        }
        public ActionResult Delete(Guid id)
        {
            ExpanseRepportRepositery expRep = new ExpanseRepportRepositery();
            ExpanseReports expReport = expRep.GetById(id);
            expRep.Delete(expReport);
            expRep.Save();
            return RedirectToAction("AllExpansesReports");
        }
    }
}