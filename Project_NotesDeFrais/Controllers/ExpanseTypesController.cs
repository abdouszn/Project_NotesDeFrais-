using Project_NotesDeFrais.Models;
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
        // GET: ExpanseTypes
        public ActionResult Index()
        {
            return View("ExpansTypeFormulaire");
        }


        public void createExpansTypes(ExpanseTypes expansType)
        {
            ExpanseTypesRepositery expTypeRep = new ExpanseTypesRepositery();
            expansType.ExpenseType_ID = Guid.NewGuid();
            expansType.Name = Convert.ToString(Request.Form["Name"]);
            expansType.Ceiling = Convert.ToDouble(Request.Form["Ceiling"]);
            expansType.Fixed = Convert.ToBoolean(Request.Form["Fixed"]);
            expansType.OnlyManagers= Convert.ToBoolean(Request.Form["OnlyManagers"]);
            expansType.Tva_ID = Guid.NewGuid();
            expTypeRep.AddExpanseType(expansType);
        }


        public ActionResult AllExpanseTypes()
        {
            ExpanseTypesRepositery expTypeRep = new ExpanseTypesRepositery();
            var expanseTypes = expTypeRep.allExpanseTypes();
            List<ExpanseTypesModel> expanseTypesModel = new List<ExpanseTypesModel>();
            foreach (var expTpe in expanseTypes)
            {
                ExpanseTypesModel expenseTypeModel = new ExpanseTypesModel();
                expenseTypeModel.ExpenseType_ID= expTpe.ExpenseType_ID;
                expenseTypeModel.Name = expTpe.Name;
                expenseTypeModel.Ceiling = expTpe.Ceiling;
                expenseTypeModel.Fixed = expTpe.Fixed;
                expenseTypeModel.OnlyManagers = expTpe.OnlyManagers;
                expenseTypeModel.Tva_ID = expTpe.Tva_ID;
                expanseTypesModel.Add(expenseTypeModel);
            }
            IQueryable<ExpanseTypesModel> listEpanTypes = expanseTypesModel.AsQueryable();
            return View("AllExpansesTypes", listEpanTypes);
        }
    }
}