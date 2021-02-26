using Capa_de_datosASELEC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_negocios_ASELEC
{
    public class CN_Prestamo
    {
        CD_Prestamo objetoCD = new CD_Prestamo();

        public void insertarPrestamo(int idEstudiante, List<Prestamo> detalles)
        {
            try
            {
                foreach (var detalle in detalles)
                {
                    objetoCD.insertarPrestamo(detalle.IdArticulo, idEstudiante, detalle.Tiempo);
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
            
        }

        public DataTable buscarPrestamosPorCedulaEstudiante(string cedulaEstudiante)
        {
            return objetoCD.buscar(cedulaEstudiante);
        }

        public void insertarDevolucion(int idPrestamo, float penalizacion, string justificacion, float total, int idArticulo)
        {
            objetoCD.insertarDevolucion(idPrestamo, penalizacion, justificacion, total, idArticulo);
        }
            
    }
}
