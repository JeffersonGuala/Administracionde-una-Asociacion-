using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
    /// Lógica de interacción para Control_de_usuario_Gastos.xaml
    /// </summary>
    public partial class Control_de_usuario_Gastos : UserControl
    {

        CN_Gasto objetoCN = new CN_Gasto();
        public Control_de_usuario_Gastos()
        {
            InitializeComponent();
            txtFecha_gastos.Text = DateTime.Now.ToString("dd/MM/yyyy");

        }

        private void txtBuscar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBuscar_gasto.Text))

            {
                txtBuscar_gasto.Visibility = System.Windows.Visibility.Collapsed;

                txtBuscar_marca.Visibility = System.Windows.Visibility.Visible;

            }
        }

        private void txtBuscar_marca_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBuscar_marca.Visibility = System.Windows.Visibility.Collapsed;

            txtBuscar_gasto.Visibility = System.Windows.Visibility.Visible;

            txtBuscar_gasto.Focus();
        }

        private void btn_buscar_gasto_Click(object sender, RoutedEventArgs e)
        {
            dtg_modulo_gastos.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = objetoCN.buscarGastos(txtBuscar_gasto.Text) });
        }

        private void btn_agregar_gasto_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_nombre_gasto.Text))
            {

                MessageBox.Show("Verifique que se ha llenado correctamente el Nombre del gasto u objeto ", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            else if (string.IsNullOrEmpty(txt_cantidad_gasto.Text) || Convert.ToInt32(txt_cantidad_gasto.Text) < 1)
            {
                MessageBox.Show("Verifique que se ha llenado correctamente la Cantidad del gasto u objeto", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
            else if (string.IsNullOrEmpty(txt_costo_total.Text))
            {
                MessageBox.Show("Verifique que se ha llenado correctamente el Precio Total de Compra del gasto u objeto", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }

            else if (string.IsNullOrEmpty(txt_justificacion_gasto.Text))
            {

                MessageBox.Show("Verifique que se ha llenado correctamente la Justificación del gasto u objeto", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }

            else
            {


                if (MessageBox.Show("\tGasto:  " + txt_nombre_gasto.Text + " \tEsta a punto de ser ingresado \n\n\t¿Está seguro que desea seguir con la transacción?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    char separator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    objetoCN.insertarGasto(
                        txt_nombre_gasto.Text,
                        int.Parse(txt_cantidad_gasto.Text),
                        float.Parse(txt_costo_total.Text.Replace(',', separator)),
                        txt_justificacion_gasto.Text);


                    MessageBox.Show("Gasto " + txt_nombre_gasto.Text + "\n Ingresado correctamente", "Ingreso de datos exitoso", MessageBoxButton.OK, MessageBoxImage.Information);

                    limpiarForm();
                }
                else
                {

                    MessageBox.Show("Gasto " + txt_nombre_gasto.Text + "\n No ingresado correctamente", "Fallo de ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Error);

                };


                listarGastos();
            }
        }

        private void limpiarForm()
        {
            txt_nombre_gasto.Clear();
            txt_cantidad_gasto.Clear();
            txt_costo_total.Clear();
            txt_justificacion_gasto.Clear();
            lbl_error.Content = "";
        }

        private void listarGastos()
        {
            dtg_modulo_gastos.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = objetoCN.listarGastos() });
        }


        private void txt_cantidad_gasto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (!char.IsDigit(e.Text, e.Text.Length - 2))
                    e.Handled = true;
            }
            catch (Exception ex)
            {

                lbl_error.Content = "No se puede insertar el valor de 0";

            }
        }

        private void txt_costo_total_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex _regex = new Regex(@"^[0-9]+(\,)?([0-9]{1,2})?$");
            TextBox textBox = sender as TextBox;
            bool handler = _regex.IsMatch(textBox.Text + e.Text);
            e.Handled = !handler;
        }
    }

}

