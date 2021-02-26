using Capa_de_datosASELEC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_negocios_ASELEC
{
    public class CN_Gasto
    {
        private CD_Gasto objetoCD = new CD_Gasto();
        public void insertarGasto(string nombreGasto, int cantidad, float precio, string justificacion)
        {
            objetoCD.insertar(nombreGasto, cantidad, precio, justificacion);
        }

        public DataTable listarGastos()
        {
            DataTable tabla = objetoCD.listar();
            return tabla;
        }

        public DataTable buscarGastos(string nombreGasto)
        {
            DataTable tabla = objetoCD.buscar(nombreGasto);
            return tabla;
        }
    }
}
