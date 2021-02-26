using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Capa_de_datosASELEC
{
    public class Conexion_DB
    {
        public abstract class ConnectionToSql
        {
            private readonly string connectionString;
            public ConnectionToSql()
            {
                connectionString = "Data Source=LAPTOP-S5VERLH1;Initial Catalog=Db_Asociacion2020B;Integrated Security=True";
            }

            protected SqlConnection GetConnection()
            {
                return new SqlConnection(connectionString);
            }

        }
            private SqlConnection Conexion = new SqlConnection("Data Source=DESKTOP-9FH3F4R;Initial Catalog=Db_Asociacion2020B;Integrated Security=True");
        
            public SqlConnection AbrirConexion()
        {
            if (Conexion.State == ConnectionState.Closed)
                Conexion.Open();
            return Conexion;
        }
        public SqlConnection CerrarConexion()
        {
            if (Conexion.State == ConnectionState.Open)
                Conexion.Close();
            return Conexion;
        }
        

    }
}
