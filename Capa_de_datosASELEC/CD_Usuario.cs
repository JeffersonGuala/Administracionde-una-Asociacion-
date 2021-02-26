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
    public class CD_Usuario: Conexion_DB.ConnectionToSql
    {
        //private Conexion_DB con = new Conexion_DB();
        private Conexion_DB con = new Conexion_DB();
        private SqlCommand comando = new SqlCommand();
        private SqlDataReader leerfilas;

        public DataTable ListarUsuarios()
        {
            comando = new SqlCommand();
            DataTable TablaPro = new DataTable();
            comando.Connection = con.AbrirConexion();
            comando.CommandText = "SELECT * FROM VUsuarios";
            leerfilas = comando.ExecuteReader();
            TablaPro.Load(leerfilas);
            leerfilas.Close();
            con.CerrarConexion();
            return TablaPro;
        }

        public DataTable Buscar()
        {
            comando = new SqlCommand();
            DataTable TablaPro = new DataTable();
            comando.Connection = con.AbrirConexion();
            comando.CommandText = "SELECT * FROM VUsuarios";
            leerfilas = comando.ExecuteReader();
            TablaPro.Load(leerfilas);
            leerfilas.Close();
            con.CerrarConexion();
            return TablaPro;
        }

        public DataTable ListarTipodeUsuarios()
        {
            comando = new SqlCommand();
            DataTable TablaP = new DataTable();
            comando.Connection = con.AbrirConexion();
            comando.CommandText = "MostrarTipodeUsuarios";
            comando.CommandType = CommandType.StoredProcedure;
            leerfilas = comando.ExecuteReader();
            TablaP.Load(leerfilas);
            leerfilas.Close();
            con.CerrarConexion();
            return TablaP;
        }


        public void Actualizar_Us(int idTipoUsuario, string nombreUsuario, string contrasenia, string correo, int id)
        {

            comando = new SqlCommand();
            comando.Connection = con.AbrirConexion();
            comando.CommandText = "sp_actualizarUsuario";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@tipoUsuario", idTipoUsuario);
            comando.Parameters.AddWithValue("@Nombre", nombreUsuario);
            comando.Parameters.AddWithValue("@Pass", contrasenia);
            comando.Parameters.AddWithValue("@email", correo);
            comando.Parameters.AddWithValue("@idUsuario", id);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            con.CerrarConexion();
        }



        public bool existsUser(int id, string loginName, string pass)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select * from tblUsuario where idUsuario=@id  and nombreUsuario=@loginName COLLATE SQL_Latin1_General_CP1_CS_AS and passwordUsuario=@pass COLLATE SQL_Latin1_General_CP1_CS_AS";
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@loginName", loginName);
                    command.Parameters.AddWithValue("@pass", pass);
                    command.CommandType = CommandType.Text;
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                        return true;
                    else
                        return false;
                }
            }

        }

        public bool Login(string user, string pass)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select * from tblUsuario where nombreUsuario=@user COLLATE SQL_Latin1_General_CP1_CS_AS and passwordUsuario=@pass COLLATE SQL_Latin1_General_CP1_CS_AS";
                    command.Parameters.AddWithValue("@user", user);
                    command.Parameters.AddWithValue("@pass", pass);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Usuario_cache.IdUsuario = reader.GetInt32(0);
                            Usuario_cache.NombreLog = reader.GetString(1);
                            Usuario_cache.Password = reader.GetString(2);
                            Usuario_cache.Nombre = reader.GetString(3);
                            Usuario_cache.Email = reader.GetString(4);
                            Usuario_cache.TipoUsuario = reader.GetInt32(5);
                        }
                        return true;
                    }
                    else
                        return false;
                }
            }
        }


    }
}
