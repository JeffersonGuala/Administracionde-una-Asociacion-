using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_negocios_ASELEC
{
    public class DetalleVenta
    {
        public int IdProducto { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public float PrecioUnitario { get; set; }
        public float Total { get; set; }
    }
}
