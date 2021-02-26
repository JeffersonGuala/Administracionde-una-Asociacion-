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
    public class CD_Estudiante
    {
        private Conexion_DB con = new Conexion_DB();
        private SqlCommand comando = new SqlCommand();
        private SqlDataReader leerfilas;

        public DataTable ListarEstudiantes()
        {
            comando = new SqlCommand();
            DataTable TablaPro = new DataTable();
            comando.Connection = con.AbrirConexion();
            comando.CommandText = "SELECT * FROM VEstudiantes WHERE Semestre = '" + Configuracion.SemestreActual + "'";
            leerfilas = comando.ExecuteReader();
            TablaPro.Load(leerfilas);
            leerfilas.Close();
            con.CerrarConexion();
            return TablaPro;
        }

        public DataTable Buscar(string nombreEstudiante)
        {
            comando = new SqlCommand();
            DataTable TablaPro = new DataTable();
            comando.Connection = con.AbrirConexion();
            comando.CommandText = "SELECT * FROM VEstudiantes WHERE nombreCliente LIKE ('%" + nombreEstudiante + "%') AND Semestre = '" + Configuracion.SemestreActual + "'";
            leerfilas = comando.ExecuteReader();
            TablaPro.Load(leerfilas);
            leerfilas.Close();
            con.CerrarConexion();
            return TablaPro;
        }

        public DataTable BuscarPorCedula(string cedula)
        {
            comando = new SqlCommand();
            comando.Connection = con.AbrirConexion();
            comando.CommandType = CommandType.Text;
            comando.CommandText = "SELECT * FROM vEstudiantes WHERE cedulaCliente LIKE " + cedula + " AND Semestre = '" + Configuracion.SemestreActual + "'";
            comando.ExecuteNonQuery();
            DataTable dta = new DataTable();
            SqlDataAdapter sqd = new SqlDataAdapter(comando);
            sqd.Fill(dta);
            con.CerrarConexion();
            return dta;

        }

        public DataTable ListarTipodeAportacion()
        {
            comando = new SqlCommand();
            DataTable TablaP = new DataTable();
            comando.Connection = con.AbrirConexion();
            comando.CommandText = "MostrarTipodeAportacion";
            comando.CommandType = CommandType.StoredProcedure;
            leerfilas = comando.ExecuteReader();
            TablaP.Load(leerfilas);
            leerfilas.Close();
            con.CerrarConexion();
            return TablaP;
        }

        public DataTable ListarCarreras()
        {
            comando = new SqlCommand();
            DataTable TablaP = new DataTable();
            comando.Connection = con.AbrirConexion();
            comando.CommandText = "MostrarCarrera";
            comando.CommandType = CommandType.StoredProcedure;
            leerfilas = comando.ExecuteReader();
            TablaP.Load(leerfilas);
            leerfilas.Close();
            con.CerrarConexion();
            return TablaP;
        }

        public int Insertar(int idTipoAportacion, string nombreCliente, string cedula, int idCarrera, string correo, string telefono, double valordeaportacion)
        {
            comando = new SqlCommand();
            comando.Connection = con.AbrirConexion();
            comando.CommandText = "sp_Estudiantes";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@Tipodeaportacion", idTipoAportacion);
            comando.Parameters.AddWithValue("@nombre", nombreCliente);
            comando.Parameters.AddWithValue("@cedula", cedula);
            comando.Parameters.AddWithValue("@carrera", idCarrera);
            comando.Parameters.AddWithValue("@correo", correo);
            comando.Parameters.AddWithValue("@telefono", telefono);
            comando.Parameters.AddWithValue("@valordeaportacion", valordeaportacion);
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

        public void Actualizar_Est(int idTipoAportacion, string nombreCliente, string cedula, int idCarrera, string correo, string telefono, double valordeaportacion,int id)
        {

            comando = new SqlCommand();
            comando.Connection = con.AbrirConexion();
            comando.CommandText = "sp_ActualizarEstudiantes";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Tipodeaportacion", idTipoAportacion);
            comando.Parameters.AddWithValue("@nombre", nombreCliente);
            comando.Parameters.AddWithValue("@cedula", cedula);
            comando.Parameters.AddWithValue("@carrera", idCarrera);
            comando.Parameters.AddWithValue("@correo", correo);
            comando.Parameters.AddWithValue("@telefono", telefono);
            comando.Parameters.AddWithValue("@valordeaportacion", valordeaportacion);
            comando.Parameters.AddWithValue("idEstudiante", id);
            comando.Parameters.AddWithValue("@idSemestre", Configuracion.SemestreActual);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            con.CerrarConexion();
        }

        public void Eliminar(int idEstudiante)
        {
            comando = new SqlCommand();
            comando.Connection = con.AbrirConexion();
            comando.CommandText = "sp_EliminarEstudiante";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@estudianteID", idEstudiante);
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
            con.CerrarConexion();
        }
    }
}
