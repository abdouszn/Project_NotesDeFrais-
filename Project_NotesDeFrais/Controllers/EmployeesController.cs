using Project_NotesDeFrais.Models;
using Project_NotesDeFrais.Models.Reposirery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Project_NotesDeFrais.Controllers
{
    public class EmployeesController : Controller
    {
        public ActionResult Index()
        {
            EmployeesModel empModel = new EmployeesModel();
            EmployesRepositery empRp = new EmployesRepositery();

            empModel.AspNetUsersList = empRp.getAllUsers().ToList();
            empModel.polesList = empRp.getAllPoles().ToList();
            return View("EmployesFormulaire" , empModel);
        }

        public ActionResult CreateEmploye(EmployeesModel empModel)
        {
            EmployesRepositery empRp = new EmployesRepositery();
            Employees emp = new Employees();
            if (!ModelState.IsValidField("FirstName") || !ModelState.IsValidField("LastName") ||
               !ModelState.IsValidField("Email") || !ModelState.IsValidField("Telephone"))
            {
                empModel.AspNetUsersList = empRp.getAllUsers().ToList();
                empModel.polesList = empRp.getAllPoles().ToList();
                return View("EmployesFormulaire",empModel);
            }
            emp.Employee_ID = Guid.NewGuid();
            String userUmail = Convert.ToString(Request.Form["userList"]);
            String userName = Convert.ToString(Request.Form["polesList"]);
            emp.User_ID = empRp.getUserByMail(userUmail);
            emp.Pole_ID = empRp.getPoleByName(userName);
            emp.FirstName = Convert.ToString(Request.Form["FirstName"]);
            emp.LastName = Convert.ToString(Request.Form["LastName"]);
            emp.Email= Convert.ToString(Request.Form["Email"]);
            emp.Telephone = Convert.ToString(Request.Form["Telephone"]);
            empRp.AddEmployes(emp);
            return RedirectToAction("AllEmployees");

        }

        public ActionResult AllEmployees(int? pageIndex)
        {
            var countElementPage = 10;
            EmployesRepositery empRp = new EmployesRepositery();
            AspNetUsers user = new AspNetUsers();
            PolesModel pole = new PolesModel();
            var employers = empRp.allEmployees();
            List<EmployeesModel> employersModel = new List<EmployeesModel>();
            foreach (var emp in employers)
            {
                var polId = emp.Pole_ID != null ? emp.Pole_ID : null;
                EmployeesModel empModel = new EmployeesModel();
                empModel.Email = emp.Email;
                empModel.Employee_ID = emp.Employee_ID;
                empModel.FirstName = emp.FirstName;
                empModel.LastName = emp.LastName;
                empModel.Telephone = emp.Telephone;
                empModel.User_ID = emp.User_ID;
                empModel.Pole_ID = emp.Pole_ID;
                empModel.AspNetUsers = empRp.getUserById(emp.User_ID);
                pole.Name= empRp.getPoleById(emp.Pole_ID).Name;
                empModel.Poles = pole;
                employersModel.Add(empModel);
            }
            IQueryable<EmployeesModel> listEmp = employersModel.AsQueryable();
            PaginatedList<EmployeesModel> lst = new PaginatedList<EmployeesModel>(listEmp, pageIndex, countElementPage);
            return View("AllEmployes", lst);
        }

        public ActionResult Searche(int? pageIndex , String query)
        {
            var countElementPage = 10;
            EmployesRepositery empRp = new EmployesRepositery();
            AspNetUsers user = new AspNetUsers();
            PolesModel pole = new PolesModel();
            var employers = empRp.getSerachingemployees(query);
            List<EmployeesModel> employersModel = new List<EmployeesModel>();
            foreach (var emp in employers)
            {
                var polId = emp.Pole_ID != null ? emp.Pole_ID : null;
                EmployeesModel empModel = new EmployeesModel();
                empModel.Email = emp.Email;
                empModel.Employee_ID = emp.Employee_ID;
                empModel.FirstName = emp.FirstName;
                empModel.LastName = emp.LastName;
                empModel.Telephone = emp.Telephone;
                empModel.User_ID = emp.User_ID;
                empModel.Pole_ID = emp.Pole_ID;
                empModel.AspNetUsers = empRp.getUserById(emp.User_ID);
                empModel.Poles.Name = empRp.getPoleById(emp.Pole_ID).Name;

                employersModel.Add(empModel);
            }
            IQueryable<EmployeesModel> listEmp = employersModel.AsQueryable();
            PaginatedList<EmployeesModel> lst = new PaginatedList<EmployeesModel>(listEmp, pageIndex, countElementPage);
            return View("Allmployes", lst);
        }

        public ActionResult createUserRole()
        {
            EmployeesModel empModel = new EmployeesModel();
            EmployesRepositery empRp = new EmployesRepositery();

            empModel.AspNetUsersList = empRp.getAllUsers().ToList();
            empModel.polesList = empRp.getAllPoles().ToList();
            return View("EmployesFormulaire", empModel);
        }

        public ActionResult CreateUserRoles(EmployeesModel empModel)
        {
            EmployesRepositery empRp = new EmployesRepositery();
            Employees emp = new Employees();
            if (!ModelState.IsValidField("FirstName") || !ModelState.IsValidField("LastName") ||
               !ModelState.IsValidField("Email") || !ModelState.IsValidField("Telephone"))
            {
                empModel.AspNetUsersList = empRp.getAllUsers().ToList();
                empModel.polesList = empRp.getAllPoles().ToList();
                return View("EmployesFormulaire", empModel);
            }
            emp.Employee_ID = Guid.NewGuid();
            String userUmail = Convert.ToString(Request.Form["userList"]);
            String userName = Convert.ToString(Request.Form["polesList"]);
            emp.User_ID = empRp.getUserByMail(userUmail);
            emp.Pole_ID = empRp.getPoleByName(userName);
            emp.FirstName = Convert.ToString(Request.Form["FirstName"]);
            emp.LastName = Convert.ToString(Request.Form["LastName"]);
            emp.Email = Convert.ToString(Request.Form["Email"]);
            emp.Telephone = Convert.ToString(Request.Form["Telephone"]);
            empRp.AddEmployes(emp);
            return RedirectToAction("AllEmployees");

        }
    }
}