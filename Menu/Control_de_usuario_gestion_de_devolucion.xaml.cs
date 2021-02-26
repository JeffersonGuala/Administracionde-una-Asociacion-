using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
using Capa_Comun_Aselec;
using Capa_de_negocios_ASELEC;

namespace Menu
{
    /// <summary>
    /// Lógica de interacción para Control_de_usuario_gestion_de_devolucion.xaml
    /// </summary>
    public partial class Control_de_usuario_gestion_de_devolucion : UserControl
    {
        CN_Prestamo prestamoCN = new CN_Prestamo();
        DataRowView prestamoSeleccionadoRow;
        double total = 0;
        double penalizacion = 0;
        public Control_de_usuario_gestion_de_devolucion()
        {
            InitializeComponent();
        }

        private void txtBuscar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBuscar_prestamos.Text))

            {
                txtBuscar_prestamos.Visibility = System.Windows.Visibility.Collapsed;

                txtBuscar_marca.Visibility = System.Windows.Visibility.Visible;

            }
        }

        private void txtBuscar_marca_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBuscar_marca.Visibility = System.Windows.Visibility.Collapsed;

            txtBuscar_prestamos.Visibility = System.Windows.Visibility.Visible;

            txtBuscar_prestamos.Focus();
        }

        private void Grid_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void btn_buscar_cedula_gdev_Click(object sender, RoutedEventArgs e)
        {
            dtg_lista_de_prestamos.SetBinding(
                ItemsControl.ItemsSourceProperty, 
                new Binding { Source = prestamoCN.buscarPrestamosPorCedulaEstudiante(txtBuscar_prestamos.Text) }
           );
        }

        private void dtg_lista_de_prestamos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView rowView = dtg_lista_de_prestamos.SelectedItem as DataRowView;
            if (rowView != null)
            {
                char separator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                double costoPorHora = Convert.ToDouble(rowView[3]);
                double horasAlquilado = Convert.ToDouble(rowView[10]);
                total = costoPorHora * horasAlquilado;
                txtFecha_de_devolucion.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txt_resp_de_devolucion.Text = Usuario_cache.Nombre;
                txtNombre_articulo.Text = rowView[2].ToString();
                txt_cedula.Text = rowView[4].ToString();
                txt_nombre_est.Text = rowView[5].ToString();
                double descuento = 0;
                if (rowView[6].ToString() == "1")
                {
                    txt_est_aportacion.Text = "Aportante";
                    descuento = total * 0.3;
                }
                else
                {
                    txt_est_aportacion.Text = "No Aportante";
                }
                total = total - descuento;
                txt_desc_aportante.Text = Math.Round(descuento, 3).ToString("0.00").Replace(',', separator);
                txt_resp_de_alquiler.Text = rowView[8].ToString();
                txt_tiempo_de_alquiler.Text = horasAlquilado.ToString();
                txt_total_alquiler.Text = Math.Round(total, 3).ToString("0.00").Replace(',', separator);
                prestamoSeleccionadoRow = rowView;
                btn_registrar_devolucion.IsEnabled = true;
            }
        }

        private void btn_registrar_devolucion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                char separator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                float penalizacion = 0;
                if (txt_valor_de_penalizacion.Text != "")
                {
                    penalizacion = float.Parse(txt_valor_de_penalizacion.Text.ToString().Replace(separator, '.'));
                }
                prestamoCN.insertarDevolucion(
                    Convert.ToInt32(prestamoSeleccionadoRow[0]),
                    penalizacion,
                    txt_justificacion_penalizacion.Text,
                    float.Parse(total.ToString().Replace(separator, '.')),
                    Convert.ToInt32(prestamoSeleccionadoRow[1])
                );
                MessageBox.Show("Devolución registrada");
                limpiar();
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void limpiar()
        {
            txtBuscar_prestamos.Clear();
            txtFecha_de_devolucion.Clear();
            txt_resp_de_devolucion.Clear();
            txtNombre_articulo.Clear();
            txt_cedula.Clear();
            txt_nombre_est.Clear();
            btn_registrar_devolucion.IsEnabled = false;
            txt_est_aportacion.Clear();
            txt_desc_aportante.Clear();
            txt_resp_de_alquiler.Clear();
            txt_total_alquiler.Clear();
            prestamoSeleccionadoRow = null;
            txt_valor_de_penalizacion.Clear();
            txt_justificacion_penalizacion.Clear();
            txt_tiempo_de_alquiler.Clear();
            dtg_lista_de_prestamos.Items.Clear();
            dtg_lista_de_prestamos.ItemsSource = null;
            txt_total_alquiler.Clear();
            total = 0;
        }

        private void txt_valor_de_penalizacion_LostFocus(object sender, RoutedEventArgs e)
        {
            

        }

        private void txt_valor_de_penalizacion_TextChanged(object sender, TextChangedEventArgs e)
        {
            double totalAux = total;
            if (txt_valor_de_penalizacion.Text != "")
            {
                penalizacion = Convert.ToDouble(txt_valor_de_penalizacion.Text);
                totalAux = total + penalizacion;
            }
            txt_total_alquiler.Text = Math.Round(totalAux, 2).ToString("0.00");
        }
    }
}
