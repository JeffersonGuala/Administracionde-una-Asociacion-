using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_negocios_ASELEC
{
    public class Prestamo
    {
        public int IdArticulo { get; set; }
        public string Articulo { get; set; }
        public int IdEstudiante { get; set; }
        public int IdUsuario { get; set; }
        public int Tiempo { get; set; }
        public float Total { get; set; }
        public float CostoPorHora { get; set; }
    }
}
