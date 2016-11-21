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
    }
}