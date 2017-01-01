using Project_NotesDeFrais.Models;
using Project_NotesDeFrais.Models.Reposirery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_NotesDeFrais.Controllers
{
    [Authorize]
    public class PolesController : Controller
    {
        // GET: Poles
        public ActionResult Index()
        {
            PolesModel pole = new PolesModel();
            EmployesRepositery empRep = new EmployesRepositery();
            EmployeesModel empModel = new EmployeesModel();
            var lisEmp = empRep.allEmployees();
            if (lisEmp.ToList().Count() == 0)
            {
                ViewData["erreur"] = "Employers";
                return View("ErrorEmptyElement");
            }
            foreach (var emp in lisEmp) {
                empModel.Employee_ID = emp.Employee_ID;
                empModel.FirstName = emp.FirstName;
                pole.Employees.Add(empModel);
            }
            
            return View("PoleFormulaire" , pole);
        }

        public ActionResult createPole(PolesModel poleModel)
        {
            if (!ModelState.IsValid) {
                return View("PoleFormulaire", poleModel);
            }
            Poles pole = new Poles();
            PolesRepository polRep = new PolesRepository();
            pole.Pole_ID = Guid.NewGuid();
            pole.Name = Convert.ToString(Request.Form["Name"]);
            pole.Manager_ID = new Guid(Convert.ToString(Request.Form["managerSelect"]));
            polRep.AddPoles(pole);
            return RedirectToAction("AllPoles");
        }

        public ActionResult Edit(Guid id)
        {
            PolesRepository polRep = new PolesRepository();
            PolesModel poleModel = new PolesModel();
            Poles pole = polRep.GetById(id);
            poleModel.Pole_ID = pole.Pole_ID;
            poleModel.Name = pole.Name;
            poleModel.Manager_ID = pole.Manager_ID;
            return View("EditPoles" , poleModel); 
        }

        public ActionResult updatePole(Guid id)
        {
            PolesRepository polRep = new PolesRepository();
            Poles pole = polRep.GetById(id);
            if (!ModelState.IsValid)
            {
                PolesModel poleModel = new PolesModel();
                poleModel.Pole_ID = pole.Pole_ID;
                poleModel.Name = pole.Name;
                poleModel.Manager_ID = pole.Manager_ID;
                return View("EditPoles", poleModel);
            }
                String name = Convert.ToString(Request.Form["Name"]);
           
            polRep.updatePole(pole , name);
            return RedirectToAction("AllPoles");
        }


        public ActionResult AllPoles(int? pageIndex)
        {
            PolesRepository polRep = new PolesRepository();
            var countElementPage = 10;
            var poles = polRep.allPoles();
            if (poles.Count() == 0)
            {
                ViewData["erreurMessage"] = "Aucun pole!";
                ViewData["element"] = "Poles";
                ViewData["create"] = "true";
                return View("ErrorEmptyList");
            }
            List<PolesModel> ploesModel = new List<PolesModel>();

            foreach (var pol in poles)
            {
                PolesModel polModel = new PolesModel();
                polModel.Pole_ID = pol.Pole_ID;
                polModel.Name = pol.Name;
                ploesModel.Add(polModel);
            }
            IQueryable<PolesModel> listPoles = ploesModel.AsQueryable();
            PaginatedList<PolesModel> lst = new PaginatedList<PolesModel>(listPoles, pageIndex, countElementPage);
            return View("AllPoles", lst);
        }

        public ActionResult Searche(String query, int? pageIndex)
        {
            var countElementPage = 10;
            PolesRepository polRep = new PolesRepository();
            var poles = polRep.getSerachingPoles(query);
            List<PolesModel> polesModel = new List<PolesModel>();

            foreach (var pl in poles)
            {
                PolesModel poleModel = new PolesModel();
                poleModel.Pole_ID = pl.Pole_ID;
                poleModel.Name = pl.Name;
                poleModel.Manager_ID = pl.Manager_ID;
                polesModel.Add(poleModel);
            }
            IQueryable<PolesModel> listPoles = polesModel.AsQueryable();
            PaginatedList<PolesModel> lst = new PaginatedList<PolesModel>(listPoles, pageIndex, countElementPage);
            return View("AllPoles", lst);
        }

        public ActionResult Delete(Guid id) {
            PolesRepository poleRep = new PolesRepository();
            ProjetRepositery prjtRepo = new ProjetRepositery();
            ExpanseRepositery expRep = new ExpanseRepositery();
            EmployesRepositery empRepo = new EmployesRepositery();
            ExpanseRepportRepositery expRepRep = new ExpanseRepportRepositery();
            List<Projects> projets = prjtRepo.GetByIdPole(id).ToList();
            foreach (var pro in projets)
            {
                List<Expanses> expList = expRep.GetByProject(pro.Project_ID).ToList();
                foreach (var expanse in expList)
                {
                    expRep.Delete(expanse);
                }
                expRep.Save();
                prjtRepo.Delete(pro);
            }
            prjtRepo.Save();
            foreach (var empl in empRepo.getByIdPole(id)) {
                List<ExpanseReports> expanseReports = expRepRep.GetByEmployesId(empl.Employee_ID).ToList();
                foreach (var expRepo in expanseReports) {
                    expRepRep.Delete(expRepo);
                }
                expRepRep.Save();
                empRepo.Delete(empl);
            }
            empRepo.Save();
            Poles pole = poleRep.GetById(id);
            poleRep.Delete(pole);
            poleRep.Save();
            return RedirectToAction("AllPoles");
        }
    }
}