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
    public class CD_Gasto
    {
        private Conexion_DB conexion = new Conexion_DB();
        public void insertar(string nombreGasto, int cantidad, float precio, string justificacion)
        {
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "sp_ingresarGasto";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@nombreGasto", nombreGasto);
            comando.Parameters.AddWithValue("@cantidad", cantidad);
            comando.Parameters.AddWithValue("@precio", precio);
            comando.Parameters.AddWithValue("@justificacion", justificacion);
            comando.Parameters.AddWithValue("@idSemestre", Configuracion.SemestreActual);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }

        public DataTable listar()
        {
            SqlCommand comando = new SqlCommand();
            DataTable tabla = new DataTable();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SELECT * FROM VGastos WHERE idSemestre = '" + Configuracion.SemestreActual + "'";
            SqlDataReader leerfilas = comando.ExecuteReader();
            tabla.Load(leerfilas);
            leerfilas.Close();
            conexion.CerrarConexion();
            return tabla;
        }

        public DataTable buscar(string nombreGasto)
        {
            SqlCommand comando = new SqlCommand();
            DataTable tabla = new DataTable();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SELECT * FROM VGastos WHERE nombreGasto LIKE('%" + nombreGasto + "%') AND idSemestre = '" + Configuracion.SemestreActual + "'";
            comando.ExecuteNonQuery();
            DataTable dta = new DataTable();
            SqlDataAdapter sqd = new SqlDataAdapter(comando);
            sqd.Fill(dta);
            conexion.CerrarConexion();
            return dta;
        }
    }
}
