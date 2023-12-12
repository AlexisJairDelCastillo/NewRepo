using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Acceso
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.AdelCastilloExamenEksContext context = new DL.AdelCastilloExamenEksContext())
                {
                    var query = context.Accesos.FromSqlRaw($"AccesoGetAll").ToList();

                    if(query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach( DL.Acceso resultAcceso in query )
                        {
                            ML.Acceso acceso = new ML.Acceso();
                            acceso.IdAcceso = resultAcceso.IdAcceso;
                            acceso.Tipo = resultAcceso.Tipo;

                            result.Objects.Add(acceso);
                        }

                        result.Correct = true;
                    }
                }
            }catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar los registros." + ex.Message;
            }

            return result;
        }
    }
}
