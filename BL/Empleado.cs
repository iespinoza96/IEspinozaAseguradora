using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
//using System.Data.Objects;
using ML;

namespace BL
{
    public class Empleado
    {
        public static Result GetAll(ML.Empleado empleado)
        {
            ML.Result result = new Result();

            try
            {
                using (DL.IEspinozaAseguradoraEntities context = new DL.IEspinozaAseguradoraEntities())
                {
                    var query = context.EmpleadoGetAll(empleado.Empresa.IdEmpresa,empleado.Nombre,empleado.ApellidoPaterno,empleado.ApellidoMaterno).ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            empleado = new ML.Empleado();

                            empleado.NumEmpleado = obj.NumEmpleado;
                            empleado.RFC = obj.RFC;
                            empleado.Nombre = obj.Nombre;
                            empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado.ApellidoMaterno = obj.ApellidoMaterno;
                            empleado.Email = obj.Email; 
                            empleado.Telefono = obj.Telefono;
                            empleado.FechaNacimiento = obj.FechaNacimiento.ToString();
                            empleado.NSS = obj.NSS;
                            empleado.FechaIngreso = obj.FechaIngreso.ToString();
                            empleado.Foto = obj.Foto;
                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.IdEmpresa = obj.IdEmpresa;
                            empleado.Empresa.Nombre = obj.EmpresaNombre;
                            

                            result.Objects.Add(empleado);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
                    }

                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
            }
            return result;
        }// GetAll 

        public static ML.Result AddEF(ML.Empleado empleado)//Stored Procedure agregar datos Entity framework
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.IEspinozaAseguradoraEntities context = new DL.IEspinozaAseguradoraEntities())
                {
                    var restulQuery = context.EmpleadoAdd(empleado.NumEmpleado,empleado.RFC,empleado.Nombre,empleado.ApellidoPaterno,empleado.ApellidoMaterno, empleado.Email, empleado.Telefono, empleado.FechaNacimiento, empleado.NSS,empleado.FechaIngreso,empleado.Foto,empleado.Empresa.IdEmpresa);


                    if (restulQuery >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se insertó el registro";
                    }

                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static Result UpdateEF(ML.Empleado empleado)//stored procedure actualizar datos con Entety Framework
        {
            Result result = new Result();
            try
            {

                using (DL.IEspinozaAseguradoraEntities context = new DL.IEspinozaAseguradoraEntities())
                {
                    var query = context.EmpleadoUpdate(empleado.NumEmpleado,empleado.RFC,empleado.Nombre,empleado.ApellidoPaterno,empleado.ApellidoMaterno, empleado.Email, empleado.Telefono, empleado.FechaNacimiento, empleado.NSS,empleado.FechaIngreso,empleado.Foto,empleado.Empresa.IdEmpresa);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizó el status de la credencial";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static Result DeleteEF(string NumEmpleado)
        {
            Result result = new Result();

            try
            {
                using (DL.IEspinozaAseguradoraEntities context = new DL.IEspinozaAseguradoraEntities())
                {

                    var query = context.EmpleadoDelete(NumEmpleado);

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se eliminó el registro";
                    }

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }//stored procedure borrar registro con EntityFramework

        public static Result GetByIdEF(string NumEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IEspinozaAseguradoraEntities context = new DL.IEspinozaAseguradoraEntities())
                {
                    var query = context.EmpleadoGetById(NumEmpleado).FirstOrDefault();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        ML.Empleado empleado = new ML.Empleado();

                        empleado.NumEmpleado = query.NumEmpleado;
                        empleado.RFC = query.RFC;
                        empleado.Nombre = query.Nombre;
                        empleado.ApellidoPaterno = query.ApellidoPaterno;
                        empleado.ApellidoMaterno = query.ApellidoMaterno;
                        empleado.Email = query.Email;
                        empleado.Telefono = query.Telefono;
                        empleado.FechaNacimiento = query.FechaNacimiento.ToString();
                        empleado.NSS = query.NSS;
                        empleado.FechaIngreso = query.FechaIngreso.ToString();
                        empleado.Foto = query.Foto;
                        empleado.Empresa = new ML.Empresa();
                        empleado.Empresa.IdEmpresa = query.IdEmpresa.Value;

                        result.Object = empleado;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }

        public static Result ConvertToDataTable(string strFilePath, string connString)
        {
            Result result = new Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connString))
                {
                    string query = "SELECT * FROM [Hoja1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;


                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;
                        DataTable tableEmpleado = new DataTable();

                        da.Fill(tableEmpleado);

                        result.Object = tableEmpleado;

                        if (tableEmpleado.Rows.Count > 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en el excel";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;

        }


        public static Result ValidarExcel(DataTable tableEmpleado)
        {
            Result result = new Result();

            try
            {
                result.Objects = new List<object>();
                //DataTable  //Rows //Columns
                foreach (DataRow row in tableEmpleado.Rows)
                {
                    ErrorExcel error = new ErrorExcel();

                    if (row[0].ToString() == "")
                    {
                        error.Message += "Por favor ingrese el número de empleado. ";
                    }
                    if (row[1].ToString() == "")
                    {
                        error.Message += "Por favor ingrese el RFC del empleado. ";
                    }
                    if (row[2].ToString() == "")
                    {
                        error.Message += "Por favor ingrese el Nombre del empleado. ";
                    }
                    if (row[3].ToString() == "")
                    {
                        error.Message += "Por favor ingrese el Apellido Paterno del empleado. ";
                    }
                    if (row[4].ToString() == "")
                    {
                        error.Message += "Por favor ingrese el Apellido Materno del empleado. ";
                    }
                    if (row[5].ToString() == "")
                    {
                        error.Message += "Por favor ingrese el Email del empleado. ";
                    }
                    if (row[6].ToString() == "")
                    {
                        error.Message += "Por favor ingrese el Télefono del empleado. ";
                    }
                    if (row[7].ToString() == "")
                    {
                        error.Message += "Por favor ingrese la Fecha de nacimiento del empleado. ";
                    }
                    if (row[8].ToString() == "")
                    {
                        error.Message += "Por favor ingrese el NSS del empleado. ";
                    }
                    if (row[9].ToString() == "")
                    {
                        error.Message += "Por favor ingrese la Fecha de ingreso del empleado. ";
                    }

                    if (error.Message != null)
                    {
                        result.Objects.Add(error);
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;

        }

    }
}
