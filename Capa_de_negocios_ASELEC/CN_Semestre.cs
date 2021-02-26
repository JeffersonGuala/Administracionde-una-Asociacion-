using Capa_de_datosASELEC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_negocios_ASELEC
{
    public class CN_Semestre
    {
        private CD_Semestre objetoCD = new CD_Semestre();

        public IEnumerable<Semestre> listarSemestres()
        {
            foreach(DataRow row in objetoCD.listar().Rows)
            {
                yield return new Semestre
                {
                    nombre = row[0].ToString(),
                    fechaInicio = Convert.ToDateTime(row[1]),
                    fechaFin = Convert.ToDateTime(row[2])
                };
            }
        }

        public void ingresarNuevoSemestre(string idSemestre, DateTime fechaInicio, DateTime fechaFin)
        {
            objetoCD.ingresar(idSemestre, fechaInicio, fechaFin);
        }
    }
}
