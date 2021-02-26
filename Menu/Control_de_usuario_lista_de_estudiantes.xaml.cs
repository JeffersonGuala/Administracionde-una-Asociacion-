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
using Capa_de_negocios_ASELEC;

namespace Menu
{
    /// <summary>
    /// Lógica de interacción para Control_de_usuario_lista_de_estudiantes.xaml
    /// </summary>
    public partial class Control_de_usuario_lista_de_estudiantes : UserControl
    {
        CN_Estudiante objetoCN = new CN_Estudiante();
        public Control_de_usuario_lista_de_estudiantes()
        {
            InitializeComponent();
        }
        private void txtBuscar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBuscar_nomb_est.Text))

            {
                txtBuscar_nomb_est.Visibility = System.Windows.Visibility.Collapsed;

                txtBuscar_marca.Visibility = System.Windows.Visibility.Visible;

            }
        }

        private void txtBuscar_marca_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBuscar_marca.Visibility = System.Windows.Visibility.Collapsed;

            txtBuscar_nomb_est.Visibility = System.Windows.Visibility.Visible;

            txtBuscar_nomb_est.Focus();
        }

        private void btn_buscar_est_Click(object sender, RoutedEventArgs e)
        {
            dtg_lista_estudiantes.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = objetoCN.Buscar(txtBuscar_nomb_est.Text) });
        }
    }
}
