using Project_NotesDeFrais.Models;
using Project_NotesDeFrais.Models.Reposirery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_NotesDeFrais.Controllers
{
    
    public class TvasController : Controller
    {

        [Authorize]
        // GET: Tvas
        public ActionResult Index()
        {
            return View("TvasFormulaire");
        }

        [Authorize]
        public ActionResult createTvas(TvasModel tvaModel)
        {
            if (!ModelState.IsValid) {
                return View("TvasFormulaire", tvaModel);
            }
            TvasRepositery tvaRep = new TvasRepositery();
            Tvas tva = new Models.Tvas();
            tva.TVA_ID = Guid.NewGuid();
            tva.Name= Convert.ToString(Request.Form["Name"]);
            tva.Value = Convert.ToDouble(Request.Form["Value"]);
            tvaRep.AddTva(tva);
            return RedirectToAction("AllTvas");
        }

        [Authorize]
        public ActionResult edit(Guid id) {
            TvasRepositery tvaRep = new TvasRepositery();
            Tvas tva = tvaRep.tvasById(id);
            TvasModel tvaModel = new TvasModel();
            tvaModel.TVA_ID = tva.TVA_ID;
            tvaModel.Name = tva.Name;
            tvaModel.Value = tva.Value;
            return View("EditTvas" , tvaModel);
        }

        [Authorize]
        public ActionResult updateTvas(Guid id) {
            TvasRepositery tvaRep = new TvasRepositery();
            Tvas tva = tvaRep.tvasById(id);
            String name = Convert.ToString(Request.Form["Name"]);
            double value = Convert.ToDouble(Request.Form["Value"]);
            tvaRep.updateTvas(tva, name, value);
            return RedirectToAction("AllTvas");
        }

        [Authorize]
        public ActionResult AllTvas(int? pageIndex)
        {
            var countElementPage = 10;
            TvasRepositery tvaRep = new TvasRepositery();
            var tvas = tvaRep.allTvas();
            if (tvas.Count() == 0)
            {
                ViewData["erreurMessage"] = "Aucune Tva !";
                ViewData["element"] = "Tvas";
                ViewData["create"] = "true";
                return View("ErrorEmptyList");
            }
            List<TvasModel> TvasModel = new List<TvasModel>();

            foreach (var tva in tvas)
            {
                TvasModel tvaModel = new TvasModel();
                tvaModel.TVA_ID = tva.TVA_ID;
                tvaModel.Name = tva.Name;
                tvaModel.Value = tva.Value;
                TvasModel.Add(tvaModel);
            }
            IQueryable<TvasModel> listTvas = TvasModel.AsQueryable();
            PaginatedList<TvasModel> lst = new PaginatedList<TvasModel>(listTvas, pageIndex, countElementPage);
            return View("AllTvas", lst);
        }

        [Authorize]
        public ActionResult Searche(String query, int? pageIndex)
        {

            var countElementPage = 10;
         
            TvasRepositery tvaRepo = new TvasRepositery();
            var tvas = tvaRepo.getSerachingTvas(query);
            List<TvasModel> TvasModel = new List<TvasModel>();

            foreach (var tva in tvas)
            {
                TvasModel tvaModel = new TvasModel();
                tvaModel.TVA_ID = tva.TVA_ID;
                tvaModel.Name = tva.Name;
                tvaModel.Value = tva.Value;
                TvasModel.Add(tvaModel);
            }
            IQueryable<TvasModel> listTvas = TvasModel.AsQueryable();
            PaginatedList<TvasModel> lst = new PaginatedList<TvasModel>(listTvas, pageIndex, countElementPage);
            return View("AllTvas", lst);
        }

        [Authorize]
        public ActionResult Delete(Guid id) {
            ExpanseTypesRepositery expTypeRepo = new ExpanseTypesRepositery();
            List<ExpanseTypes> expTypes = expTypeRepo.getByTvaId(id).ToList();
            foreach (var expType in expTypes) {
                expTypeRepo.delete(expType);
            }
            expTypeRepo.Save();
            TvasRepositery tvaRepo = new TvasRepositery();
            Tvas tva = tvaRepo.tvasById(id);
            tvaRepo.delete(tva);
            tvaRepo.Save();
            return RedirectToAction("AllTvas");
        }

        [Authorize]
        public ActionResult confirmDelete(Guid id)
        {
            ViewData["confirmDelete"] = "/Tvas/Delete?id=" + id;
            return PartialView("_confirmDelet");
        }
    }
}