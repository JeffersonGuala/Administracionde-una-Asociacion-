using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_de_datosASELEC;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading;

namespace Capa_de_negocios_ASELEC
{
    public class TipodeAportacion
    {
        public int Id_TA { get; set; }
        public string Tipodeaportacion { get; set; }
    }

    public class Carrera
    {
        public int Id { get; set; }
        public string nombreCarrera { get; set; }
    }

    public class CN_Estudiante
    {
        private CD_Estudiante estudiante_cd = new CD_Estudiante();

        public DataTable mostrarTipodeAportacion()
        {
            DataTable tablaTipoaportacion = new DataTable();
            tablaTipoaportacion = estudiante_cd.ListarTipodeAportacion();
            return tablaTipoaportacion;
        }

        public DataTable mostrarCarrera()
        {
            DataTable tablatipoCarrera = new DataTable();
            tablatipoCarrera = estudiante_cd.ListarCarreras();
            return tablatipoCarrera;
        }
        public DataTable mostrarEstudiantes()
        {
            DataTable tablaProveedores = new DataTable();
            tablaProveedores = estudiante_cd.ListarEstudiantes();
            return tablaProveedores;
        }

        public DataTable BuscarPorCedula(string cedula)
        {
            DataTable dtb = new DataTable();
            dtb = estudiante_cd.BuscarPorCedula(cedula);
            return dtb;
        }

        public DataTable Buscar(string nombreProveedor)
        {
            return estudiante_cd.Buscar(nombreProveedor);
        }

        public ObservableCollection<TipodeAportacion> GetTipodeAportacion()
        {
            ObservableCollection<TipodeAportacion> aportaciones = new ObservableCollection<TipodeAportacion>();

            foreach (DataRow item in mostrarTipodeAportacion().Rows)
            {
                var aportacion = new TipodeAportacion
                {
                    Id_TA = int.Parse(item["idTipoAportacion"].ToString()),
                    Tipodeaportacion = item["NombreTipoAport"].ToString(),
                    
                };

                aportaciones.Add(aportacion);
            }

            return aportaciones;
        }

        public ObservableCollection<Carrera> GetCarrera()
        {
            ObservableCollection<Carrera> carreras = new ObservableCollection<Carrera>();

            foreach (DataRow item in mostrarCarrera().Rows)
            {
                var carrera = new Carrera
                {
                    Id = int.Parse(item["idCarrera"].ToString()),
                    nombreCarrera = item["NombreCarrera"].ToString(),
                };

                carreras.Add(carrera);
            }

            return carreras;
        }
      
        

        public int Insertar(int idTipoAportacion, string nombreCliente, string cedula, int idCarrera, string correo, string telefono, string valordeaportacion)
        {
            string val1;
            char separator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            val1 = String.Format("{0:C}", valordeaportacion);
            try
            {
                return estudiante_cd.Insertar(Convert.ToInt32(idTipoAportacion), nombreCliente, cedula, Convert.ToInt32(idCarrera), correo, telefono, Convert.ToDouble(val1.Replace(',', separator)));
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Actualizar(int idTipoAportacion, string nombreCliente, string cedula, int idCarrera, string correo, string telefono, string valordeaportacion,int id)
        {
            char separator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            valordeaportacion = String.Format("{0:C}", valordeaportacion);
            estudiante_cd.Actualizar_Est(Convert.ToInt32(idTipoAportacion), nombreCliente, cedula, Convert.ToInt32(idCarrera), correo, telefono, Convert.ToDouble(valordeaportacion.Replace(',', separator)), Convert.ToInt32 (id));
        }


        public void EliminarEst(int idEstudiante)
        {
            try
            {
                estudiante_cd.Eliminar(Convert.ToInt32(idEstudiante));
            }
            catch (Exception ex)
            {
                throw new Exception();
            }

        }


    }
}
