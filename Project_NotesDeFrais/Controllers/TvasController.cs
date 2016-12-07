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
        // GET: Tvas
        public ActionResult Index()
        {
            return View("TvasFormulaire");
        }

        public void createTvas(Tvas tva)
        {
            TvasRepositery tvaRep = new TvasRepositery();
            tva.TVA_ID = Guid.NewGuid();
            tva.Name= Convert.ToString(Request.Form["Name"]);
            tva.Value = Convert.ToDouble(Request.Form["Value"]);
            tvaRep.AddTva(tva);
        }


        public ActionResult AllTvas(int? pageIndex)
        {
            var countElementPage = 10;
            TvasRepositery tvaRep = new TvasRepositery();
            var tvas = tvaRep.allTvas();
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
    }
}