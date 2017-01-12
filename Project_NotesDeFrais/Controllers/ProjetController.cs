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
        [Authorize]
        // GET: Projet
        public ActionResult Index(Guid? id_Customer) 
        {
            ProjetRepositery prtRep = new ProjetRepositery();
            ProjectsModel prjtModel = new ProjectsModel();
            if (prtRep.getAllCustomers().ToList().Count()==0 || prtRep.getAllPoles().ToList().Count()==0)
            {
                ViewData["erreur"]  = "Customers et des Poles ";
                return View("ErrorEmptyElement");
            }
            prjtModel.CustomersList = prtRep.getAllCustomers().ToList();
           
            prjtModel.PolesList = prtRep.getAllPoles().ToList();
            ViewData["id_Customer"] = id_Customer;

            return View("ProjectFormulaire" , prjtModel);
        }

        [Authorize]
        public ActionResult createProject(ProjectsModel projetModel , Guid? id_Customer)
        {
            ProjetRepositery prtRep = new ProjetRepositery();
            if (!ModelState.IsValidField("Name") || !ModelState.IsValidField("Budget"))
            {
                projetModel.CustomersList = prtRep.getAllCustomers().ToList();
                projetModel.PolesList = prtRep.getAllPoles().ToList();
                return View("ProjectFormulaire", projetModel);
            }
            ViewData["id_Customer"] = id_Customer;
            Projects projet = new Projects();
            projet.Project_ID = Guid.NewGuid();
            projet.Name = Convert.ToString(Request.Form["Name"]);
            projet.Description = Convert.ToString(Request.Form["Description"]);
            projet.Budget = Convert.ToDouble(Request.Form["Budget"]);
            

            projet.Customer_ID = id_Customer != null ? (Guid)id_Customer : new Guid(Convert.ToString(Request.Form["customersList"]));
            projet.Pole_ID = new Guid(Convert.ToString(Request.Form["polesList"]));
            prtRep.AddProjet(projet);
            return RedirectToAction("AllProjets");
        }

        [Authorize]
        public ActionResult edit(Guid id)
        {
            ProjetRepositery prtRep = new ProjetRepositery();
            Projects projet = prtRep.GetById(id);
            ProjectsModel prjtModel = new ProjectsModel();
            prjtModel.Project_ID = projet.Project_ID;
            prjtModel.Name = projet.Name;
            prjtModel.Description = projet.Description;
            prjtModel.Budget = projet.Budget;
            prjtModel.Customer_ID = projet.Customer_ID;
            prjtModel.Pole_ID = projet.Pole_ID;
            return View("EditProject", prjtModel);
        }

        [Authorize]
        public ActionResult AllProjets(int? pageIndex)
        {
            ProjetRepositery prtRep = new ProjetRepositery();
            int countElementPage = 10;
            var projets = prtRep.allProjects();

            if (projets.Count() == 0)
            {
                ViewData["erreurMessage"] = "Aucun Projet!";
                ViewData["element"] = "Projet";
                ViewData["create"] = "true";
                return View("ErrorEmptyList");
            }
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

        [Authorize]
        public ActionResult updateProject(Guid id) {
            ProjetRepositery prtRep = new ProjetRepositery();
            ProjectsModel prjtModel = new ProjectsModel();
            Projects projet = prtRep.GetById(id);
            if (!ModelState.IsValidField("Name") || !ModelState.IsValidField("Budget"))
            {
                prjtModel.Project_ID = projet.Project_ID;
                prjtModel.Name = projet.Name;
                prjtModel.Description = projet.Description;
                prjtModel.CustomersList = prtRep.getAllCustomers().ToList();
                prjtModel.PolesList = prtRep.getAllPoles().ToList();
                return View("EditProject", prjtModel);
            }
            
            String name = Convert.ToString(Request.Form["Name"]);
            string description = Convert.ToString(Request.Form["Description"]);
            double budget = Convert.ToDouble(Request.Form["Budget"]);
            prtRep.updateProject(projet, name, description,budget);
            return RedirectToAction("AllProjets");

        }

        [Authorize]
        public ActionResult Searche(String query, int? pageIndex)
        {
            var countElementPage = 10;
            ProjetRepositery projetRep = new ProjetRepositery();
            var projets = projetRep.getSerachingProjects(query);
            List<ProjectsModel> projetsModel = new List<ProjectsModel>();
            CustomersModel customer = new CustomersModel();

            foreach (var prjt in projets)
            {
                ProjectsModel prjtModel = new ProjectsModel();
                prjtModel.Project_ID = prjt.Project_ID;
                prjtModel.Pole_ID = prjt.Pole_ID;
                prjtModel.Description = prjt.Description;
                prjtModel.Budget = prjt.Budget;
                prjtModel.Name = prjt.Name;
                customer.Name= projetRep.GetByIdCutomer(prjt.Customer_ID).Name;
                prjtModel.Customers = customer;
                projetsModel.Add(prjtModel);
            }
            IQueryable<ProjectsModel> listProjets = projetsModel.AsQueryable();
            PaginatedList<ProjectsModel> lst = new PaginatedList<ProjectsModel>(listProjets, pageIndex, countElementPage);
            return View("AllProjects", lst);
        }

        [Authorize]
        public ActionResult Delete(Guid id) {
            ProjetRepositery prjtRepo = new ProjetRepositery();
            Projects project = prjtRepo.GetById(id);
            ExpanseRepositery expRep = new ExpanseRepositery();
            ExpanseRepportRepositery expRepRep = new ExpanseRepportRepositery();
            List<Expanses> expList = expRep.GetByProject(id).ToList();
            foreach (var expanse in expList)
            {
                expRep.Delete(expanse);
            }
            expRep.Save();
            prjtRepo.Delete(project);
            prjtRepo.Save();
            return RedirectToAction("AllProjets");
        }

        [Authorize]
        public ActionResult confirmDelete(Guid id)
        {
            ViewData["confirmDelete"] = "/Projet/Delete?id=" + id;
            return PartialView("_confirmDelet");
        }
    }
}