using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;

namespace PL.Controllers
{
    public class EmpleadoCargaMasivaController : Controller
    {
        // GET: EmpleadoCargaMasiva
        [HttpGet]
        public ActionResult CargaMasiva()
        {
            //ML.Empleado empleado = new ML.Empleado()
            ML.ErrorExcel errorExcel = new ML.ErrorExcel();

            errorExcel.Errores = new List<object>(); 
            errorExcel.Empleado = new ML.Empleado();
            errorExcel.Empleado.Empresa = new ML.Empresa();

            ML.Result result = BL.Empresa.GetAll();

            if (Session["RutaExcel"] != null)
            {
                errorExcel.Empleado.Empresa.Empresas = result.Objects;
                errorExcel.Correct = true;
            }
            return View(errorExcel);
        }

        [HttpPost]
        public ActionResult CargaMasiva(ML.ErrorExcel errorItem)
        {
            if (Session["RutaExcel"] != null) //Que ya tiene la ruta del archivo
            {
                string direccionExcel = (string)Session["RutaExcel"];
                string CadenaConexion = System.Configuration.ConfigurationManager.AppSettings["ConexionExcel"].ToString();
                string ConnectionString = CadenaConexion + direccionExcel;


                ML.Result resultDataTable = BL.Empleado.ConvertToDataTable(direccionExcel, ConnectionString);

                if (resultDataTable.Correct)
                {
                    string ErrorMessage = " ";
                    DataTable tableEmpleado = (DataTable)resultDataTable.Object;//unboxing
                    foreach (DataRow row in tableEmpleado.Rows)
                    {
                        ML.Empleado empleado = new ML.Empleado();

                        empleado.NumEmpleado = row[0].ToString();
                        empleado.RFC = row[1].ToString();
                        empleado.Nombre = row[2].ToString();
                        empleado.ApellidoPaterno = row[3].ToString();
                        empleado.ApellidoMaterno = row[4].ToString();
                        empleado.Email = row[5].ToString(); 
                        empleado.Telefono = row[6].ToString();
                        empleado.FechaNacimiento = row[7].ToString();
                        empleado.NSS = row[8].ToString();
                        empleado.FechaIngreso   = row[9].ToString();
                        empleado.Foto = null;
                        empleado.Empresa = new ML.Empresa();
                        empleado.Empresa.IdEmpresa = int.Parse(row[11].ToString());


                        ML.Result resultEmpleado = BL.Empleado.AddEF(empleado);

                        if (!resultEmpleado.Correct)
                        {
                            ErrorMessage += resultEmpleado.ErrorMessage;
                        }
                    }

                    if (ErrorMessage == "")
                    {
                        ViewBag.Message = "Los empleados han sido cargados correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrió un error al insertar los empleados" + ErrorMessage;
                    }

                }

                //Cargar el archivo
            }
            else
            {
                //Validar el archivo
                HttpPostedFileBase file = Request.Files["ExcelEmpleados"];

                if (file.ContentLength > 0)//Si el usuario seleccionó un excel
                {
                    string extension = Path.GetExtension(file.FileName).ToLower();
                    if (extension == ".xlsx")
                    {
                        string direccionExcel = Server.MapPath("~/ExcelCargaMasivaEmpleado/") + Path.GetFileNameWithoutExtension(file.FileName) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                        if (!System.IO.File.Exists(direccionExcel))
                        {
                            //string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + direccionExcel + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

                            try
                            {

                                file.SaveAs(direccionExcel);
                                Session["RutaExcel"] = direccionExcel;
                                string CadenaConexion = System.Configuration.ConfigurationManager.AppSettings["ConexionExcel"].ToString();
                                string ConnectionString = CadenaConexion + direccionExcel;

                                ML.Result resultDataTable = BL.Empleado.ConvertToDataTable(direccionExcel, ConnectionString);

                                if (resultDataTable.Correct)
                                {
                                    DataTable tableEmpleado = (DataTable)resultDataTable.Object;//unboxing
                                    ML.Result resultValidarExcel = BL.Empleado.ValidarExcel(tableEmpleado);
                                    if (!resultValidarExcel.Correct) //si hubo errores
                                    {
                                        ML.ErrorExcel error = new ML.ErrorExcel();
                                        error.Errores = resultValidarExcel.Objects;
                                        return View(error);
                                    }
                                    else
                                    {
                                        ViewBag.Message = "Todos los registros han sido validados correctamente, puede proceder a cargarlos";
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                ViewBag.Message = ex.Message;
                            }

                        }
                        else
                        {
                            ViewBag.Message = "Ya existe el nombre del archivo, por favor renombrarlo";
                        }

                    }
                    else
                    {
                        ViewBag.Message = "Seleccione un archivo con extensión .xlsx";
                    }
                }
                else
                {
                    ViewBag.Message = "Seleccione un archivo";
                }
            }
            return PartialView("ValidationModal");
        }
        public JsonResult GetEmpresa(int IdEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();

            empresa.IdEmpresa = 0;
            empresa.Nombre = "Seleccione una opción";

            var result = BL.Empresa.GetByIdEF(IdEmpresa);
            result.Objects.Insert(0, empresa);

            return Json(result.Objects, JsonRequestBehavior.AllowGet);


        }

    }


}