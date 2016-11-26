using Project_NotesDeFrais.Models;
using Project_NotesDeFrais.Models.Reposirery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_NotesDeFrais.Controllers
{
    public class ProjetController : Controller
    {
        // GET: Projet
        public ActionResult Index(Guid? id_Customer) 
        {
            ViewData["id_Customer"] = id_Customer;
            return View("ProjectFormulaire");
        }


        public void createProject(Projects projet , Guid id_Customer)
        {
            ProjetRepositery prtRep = new ProjetRepositery();
            projet.Project_ID = Guid.NewGuid();
            projet.Name = Convert.ToString(Request.Form["Name"]);
            projet.Description = Convert.ToString(Request.Form["Description"]);
            projet.Budget = Convert.ToDouble(Request.Form["Budget"]);
            projet.Customer_ID = id_Customer;
            projet.Pole_ID = prtRep.maxIdPoles();
            prtRep.AddProjet(projet);
        }
        

        public ActionResult AllProjets(int? pageIndex)
        {
            ProjetRepositery prtRep = new ProjetRepositery();
            int countElementPage = 10;
            var projets = prtRep.allProjects();
            List<ProjectsModel> projetsModel = new List<ProjectsModel>();

            foreach (var prjt in projets)
            {
                ProjectsModel prjtModel = new ProjectsModel();
                CustomersModel Customer = new CustomersModel();
                prjtModel.Project_ID = prjt.Project_ID;
                prjtModel.Pole_ID = prjt.Pole_ID;
                prjtModel.Description = prjt.Description;
                prjtModel.Budget=prjt.Budget;
                prjtModel.Name = prjt.Name;
                Customer.Name = prtRep.GetByIdCutomer(prjt.Customer_ID).Name;
                prjtModel.Customers = Customer;
                projetsModel.Add(prjtModel);
            }
            IQueryable<ProjectsModel> listProjets = projetsModel.AsQueryable();
            PaginatedList<ProjectsModel> lst = new PaginatedList<ProjectsModel>(listProjets, pageIndex, countElementPage);
            return View("AllProjects", lst);
        }


        public ActionResult Searche(String query, int? pageIndex)
        {
            var countElementPage = 10;
            ProjetRepositery projetRep = new ProjetRepositery();
            var projets = projetRep.getSerachingProjects(query);
            List<ProjectsModel> projetsModel = new List<ProjectsModel>();

            foreach (var prjt in projets)
            {
                ProjectsModel prjtModel = new ProjectsModel();
                prjtModel.Project_ID = prjt.Project_ID;
                prjtModel.Pole_ID = prjt.Pole_ID;
                prjtModel.Description = prjt.Description;
                prjtModel.Budget = prjt.Budget;
                prjtModel.Name = prjt.Name;
                prjtModel.Customer_ID = prjt.Customer_ID;
                projetsModel.Add(prjtModel);
            }
            IQueryable<ProjectsModel> listProjets = projetsModel.AsQueryable();
            PaginatedList<ProjectsModel> lst = new PaginatedList<ProjectsModel>(listProjets, pageIndex, countElementPage);
            return View("AllProjects", lst);
        }
    }
}