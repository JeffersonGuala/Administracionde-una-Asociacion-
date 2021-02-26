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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using Capa_de_negocios_ASELEC;
using System.Text.RegularExpressions;
using System.Threading;
using MaterialDesignThemes.Wpf;
using System.Globalization;


namespace Menu
{
    /// <summary>
    /// Lógica de interacción para Control_de_Usuarios.xaml
    /// </summary>
    public partial class Control_de_Usuarios : UserControl
    {
        private int idUs = 0;
        CN_Usuario objetoCN = new CN_Usuario();
        public Control_de_Usuarios()
        {
            InitializeComponent();

            ListarTipodeUsuarios();
        }

        private void limpiarForm()
        {
            txt_nombre.Clear();
            cmb_t_U.Text = "";
            txt_email.Clear();
            pass_box.Clear();
            pass_confirm_box.Clear();

        }

        public void ListarTipodeUsuarios()
        {
            CN_Usuario objCNPro = new CN_Usuario();
            cmb_t_U.ItemsSource = objCNPro.GetTipodeUsuario();
            this.cmb_t_U.DisplayMemberPath = " Tipodeusuario";
            this.cmb_t_U.SelectedValuePath = "Id_TU";
        }
        private void actualizar_usuario_Click(object sender, RoutedEventArgs e)
        {

            CN_Usuario NEST = new CN_Usuario();

            if (Regex.IsMatch(txt_email.Text,
                 @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"))
            {

            }
            else
            {

                MessageBox.Show("Verifique que el correo ingresado sea correcto", "Error en validación de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;


            }
            if (pass_confirm_box.Password != pass_box.Password)
            {
                MessageBox.Show("Verifique que las contraseñas ingresadas sean iguales", "Error en validación de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
            else if (string.IsNullOrEmpty(txt_nombre.Text) || txt_nombre.Text.Length < 10)
            {


                MessageBox.Show("Verifique que se lleno correctamente el nombre", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }

            else
            {
                char separator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                NEST.Actualizar(Convert.ToInt32(cmb_t_U.SelectedValue), txt_nombre.Text, pass_box.Password, txt_email.Text, Convert.ToInt32(idUs));
                MessageBox.Show("   Usuario " + cmb_t_U.Text + " Actualizado Correctamente", "Actualización de datos exitosa", MessageBoxButton.OK, MessageBoxImage.Information);

                dtg_modulo_usuarios.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = new DataTable() });

                limpiarForm();


                return;
            }



        }

        private void dtg_modulo_usuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CultureInfo ci = new CultureInfo("en-us");

            DataRowView rowView = dtg_modulo_usuarios.SelectedItem as DataRowView;
            if (dtg_modulo_usuarios.SelectedItem != null)
            {
            }
            if (rowView != null)
            {
                char separator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                cmb_t_U.Text = rowView[1].ToString();
                txt_nombre.Text = rowView[2].ToString();
                txt_email.Text = rowView[3].ToString();
                pass_box.Password = rowView[4].ToString();
                pass_confirm_box.Password = rowView[4].ToString();

                idUs = Convert.ToInt32(rowView[0].ToString());



            }
        }

        private void btn_cargar_Click(object sender, RoutedEventArgs e)
        {

            dtg_modulo_usuarios.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = objetoCN.Buscar() });
            //limpiar();




        }
    }
}

