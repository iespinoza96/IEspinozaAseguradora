using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class PolizaController : Controller
    {
        //
        // GET: /Poliza/
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Poliza poliza = new ML.Poliza();
            ML.Usuario usuario = new ML.Usuario();
            ML.Vigencia vigencia = new ML.Vigencia();
            ML.Result result = BL.Poliza.GetAll();

            if (result.Correct)
            {
                poliza.Polizas = result.Objects;
                usuario.Usuarios = result.Objects;
                vigencia.Vigencias = result.Objects;
                return View(poliza);
            }
            else
            {
                ViewBag.Message = "Ocurrió un error al obtener la información" + result.ErrorMessage;
                return PartialView("ValidationModal");
            }

            // return View(empresa);
        }
	}
}