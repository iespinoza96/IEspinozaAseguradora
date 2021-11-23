using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML;

namespace BL
{
    public class SubPoliza
    {
        public static Result GetAll()
        {
            ML.Result result = new Result();

            try
            {
                using (DL.IEspinozaAseguradoraEntities context = new DL.IEspinozaAseguradoraEntities())
                {
                    var query = context.SubPolizaGetAll().ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.SubPoliza subpoliza = new ML.SubPoliza();

                            subpoliza.IdSubPoliza = obj.IdSubPoliza;
                            subpoliza.Nombre = obj.Nombre;

                            result.Objects.Add(subpoliza);
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

        public static ML.Result Add(ML.SubPoliza subpoliza)//Stored Procedure agregar datos Entity framework
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.IEspinozaAseguradoraEntities context = new DL.IEspinozaAseguradoraEntities())
                {
                    var restulQuery = context.SubPolizaAdd(subpoliza.Nombre);


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

        public static Result Update(ML.SubPoliza subpoliza)//stored procedure actualizar datos con Entety Framework
        {
            Result result = new Result();
            try
            {

                using (DL.IEspinozaAseguradoraEntities context = new DL.IEspinozaAseguradoraEntities())
                {
                    var query = context.SubPolizaUpdate(subpoliza.IdSubPoliza, subpoliza.Nombre);
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

        public static Result Delete(byte IdSubPoliza)
        {
            Result result = new Result();

            try
            {
                using (DL.IEspinozaAseguradoraEntities context = new DL.IEspinozaAseguradoraEntities())
                {

                    var query = context.SubPolizaDelete(IdSubPoliza);

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

        public static Result GetById(byte IdSubPoliza)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IEspinozaAseguradoraEntities context = new DL.IEspinozaAseguradoraEntities())
                {
                    var query = context.SubPolizaGetById(IdSubPoliza).FirstOrDefault();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        ML.SubPoliza subpoliza = new ML.SubPoliza();
                        subpoliza.IdSubPoliza = query.IdSubPoliza;
                        subpoliza.Nombre = query.Nombre;

                        result.Object = subpoliza;

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
