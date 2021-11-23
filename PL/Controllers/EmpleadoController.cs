using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        //
        // GET: /Empleado/
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();

            empleado.Nombre = "";
            empleado.ApellidoPaterno = "";
            empleado.ApellidoMaterno = "";
            empleado.Empresa = new ML.Empresa();
            empleado.Empresa.IdEmpresa = 0;

            ML.Result result = BL.Empleado.GetAll(empleado);
            ML.Result resultempresa = BL.Empresa.GetAll();

            if (resultempresa.Correct)
            {            
                if (result.Correct)
                {
                    empleado.Empleados = result.Objects;
                    return View(empleado);
                }
                else
                {
                    ViewBag.Message = "Ocurrió un error al obtener la información" + result.ErrorMessage;
                    return PartialView("ValidationModal");
                }

            }
            else
            {
                ViewBag.Message = "Ocurrio un error al obtener la informacion" + resultempresa.ErrorMessage;
                return PartialView("ValidationModal");
            }


        }

        [HttpPost]
        public ActionResult GetAll(ML.Empleado empleado)
        {
            empleado.Nombre =  "";
            empleado.ApellidoPaterno =  "" ;
            empleado.ApellidoMaterno = "" ;
            empleado.Empresa = new ML.Empresa();
            empleado.Empresa.IdEmpresa = 0;

            ML.Result result = BL.Empleado.GetAll(empleado);          
                
                if (result.Correct)
                {
                    empleado.Empleados = result.Objects;
                    return View(empleado);
                }
                else
                {
                    ViewBag.Message = "Ocurrió un error al obtener la información" + result.ErrorMessage;
                    return PartialView("ValidationModal");
                }

 

        }

        [HttpPost]

       public ActionResult Form(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            HttpPostedFileBase file = Request.Files["ImagenData"];

            if (file.ContentLength > 0)
            {
                empleado.Foto = ConvertToBytes(file);
            }

            if (empleado.NumEmpleado == null)
            {
              result = BL.Empleado.AddEF(empleado);

                    if (result.Correct)
                    {
                        ViewBag.Mesaage = "Empleado agregada correctamente";
                    }
                    else
                    {
                        ViewBag.Mesaage = "Ocurrio un error al ingresar al empleado";
                    }
            }
            
            else
            {
                result = BL.Empleado.UpdateEF(empleado);
                if (result.Correct)
                {
                    ViewBag.Message = "Empleado actualizado correctamente";
                }
                else
                {
                    ViewBag.Message = "El empleadp no pudo ser actualizada";

                }
            }

            if (!result.Correct)
            {
                ViewBag.Message = "No se pudo agregar correctamente al empleado " + result.ErrorMessage;
            }

            return PartialView("ValidationModal");
        }

        [HttpGet]
        public ActionResult Form(string NumEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            ML.Result resultEmpresa = BL.Empresa.GetAll();

            if (resultEmpresa.Correct)
            {
                empleado.Empresa = new ML.Empresa();
            }

            if (NumEmpleado == null)
            {
                empleado.Action = "Add";
                return View(empleado);
            }
            else
            {
                ML.Result result = BL.Empleado.GetByIdEF(NumEmpleado);

                if (result.Correct)
                {
                    empleado.Action = "Update";

                    empleado.NumEmpleado = ((ML.Empleado)result.Object).NumEmpleado;
                    empleado.RFC = ((ML.Empleado)result.Object).RFC;
                    empleado.Nombre = ((ML.Empleado)result.Object).Nombre;
                    empleado.ApellidoPaterno = ((ML.Empleado)result.Object).ApellidoPaterno;
                    empleado.ApellidoMaterno = ((ML.Empleado)result.Object).ApellidoMaterno;
                    empleado.Email = ((ML.Empleado)result.Object).Email;
                    empleado.Telefono = ((ML.Empleado)result.Object).Telefono;
                    empleado.FechaNacimiento = ((ML.Empleado)result.Object).FechaNacimiento;
                    empleado.NSS = ((ML.Empleado)result.Object).NSS;
                    empleado.FechaIngreso = ((ML.Empleado)result.Object).FechaIngreso;
                    empleado.Foto = ((ML.Empleado)result.Object).Foto;
                    empleado.Empresa = new ML.Empresa();
                    empleado.Empresa.IdEmpresa = ((ML.Empleado)result.Object).Empresa.IdEmpresa;


                    return View(empleado);


                }
                else
                {
                    ViewBag.Message = "Ocurrió un error al obtener la información" + result.ErrorMessage;
                    return PartialView("ValidationModal");
                }
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