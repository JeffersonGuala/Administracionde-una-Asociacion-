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
    public class CD_Venta
    {
        private Conexion_DB con = new Conexion_DB();
        private SqlCommand comando = new SqlCommand();

        public int insertarVenta(float totalVenta)
        {
            comando = new SqlCommand();
            comando.Connection = con.AbrirConexion();
            comando.CommandText = "sp_AgregarVenta";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@totalVenta", totalVenta);
            comando.Parameters.AddWithValue("@idUsuario", Usuario_cache.IdUsuario);
            comando.Parameters.AddWithValue("@idSemestre", Configuracion.SemestreActual);
            SqlParameter outputIdParam = new SqlParameter("@id", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            comando.Parameters.Add(outputIdParam);
            try
            {
                comando.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                comando.Parameters.Clear();
                throw new Exception(ex.Message);
            }
            int id = int.Parse(outputIdParam.Value.ToString());
            comando.Parameters.Clear();
            return id;
        }
        public void insertarDetalle(int idProducto, int idVenta, int cantidad, float total)
        {
            comando = new SqlCommand();
            comando.Connection = con.AbrirConexion();
            comando.CommandText = "sp_AgregarDetalle";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@idProducto", idProducto);
            comando.Parameters.AddWithValue("@idVenta", idVenta);
            comando.Parameters.AddWithValue("@cantidad", cantidad);
            comando.Parameters.AddWithValue("@total", total);
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
