using Project_NotesDeFrais.Models;
using Project_NotesDeFrais.Models.Reposirery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_NotesDeFrais.Controllers
{
    public class PolesController : Controller
    {
        // GET: Poles
        public ActionResult Index()
        {
            return View("PoleFormulaire");
        }

        public void createPole(Poles pole)
        {
            PolesRepository polRep = new PolesRepository();
            pole.Pole_ID = Guid.NewGuid();
            pole.Name = Convert.ToString(Request.Form["Name"]);
            pole.Manager_ID = polRep.maxIdEmployer();
            polRep.AddPoles(pole);
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
            String name = Convert.ToString(Request.Form["Name"]);
            Poles pole= polRep.GetById(id);
            polRep.updatePole(pole , name);
            return RedirectToAction("AllPoles");
        }


        public ActionResult AllPoles(int? pageIndex)
        {
            PolesRepository polRep = new PolesRepository();
            var countElementPage = 10;
            var poles = polRep.allPoles();
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
    }
}