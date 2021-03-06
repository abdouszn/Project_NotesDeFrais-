﻿using Project_NotesDeFrais.Models;
using Project_NotesDeFrais.Models.Reposirery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_NotesDeFrais.Controllers
{
   
    public class ExpanseTypesController : Controller
    {
        [Authorize]
        // GET: ExpanseTypes
        public ActionResult Index()
        {
            ExpanseTypesModel expTypeModel = new ExpanseTypesModel();
            TvasRepositery tvaRepo = new TvasRepositery();
            var tvaLis = tvaRepo.allTvas().ToList();
            if (tvaLis.Count() == 0)
            {
                ViewData["erreur"] = "Tva";
                return View("ErrorEmptyElement");
            }
            expTypeModel.tvaList = tvaLis;
            return View("ExpansTypeFormulaire" , expTypeModel);
        }

        [Authorize]
        public ActionResult createExpansTypes(ExpanseTypesModel expansTypeModel)
        {
            if (Convert.ToBoolean(Request.Form["Fixed"]) == true) {

            }
            if (!ModelState.IsValid) {
                TvasRepositery tvaRepo = new TvasRepositery();
                var tvaLis = tvaRepo.allTvas().ToList();
                expansTypeModel.tvaList = tvaLis;
                return View("ExpansTypeFormulaire" , expansTypeModel);
            }
            ExpanseTypes expansType = new ExpanseTypes();
            ExpanseTypesRepositery expTypeRep = new ExpanseTypesRepositery();
            expansType.ExpenseType_ID = Guid.NewGuid();
            expansType.Name = Convert.ToString(Request.Form["Name"]);
            if (Request.Form["Ceiling"] == null || string.IsNullOrWhiteSpace(Request.Form["Ceiling"]))
            {
                expansType.Ceiling = 0;
            }
            else {
                expansType.Ceiling = Convert.ToDouble(Request.Form["Ceiling"]);
            }
            expansType.Fixed = Convert.ToBoolean(Request.Form["Fixed"]);
            expansType.OnlyManagers= Convert.ToBoolean(Request.Form["OnlyManagers"]);
            expansType.Tva_ID = new Guid(Convert.ToString(Request.Form["tvaSelect"]));
            expTypeRep.AddExpanseType(expansType);
            return RedirectToAction("AllExpanseTypes");
        }

        [Authorize]
        public ActionResult edit(Guid id) {
            
            ExpanseTypesRepositery expTypeRep = new ExpanseTypesRepositery();
            ExpanseTypes expTypes = expTypeRep.getById(id);
            ExpanseTypesModel expTypeModel = new ExpanseTypesModel();
            expTypeModel.ExpenseType_ID = expTypes.ExpenseType_ID;
            expTypeModel.Name = expTypes.Name;
            expTypeModel.Ceiling = expTypes.Ceiling;
            expTypeModel.Fixed = expTypes.Fixed;
            expTypeModel.OnlyManagers = expTypes.OnlyManagers;
            return View("EditExpansesTypes", expTypeModel);
        }

        [Authorize]
        public ActionResult update(Guid id)
        {
            ExpanseTypesRepositery expTypeRep = new ExpanseTypesRepositery();
            ExpanseTypes expTypes = expTypeRep.getById(id);
            ExpanseTypesModel expTypeModel = new ExpanseTypesModel();
            if (!ModelState.IsValid)
            {
                TvasRepositery tvaRepo = new TvasRepositery();
                var tvaLis = tvaRepo.allTvas().ToList();
                expTypeModel.tvaList = tvaLis;
                expTypeModel.ExpenseType_ID = expTypes.ExpenseType_ID;
                expTypeModel.Name = expTypes.Name;
                expTypeModel.Ceiling = expTypes.Ceiling;
                expTypeModel.Fixed = expTypes.Fixed;
                expTypeModel.OnlyManagers = expTypes.OnlyManagers;
                return View("EditExpansesTypes", expTypeModel);
            }
            String name = Convert.ToString(Request.Form["Name"]);
            double ceiling = Convert.ToDouble(Request.Form["Ceiling"]);
            Boolean fixe = Convert.ToBoolean(Request.Form["Fixed"]);
            Boolean OnlyManagers = Convert.ToBoolean(Request.Form["OnlyManagers"]);
            expTypeRep.updateExpanseType(expTypes, name, ceiling, fixe, OnlyManagers);
            return RedirectToAction("AllExpanseTypes");
        }

        [Authorize]
        public ActionResult AllExpanseTypes(int? pageIndex)
        {
            var countElementPage = 10;
            TvasRepositery tvaRepo = new TvasRepositery();
            TvasModel tvaModel = new TvasModel();
            ExpanseTypesRepositery expTypeRep = new ExpanseTypesRepositery();
            var expanseTypes= expTypeRep.allExpanseTypes();
            if (expanseTypes.Count() == 0)
            {
                ViewData["erreurMessage"] = "Aucun type de frais !";
                ViewData["create"] = "true";
                ViewData["element"] = "ExpanseTypes";
                return View("ErrorEmptyList");
            }
            if (User.IsInRole("manager")) {
                expanseTypes = expTypeRep.allExpanseTypesManager();
            }
            
            List<ExpanseTypesModel> expanseTypesModel = new List<ExpanseTypesModel>();
            foreach (var expTpe in expanseTypes)
            {
                ExpanseTypesModel expenseTypeModel = new ExpanseTypesModel();
                expenseTypeModel.ExpenseType_ID= expTpe.ExpenseType_ID;
                expenseTypeModel.Name = expTpe.Name;
                expenseTypeModel.Ceiling = expTpe.Ceiling;
                expenseTypeModel.Fixed = expTpe.Fixed;
                expenseTypeModel.OnlyManagers = expTpe.OnlyManagers;
                tvaModel.Name = tvaRepo.tvasById(expTpe.Tva_ID).Name;
                tvaModel.TVA_ID = tvaRepo.tvasById(expTpe.Tva_ID).TVA_ID;
                tvaModel.Value = tvaRepo.tvasById(expTpe.Tva_ID).Value;
                expenseTypeModel.Tvas = tvaModel;
                expanseTypesModel.Add(expenseTypeModel);
            }
            IQueryable<ExpanseTypesModel> listEpanTypes = expanseTypesModel.AsQueryable();
            PaginatedList<ExpanseTypesModel> lst = new PaginatedList<ExpanseTypesModel>(listEpanTypes, pageIndex, countElementPage);
            return View("AllExpansesTypes", lst);
        }

        [Authorize]
        public String cellingTvaById(Guid expanseTypeID) {
            double celling = 0;
            String cellingTva = null;
            ExpanseTypesRepositery expTypeRep = new ExpanseTypesRepositery();
            var expAnseType = expTypeRep.getById(expanseTypeID);
            double tva = (double)expAnseType.Tvas.Value;

            if (expAnseType.Fixed == true) {
                celling = (double)expAnseType.Ceiling;
            }
            cellingTva = Convert.ToString(celling) +"-"+Convert.ToString(tva);

            return cellingTva;
        }

        [Authorize]
        public ActionResult Searche(String query, int? pageIndex)
        {

            var countElementPage = 10;
            ExpanseTypesRepositery expTypeRep = new ExpanseTypesRepositery();
            TvasRepositery tvaRepo = new TvasRepositery();
            TvasModel tvaModel = new TvasModel();
            var expanseTypes = expTypeRep.getSerachingExpanses(query);
            List<ExpanseTypesModel> expanseTypesModel = new List<ExpanseTypesModel>();
            foreach (var expTpe in expanseTypes)
            {
                ExpanseTypesModel expenseTypeModel = new ExpanseTypesModel();
                expenseTypeModel.ExpenseType_ID = expTpe.ExpenseType_ID;
                expenseTypeModel.Name = expTpe.Name;
                expenseTypeModel.Ceiling = expTpe.Ceiling;
                expenseTypeModel.Fixed = expTpe.Fixed;
                expenseTypeModel.OnlyManagers = expTpe.OnlyManagers;
                tvaModel.Name = tvaRepo.tvasById(expTpe.Tva_ID).Name;
                tvaModel.TVA_ID = tvaRepo.tvasById(expTpe.Tva_ID).TVA_ID;
                tvaModel.Value = tvaRepo.tvasById(expTpe.Tva_ID).Value;
                expenseTypeModel.Tvas = tvaModel;
                expanseTypesModel.Add(expenseTypeModel);
            }
            IQueryable<ExpanseTypesModel> listEpanTypes = expanseTypesModel.AsQueryable();
            PaginatedList<ExpanseTypesModel> lst = new PaginatedList<ExpanseTypesModel>(listEpanTypes, pageIndex, countElementPage);
            return View("AllExpansesTypes", lst);
        }

        [Authorize]
        public ActionResult Delete(Guid id) {
            ExpanseTypesRepositery expTypeRepo = new ExpanseTypesRepositery();
            ExpanseRepositery expRep = new ExpanseRepositery();
            List<Expanses> expList = expRep.GetExpansesByIdExpanseTypes(id).ToList();
            foreach (var expanse in expList)
            {
                expRep.Delete(expanse);
            }
            expRep.Save();
            ExpanseTypes expanseType = expTypeRepo.getById(id);
            expTypeRepo.delete(expanseType);
            expTypeRepo.Save();
            return RedirectToAction("AllExpanseTypes");
        }

        [Authorize]
        public ActionResult confirmDelete(Guid id)
        {
            ViewData["confirmDelete"] = "/ExpanseTypes/Delete?id=" + id;
            return PartialView("_confirmDelet");
        }
    }
}