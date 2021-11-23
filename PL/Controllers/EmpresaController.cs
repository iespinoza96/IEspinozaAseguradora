using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class EmpresaController : Controller
    {
        //
        // GET: /Empresa/
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Empresa empresa = new ML.Empresa();
            ML.Result result = BL.Empresa.GetAll();

            if (result.Correct)
            {
                empresa.Empresas = result.Objects;
                return View(empresa);
            }
            else
            {
                ViewBag.Message = "Ocurrió un error al obtener la información" + result.ErrorMessage;
                return PartialView("ValidationModal");
            }

           // return View(empresa);
        }

        [HttpGet]
        public ActionResult Form(int? IdEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();

            if (empresa.IdEmpresa == 0)
            {
                return View(empresa);
            }
            else
            {
                ML.Result result = BL.Empresa.GetByIdEF(empresa.IdEmpresa);

                if (result.Correct)
                {
                   
                    empresa.IdEmpresa = ((ML.Empresa)result.Object).IdEmpresa;
                    empresa.Nombre = ((ML.Empresa)result.Object).Nombre;
                    empresa.Telefono = ((ML.Empresa)result.Object).Telefono;
                    empresa.Email = ((ML.Empresa)result.Object).Email;
                    empresa.DireccionWeb = ((ML.Empresa)result.Object).DireccionWeb;
                    empresa.Logo = ((ML.Empresa)result.Object).Logo;

                    return View(empresa);


                }
                else
                {
                    ViewBag.Message = "Ocurrió un error al obtener la información" + result.ErrorMessage;
                    return PartialView("ValidationModal");
                }
            }
            
        }

        [HttpPost]
        public ActionResult Form(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();

            HttpPostedFileBase file = Request.Files["ImagenData"];

            if (file.ContentLength > 0)
            {
                empresa.Logo = ConvertToBytes(file);
            }

            if (empresa.IdEmpresa == 0)
            {
              result = BL.Empresa.AddEF(empresa);

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
                result = BL.Empresa.UpdateEF(empresa);
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
        public ActionResult Delete(int IdEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();
            empresa.IdEmpresa = IdEmpresa;

            ML.Result result = BL.Empresa.DeleteEF(IdEmpresa);

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
        public byte[] ConvertToBytes(HttpPostedFileBase Imagen)
        {
            byte[] data = null;
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Imagen.InputStream);
            data = reader.ReadBytes((int)Imagen.ContentLength);

            return data;
        }
	}
}