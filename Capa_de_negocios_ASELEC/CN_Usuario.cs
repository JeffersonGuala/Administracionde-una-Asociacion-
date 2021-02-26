using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_de_datosASELEC;
using Capa_Comun_Aselec;
using System.Data;
using System.Collections.ObjectModel;

namespace Capa_de_negocios_ASELEC
{

    public class TipoUser
    {
        public int Id_TU { get; set; }
        public string Tipodeusuario { get; set; }
    }
    public class CN_Usuario
    {
        

        private CD_Usuario userDao = new CD_Usuario();


        public DataTable mostrarTusuarios()
        {
            DataTable tablatipoCarrera = new DataTable();
            tablatipoCarrera = userDao.ListarTipodeUsuarios();
            return tablatipoCarrera;
        }

        public DataTable Buscar()
        {
            DataTable dtb = new DataTable();
            dtb = userDao.Buscar();
            return dtb;
        }
        public DataTable mostrarUsuarios()
        {

            DataTable tablaProveedores = userDao.ListarUsuarios();
            return tablaProveedores;
        }

        public ObservableCollection<TipoUser> GetTipodeUsuario()
        {
            ObservableCollection<TipoUser> usuarios = new ObservableCollection<TipoUser>();

            foreach (DataRow item in mostrarTusuarios().Rows)
            {
                var usuario = new TipoUser
                {
                    Id_TU = int.Parse(item["id"].ToString()),
                    Tipodeusuario = item["tipo"].ToString(),

                };

                usuarios.Add(usuario);
            }

            return usuarios;
        }

        public void Actualizar(int idTipoUsuario, string nombreUsuario, string contrasenia, string correo, int id)
        {
            //char separator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            //valordeaportacion = String.Format("{0:C}", valordeaportacion);
            userDao.Actualizar_Us(Convert.ToInt32(idTipoUsuario), nombreUsuario, contrasenia, correo, Convert.ToInt32(id));
        }
        public bool securityLogin()
        {
            if (Usuario_cache.IdUsuario >= 1)
            {
                if (userDao.existsUser(Usuario_cache.IdUsuario, Usuario_cache.NombreLog, Usuario_cache.Password) == true)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public bool LoginUser(string user, string pass)
        {
            return userDao.Login(user, pass);
        }

    }
}
