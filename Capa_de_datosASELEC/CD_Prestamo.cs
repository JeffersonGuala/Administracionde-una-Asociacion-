using Capa_Comun_Aselec;
using Capa_Comun_Aselec.Cache;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_datosASELEC
{
    public class CD_Prestamo
    {

        private Conexion_DB con = new Conexion_DB();
        private SqlCommand comando = new SqlCommand();
        private SqlDataReader leerfilas;

        public DataTable listar()
        {
            comando = new SqlCommand();
            DataTable TablaPro = new DataTable();
            comando.Connection = con.AbrirConexion();
            comando.CommandText = "SELECT * FROM VPrestamos WHERE id = '" + Configuracion.SemestreActual + "'";
            leerfilas = comando.ExecuteReader();
            TablaPro.Load(leerfilas);
            leerfilas.Close();
            con.CerrarConexion();
            return TablaPro;
        }

        public DataTable buscar(string cedulaEstudiante)
        {
            comando = new SqlCommand();
            DataTable TablaPro = new DataTable();
            comando.Connection = con.AbrirConexion();
            comando.CommandText = "SELECT * FROM VPrestamos WHERE cedulaCliente LIKE ('%" + cedulaEstudiante + "%') AND id = '" + Configuracion.SemestreActual + "'";
            leerfilas = comando.ExecuteReader();
            TablaPro.Load(leerfilas);
            leerfilas.Close();
            con.CerrarConexion();
            return TablaPro;
        }

        public void insertarPrestamo(int idArticulo, int idEstudiante, int tiempo)
        {
            comando = new SqlCommand();
            comando.Connection = con.AbrirConexion();
            comando.CommandText = "sp_AgregarPrestamo";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@idArticulo", idArticulo);
            comando.Parameters.AddWithValue("@idEstudiante", idEstudiante);
            comando.Parameters.AddWithValue("@idUsuario", Usuario_cache.IdUsuario);
            comando.Parameters.AddWithValue("@tiempo", tiempo);
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
        }

        public void insertarDevolucion(int idPrestamo, float penalizacion, string justificacion, float total, int idArticulo)
        {
            comando = new SqlCommand();
            comando.Connection = con.AbrirConexion();
            comando.CommandText = "sp_AgregarDevolucion";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@idPrestamo", idPrestamo);
            comando.Parameters.AddWithValue("@idUsuario", Usuario_cache.IdUsuario);
            comando.Parameters.AddWithValue("@penalizacion", penalizacion);
            comando.Parameters.AddWithValue("@justificacion", justificacion);
            comando.Parameters.AddWithValue("@total", total);
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
        }
    }
}
