using Microsoft.AspNet.Identity;
using Project_NotesDeFrais.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_NotesDeFrais.Controllers
{
    public class NotesDeFraisController : Controller
    {
        // GET: NotesDeFrais
        public ActionResult Index()
        {
            return View("EmployeFormulaire");
        }

        public void CreatEmploye(Employees emp) {
            NotesDeFraisRepositery ndfRp = new NotesDeFraisRepositery();

            emp.Employee_ID = Guid.NewGuid();
            emp.User_ID= User.Identity.GetUserId();
            emp.FirstName = Convert.ToString(Request.Form["FirstName"]);
            emp.LastName = Convert.ToString(Request.Form["LastName"]);
            emp.Telephone = Convert.ToString(Request.Form["Telephone"]);
            ndfRp.Add(emp);
           
        }

        public ActionResult getAllEmmployers() {
            NotesDeFraisRepositery ndfRp = new NotesDeFraisRepositery();
            var employers = ndfRp.allEmployers();
            List< EmployeesModel> employersModel = new List<EmployeesModel>();
            foreach(var emp in employers) {
                EmployeesModel empModel = new EmployeesModel();
                empModel.Email = emp.Email;
                empModel.Employee_ID = emp.Employee_ID;
                empModel.FirstName = emp.FirstName;
                empModel.LastName = emp.LastName;
                empModel.Telephone = emp.Telephone;
                empModel.User_ID = emp.User_ID;
                employersModel.Add(empModel);
            }
            IQueryable<EmployeesModel>listEmp= employersModel.AsQueryable();
            return View("AffichageEmploye" , listEmp);
        }
    }
}