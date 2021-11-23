using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class SubPolizaController : Controller
    {
        //
        // GET: /SubPoliza/
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.SubPoliza subpoliza = new ML.SubPoliza();
            ML.Result result = BL.SubPoliza.GetAll();

            if (result.Correct)
            {
                subpoliza.SubPolizas = result.Objects;
                return View(subpoliza);
            }
            else
            {
                ViewBag.Message = "Ocurrió un error al obtener la información" + result.ErrorMessage;
                return PartialView("ValidationModal");
            }

            // return View(empresa);
        }

        [HttpPost]
        public ActionResult Form(ML.SubPoliza subpoliza)
        {
            ML.Result result = new ML.Result();


            if (subpoliza.IdSubPoliza == 0)
            {
                result = BL.SubPoliza.Add(subpoliza);

                if (result.Correct)
                {
                    ViewBag.Mesaage = "Empresa agregada correctamente";
                }
                else
                {
                    ViewBag.Mesaage = "Ocurrio un error al ingresar la empresa";
                }
            }

            else
            {
                result = BL.SubPoliza.Update(subpoliza);
                if (result.Correct)
                {
                    ViewBag.Message = "Empresa actualizado correctamente";
                }
                else
                {
                    ViewBag.Message = "La empresa no pudo ser actualizada";

                }
            }

            if (!result.Correct)
            {
                ViewBag.Message = "No se pudo agregar correctamente la Empresa " + result.ErrorMessage;
            }

            return PartialView("ValidationModal");
        }

        [HttpGet]
        public ActionResult Form(byte IdSubPoliza)
        {
            ML.SubPoliza subpoliza = new ML.SubPoliza();

            if (IdSubPoliza == 0)
            {
                return View(subpoliza);
            }
            else
            {
                ML.Result result = BL.SubPoliza.GetById(IdSubPoliza);

                if (result.Correct)
                {

                    subpoliza.IdSubPoliza = ((ML.SubPoliza)result.Object).IdSubPoliza;
                    subpoliza.Nombre = ((ML.SubPoliza)result.Object).Nombre;

                    return View(subpoliza);


                }
                else
                {
                    ViewBag.Message = "Ocurrió un error al obtener la información" + result.ErrorMessage;
                    return PartialView("ValidationModal");
                }
            }

        }

        [HttpGet]
        public ActionResult Delete(byte IdSubPoliza)
        {
            ML.SubPoliza subpoliza = new ML.SubPoliza();
            subpoliza.IdSubPoliza = IdSubPoliza;

            ML.Result result = BL.SubPoliza.Delete(IdSubPoliza);

            if (result.Correct)
            {
                return RedirectToAction("GetAll");
            }
            else
            {
                ViewBag.Message = "Ocurrió un error al eliminar el producto " + result.ErrorMessage;
                return PartialView("ValidationModal");
            }
        }

	}
}