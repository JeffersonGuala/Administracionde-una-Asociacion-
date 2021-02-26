using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_de_datosASELEC;
using System.Collections.ObjectModel;
using System.Data;

namespace Capa_de_negocios_ASELEC
{
    public class CN_Producto
    {
        private CD_Producto objetoCD = new CD_Producto();
        public void insertarProducto(string nombreProducto, string marca, float precio, float pvp, int stock, int tipoProducto)
        {
            objetoCD.insertar(nombreProducto, marca, precio, pvp, stock, tipoProducto);
        }

        public void actualizarProducto(int idProducto, string nombreProducto, string marca, float precio, float pvp, int stock, int tipoProducto)
        {
            objetoCD.actualizar(idProducto, nombreProducto, marca, precio, pvp, stock, tipoProducto);
        }

        public ObservableCollection<TipoProducto> getTipoProductos()
        {
            DataTable tablaTipoProducto = objetoCD.listarTipoProductos();
            ObservableCollection<TipoProducto> tproducto = new ObservableCollection<TipoProducto>();
            foreach (DataRow item in tablaTipoProducto.Rows)
            {
                var tproductos = new TipoProducto
                {
                    Id = int.Parse(item["idTipoProducto"].ToString()),
                    Tipo_de_Producto = item["NombreTipoP"].ToString(),
                };
                tproducto.Add(tproductos);
            }
            return tproducto;
        }

        public DataTable listarProductos()
        {
            DataTable tablaProductos = objetoCD.listarProductos();
            return tablaProductos;
        }

        public DataTable buscarProductos(string nombreProducto)
        {
            DataTable tablaProductos = objetoCD.buscarProductos(nombreProducto);
            return tablaProductos;
        }

        public void eliminarProducto(int idProducto)
        {
            try
            {
                objetoCD.eliminar(idProducto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
