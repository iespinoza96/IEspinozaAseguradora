using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML;

namespace BL
{
    public class Empresa
    {
        public static Result GetAll()
        {
            ML.Result result = new Result();

            try
            {
                using (DL.IEspinozaAseguradoraEntities context = new DL.IEspinozaAseguradoraEntities())
                {
                    var query = context.EmpresaGetAll().ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Empresa empresa = new ML.Empresa();

                            empresa.IdEmpresa = obj.IdEmpresa;
                            empresa.Nombre = obj.Nombre;
                            empresa.Telefono = obj.Telefono;
                            empresa.Email = obj.Email;
                            empresa.DireccionWeb = obj.DireccionWeb;
                            empresa.Logo = obj.Logo;

                            result.Objects.Add(empresa);
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

        public static ML.Result AddEF(ML.Empresa empresa)//Stored Procedure agregar datos Entity framework
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.IEspinozaAseguradoraEntities context = new DL.IEspinozaAseguradoraEntities())
                {
                    var restulQuery = context.EmpresaAdd(empresa.Nombre, empresa.Telefono,empresa.Email,empresa.DireccionWeb,empresa.Logo);


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

        public static Result UpdateEF(ML.Empresa empresa)//stored procedure actualizar datos con Entety Framework
        {
            Result result = new Result();
            try
            {

                using (DL.IEspinozaAseguradoraEntities context = new DL.IEspinozaAseguradoraEntities())
                {
                    var query = context.EmpresaUpdate(empresa.IdEmpresa,empresa.Nombre, empresa.Telefono, empresa.Email, empresa.DireccionWeb, empresa.Logo);
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

        public static Result DeleteEF(int IdEmpresa)
        {
            Result result = new Result();

            try
            {
                using (DL.IEspinozaAseguradoraEntities context = new DL.IEspinozaAseguradoraEntities())
                {

                    var query = context.EmpresaDelete(IdEmpresa);

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

        public static Result GetByIdEF(int IdEmpresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IEspinozaAseguradoraEntities context = new DL.IEspinozaAseguradoraEntities())
                {
                    var query = context.EmpresaGetById(IdEmpresa).FirstOrDefault();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        ML.Empresa empresa = new ML.Empresa();
                        empresa.IdEmpresa = query.IdEmpresa;
                        empresa.Nombre = query.Nombre;
                        empresa.Telefono = query.Telefono;
                        empresa.Email = query.Email;
                        empresa.DireccionWeb = query.DireccionWeb;
                        empresa.Logo = query.Logo;

                        result.Object = empresa;

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
 
    }
}
