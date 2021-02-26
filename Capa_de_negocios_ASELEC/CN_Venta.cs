using Capa_de_datosASELEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_negocios_ASELEC
{
    public class CN_Venta
    {
        CD_Venta objetoCD = new CD_Venta();

        public void insertarVenta(float totalVenta, List<DetalleVenta> detalles)
        {
            try
            {
                int ventaId = objetoCD.insertarVenta(totalVenta);
                foreach (var detalle in detalles)
                {
                    objetoCD.insertarDetalle(detalle.IdProducto, ventaId, detalle.Cantidad, detalle.Total);
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
