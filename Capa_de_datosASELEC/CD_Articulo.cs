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
    public class CD_Articulo
    {
        private Conexion_DB conexion = new Conexion_DB();
        public void insertar(string nombreArticulo, string marca, float precio, float costoEnHoras, int cantidad, int idTipoArticulo)
        {
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "sp_ingresarArticulo";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@nombreArticulo", nombreArticulo);
            comando.Parameters.AddWithValue("@marca", marca);
            comando.Parameters.AddWithValue("@precio", precio);
            comando.Parameters.AddWithValue("@costoEnHoras", costoEnHoras);
            comando.Parameters.AddWithValue("@cantidad", cantidad);
            comando.Parameters.AddWithValue("@idTipoArticulo", idTipoArticulo);
            comando.Parameters.AddWithValue("@idSemestre", Configuracion.SemestreActual);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }

        public void actualizar(int idArticulo, string nombreArticulo, string marca, float precio, float costoEnHoras, int cantidad, int idTipoArticulo)
        {
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "sp_actualizarArticulo";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@idArticulo", idArticulo);
            comando.Parameters.AddWithValue("@nombreArticulo", nombreArticulo);
            comando.Parameters.AddWithValue("@marca", marca);
            comando.Parameters.AddWithValue("@precio", precio);
            comando.Parameters.AddWithValue("@costoEnHoras", costoEnHoras);
            comando.Parameters.AddWithValue("@cantidad", cantidad);
            comando.Parameters.AddWithValue("@idTipoArticulo", idTipoArticulo);
            comando.Parameters.AddWithValue("@idSemestre", Configuracion.SemestreActual);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }

        public DataTable listarTipoArticulos()
        {
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion.AbrirConexion();
            DataTable TablaP = new DataTable();
            comando.CommandText = "sp_listarTipoArticulo";
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataReader leerfilas = comando.ExecuteReader();
            TablaP.Load(leerfilas);
            leerfilas.Close();
            conexion.CerrarConexion();
            return TablaP;
        }

        public DataTable listar()
        {
            SqlCommand comando = new SqlCommand();
            DataTable tabla = new DataTable();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SELECT * FROM VArticulos WHERE Semestre = '" + Configuracion.SemestreActual + "'";
            SqlDataReader leerfilas = comando.ExecuteReader();
            tabla.Load(leerfilas);
            leerfilas.Close();
            conexion.CerrarConexion();
            return tabla;
        }

        public DataTable buscar(string nombreArticulo)
        {
            SqlCommand comando = new SqlCommand();
            DataTable tabla = new DataTable();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SELECT * FROM VArticulos WHERE NombreArticulo LIKE('%" + nombreArticulo + "%') AND Semestre = '" + Configuracion.SemestreActual + "'";
            comando.ExecuteNonQuery();
            DataTable dta = new DataTable();
            SqlDataAdapter sqd = new SqlDataAdapter(comando);
            sqd.Fill(dta);
            conexion.CerrarConexion();
            return dta;
        }

        public void eliminar(int idArticulo)
        {
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "sp_eliminarArticulo";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@idArticulo", idArticulo);
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
