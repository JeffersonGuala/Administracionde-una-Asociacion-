using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Comun_Aselec;
using System.Data.SqlClient;
using System.Data;

namespace Capa_de_datosASELEC
{
    public class CD_Semestre: Conexion_DB.ConnectionToSql
    {
        public DataTable listar()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM Semestre ORDER BY fechaInicio DESC";
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    DataTable dta = new DataTable();
                    dta.Load(reader);
                    return dta;
                }
            }

        }

        public void ingresar(string idSemestre, DateTime fechaInicio, DateTime fechaFin)
        {
            using (var conexion = GetConnection())
            {
                conexion.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandText = "sp_ingresarSemestre";
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@id", idSemestre);
                    comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    comando.Parameters.AddWithValue("@fechaFin", fechaFin);
                    comando.ExecuteNonQuery();
                    comando.Parameters.Clear();
                }
            }
        }
    }
}
