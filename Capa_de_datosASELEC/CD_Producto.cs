using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Capa_Comun_Aselec.Cache;

namespace Capa_de_datosASELEC
{
    public class CD_Producto
    {
        private Conexion_DB conexion = new Conexion_DB();
        public void insertar(string nombreProducto, string marca, float precio, float pvp, int stock, int tipoProducto)
        {
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "sp_ingresarProducto";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@nombreProducto", nombreProducto);
            comando.Parameters.AddWithValue("@marca", marca);
            comando.Parameters.AddWithValue("@precio", precio);
            comando.Parameters.AddWithValue("@pvp", pvp);
            comando.Parameters.AddWithValue("@stock", stock);
            comando.Parameters.AddWithValue("@tipoProducto", tipoProducto);
            comando.Parameters.AddWithValue("@idSemestre", Configuracion.SemestreActual);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }

        public void actualizar(int idProducto, string nombreProducto, string marca, float precio, float pvp, int stock, int tipoProducto)
        {
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "sp_actualizarProducto";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@idProducto", idProducto);
            comando.Parameters.AddWithValue("@nombreProducto", nombreProducto);
            comando.Parameters.AddWithValue("@marca", marca);
            comando.Parameters.AddWithValue("@precio", precio);
            comando.Parameters.AddWithValue("@pvp", pvp);
            comando.Parameters.AddWithValue("@stock", stock);
            comando.Parameters.AddWithValue("@tipoProducto", tipoProducto);
            comando.Parameters.AddWithValue("@idSemestre", Configuracion.SemestreActual);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }

        public DataTable listarTipoProductos()
        {
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion.AbrirConexion();
            DataTable TablaP = new DataTable();
            comando.CommandText = "sp_listarTipoProducto";
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataReader leerfilas = comando.ExecuteReader();
            TablaP.Load(leerfilas);
            leerfilas.Close();
            conexion.CerrarConexion();
            return TablaP;
        }

        public DataTable listarProductos()
        {
            SqlCommand comando = new SqlCommand();
            DataTable tabla = new DataTable();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SELECT * FROM VProductos WHERE Semestre = '" + Configuracion.SemestreActual + "'";
            SqlDataReader leerfilas = comando.ExecuteReader();
            tabla.Load(leerfilas);
            leerfilas.Close();
            conexion.CerrarConexion();
            return tabla;
        }

        public DataTable buscarProductos(string nombreProducto)
        {
            SqlCommand comando = new SqlCommand();
            DataTable tabla = new DataTable();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SELECT * FROM VProductos WHERE NombreProducto LIKE('%" + nombreProducto + "%') AND Semestre = '" + Configuracion.SemestreActual + "'";
            comando.ExecuteNonQuery();
            DataTable dta = new DataTable();
            SqlDataAdapter sqd = new SqlDataAdapter(comando);
            sqd.Fill(dta);
            conexion.CerrarConexion();
            return dta;
        }

        public void eliminar(int idProducto)
        {
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "sp_eliminarProducto";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@idProducto", idProducto);
            comando.Parameters.AddWithValue("@idSemestre", Configuracion.SemestreActual);
            try
            {
                comando.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                comando.Parameters.Clear();
                throw new Exception(ex.Message);
            }
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }
    }
}
