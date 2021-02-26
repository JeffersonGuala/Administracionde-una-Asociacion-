using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Capa_Comun_Aselec;
using Capa_de_negocios_ASELEC;
namespace Menu
{
    /// <summary>
    /// Lógica de interacción para Control_de_bienvenida.xaml
    /// </summary>
    public partial class Control_de_bienvenida : UserControl
    {
        public Control_de_bienvenida()
        {
            InitializeComponent();
            security();
            LoadUserData();
        }

        private void security()
        {
            var userModel = new CN_Usuario();
            if (userModel.securityLogin() == false)
            {
                MessageBox.Show("Error Fatal, se detectó que está intentando acceder al sistema sin credenciales, por favor inicie sesión e indentifiquese");
                Application.Current.Shutdown();
            }
        }

        private void LoadUserData()
        {
            lbl_correo_usuario.Content = Usuario_cache.Email;
            //lbl_tipo_de_usuario.Content = Usuario_cache.NombreLog;
            lbl_tipo_de_usuario1.Content = Usuario_cache.NombreLog;
            lbl_nombre_usuario.Content = Usuario_cache.Nombre;
        }


    }
}
