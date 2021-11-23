using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();

            ML.Result result = BL.Usuario.GetAll();

   
                if (result.Correct)
                {
                    usuario.Usuarios = result.Objects;
                    return View(usuario);
                }
                else
                {
                    ViewBag.Message = "Ocurrió un error al obtener la información" + result.ErrorMessage;
                    return PartialView("ValidationModal");
                }



        }
	}
}