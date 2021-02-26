using Capa_de_datosASELEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data;

namespace Capa_de_negocios_ASELEC
{
    public class CN_Articulo
    {
        private CD_Articulo objetoCD = new CD_Articulo();
        public void insertarArticulo(string nombreArticulo, string marca, float precio, float costoEnHoras, int cantidad, int idTipoArticulo)
        {
            objetoCD.insertar(nombreArticulo, marca, precio, costoEnHoras, cantidad, idTipoArticulo);
        }

        public void actualizarArticulo(int idArticulo, string nombreArticulo, string marca, float precio, float costoEnHoras, int cantidad, int idTipoArticulo)
        {
            objetoCD.actualizar(idArticulo, nombreArticulo, marca, precio, costoEnHoras, cantidad, idTipoArticulo);
        }

        public ObservableCollection<TipoProducto> getTipoArticulo()
        {
            DataTable tablaTipoProducto = objetoCD.listarTipoArticulos();
            ObservableCollection<TipoProducto> tproducto = new ObservableCollection<TipoProducto>();
            foreach (DataRow item in tablaTipoProducto.Rows)
            {
                var tproductos = new TipoProducto
                {
                    Id = int.Parse(item[0].ToString()),
                    Tipo_de_Producto = item[1].ToString(),
                };
                tproducto.Add(tproductos);
            }
            return tproducto;
        }

        public DataTable listarArticulos()
        {
            DataTable tablaProductos = objetoCD.listar();
            return tablaProductos;
        }

        public DataTable buscarArticulo(string nombreArticulo)
        {
            DataTable tablaProductos = objetoCD.buscar(nombreArticulo);
            return tablaProductos;
        }

        public void eliminarArticulo(int idArticulo)
        {
            try
            {
                objetoCD.eliminar(idArticulo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
