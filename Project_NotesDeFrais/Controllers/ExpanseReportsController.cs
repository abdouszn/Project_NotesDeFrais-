using Project_NotesDeFrais.Models;
using Project_NotesDeFrais.Models.Reposirery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_NotesDeFrais.Controllers
{
    public class ExpanseReportsController : Controller
    {
        // GET: Expanses
        public ActionResult Index()
        {
            return View("ExpanseReportsFormulaire");
        }

        public void createCustomer(ExpanseReports exp, Guid employer_ID , Guid auther_id)
        {
            ExpanseRepportRepositery expRepRepo = new ExpanseRepportRepositery();
            var idEmployer = employer_ID != null ? (Guid)employer_ID : expRepRepo.maxIdCustomers();
            
            exp.ExpanseReport_ID = Guid.NewGuid();
            exp.CreationDate = Convert.ToDateTime(Request.Form["CreationDate"]);
            exp.Year= Convert.ToInt32(Request.Form["Year"]);
            exp.Month = Convert.ToInt32(Request.Form["Month"]);
            exp.StatusCode = Convert.ToInt32(Request.Form["StatusCode"]);
            exp.ManagerValidationDate= Convert.ToDateTime(Request.Form["ManagerValidationDate"]);
            exp.ManagerComment= Convert.ToString(Request.Form["ManagerComment"]);
            exp.AccountingValidatationDate= Convert.ToDateTime(Request.Form["AccountingValidatationDate"]);
            exp.AccountingComment = Convert.ToString(Request.Form["AccountingComment"]);
            exp.Total_HT = Convert.ToDouble(Request.Form["Total_HT"]);
            exp.Total_TTC = Convert.ToDouble(Request.Form["Total_TTC"]);
            exp.Total_TVA = Convert.ToDouble(Request.Form["Total_TVA"]);
            exp.Employee_ID = employer_ID;
            exp.Author_ID = auther_id;
            expRepRepo.AddExpansesReports(exp);
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
                expReportModel.Employees.FirstName = expRepRepo.GetByIdEmployes(expReportModel.Employee_ID).FirstName;

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

        /*public ActionResult Searche(String query, int? pageIndex)
        {
            var countElementPage = 10;
            ExpansesRepositery expRep = new ExpansesRepositery();
            var expanse = expRep.getSerachingExpanses(query);
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
        }*/
    }
}