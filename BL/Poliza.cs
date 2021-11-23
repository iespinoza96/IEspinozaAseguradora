using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML;

namespace BL
{
    public class Poliza
    {
        public static Result GetAll()
        {
            ML.Result result = new Result();

            try
            {
                using (DL.IEspinozaAseguradoraEntities context = new DL.IEspinozaAseguradoraEntities())
                {
                    var query = context.PolizaGetAll().ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Poliza poliza = new ML.Poliza();

                            poliza.IdPoliza = obj.IdPoliza;
                            poliza.Nombre = obj.PolizaNombre;
                            poliza.NumeroPolzia = obj.NumeroPoliza;
                            poliza.FechaCreacion = obj.CreacionPoliza.ToString();
                            poliza.FechaModificacion = obj.ModificacionPoliza.ToString();

                            poliza.Usuario = new ML.Usuario();
                            poliza.Usuario.IdUsuario = obj.IdUsuario.Value;

                            poliza.SubPolzia  = new ML.SubPoliza();
                            poliza.SubPolzia.IdSubPoliza = obj.IdSubPoliza.Value;
                            poliza.SubPolzia.Nombre = obj.SubPolizaNombre;

                            poliza.Vigencia = new Vigencia();
                            poliza.Vigencia.IdVigencia = obj.IdVigencia;
                            poliza.Vigencia.FechaCreacion = obj.CreacionVigencia.ToString();
                            poliza.Vigencia.FechaModificacion = obj.ModificacionVigencia.ToString();

                            poliza.Usuario.Nombre = obj.UsuarioNombre;
                            poliza.Usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            poliza.Usuario.ApellidoMaterno = obj.ApellidoMaterno;

                            result.Objects.Add(poliza);
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
        }
    }
}
