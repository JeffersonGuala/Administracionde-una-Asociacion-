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
using Capa_de_negocios_ASELEC;

namespace Menu
{
    /// <summary>
    /// Lógica de interacción para Control_de_usuario_ventas.xaml
    /// </summary>
    public partial class Control_de_usuario_ventas : UserControl
    {
        CN_Producto productoCN = new CN_Producto();
        CN_Venta ventaCN = new CN_Venta();
        DataRowView productoSeleccionadoRow;
        public Control_de_usuario_ventas()
        {
            InitializeComponent();
            txtFecha_vnt.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void txtBuscar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBuscar_nomb_prod_vnt.Text))

            {
                txtBuscar_nomb_prod_vnt.Visibility = System.Windows.Visibility.Collapsed;

                txtBuscar_marca.Visibility = System.Windows.Visibility.Visible;

            }
        }

        private void txtBuscar_marca_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBuscar_marca.Visibility = System.Windows.Visibility.Collapsed;

            txtBuscar_nomb_prod_vnt.Visibility = System.Windows.Visibility.Visible;

            txtBuscar_nomb_prod_vnt.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int productosDisponibles = Convert.ToInt32(productoSeleccionadoRow[4].ToString());
            int productosDeseados = Convert.ToInt32(txt_cantidad.Text);
            if(productosDeseados > productosDisponibles)
            {
                MessageBox.Show("No hay suficientes productos disponibles", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                char separator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                float precioUnitario = float.Parse(productoSeleccionadoRow[5].ToString().Replace(separator, '.'));
                float totalPorProducto = productosDeseados * precioUnitario;
                dtg_detalle_vnt.Items.Add(new DetalleVenta
                {
                    IdProducto = Convert.ToInt32(productoSeleccionadoRow[0]),
                    Producto = productoSeleccionadoRow[1].ToString(),
                    Cantidad = productosDeseados,
                    PrecioUnitario = precioUnitario,
                    Total = totalPorProducto
                });
                float total = float.Parse(txt_total_vnt.Text) + totalPorProducto;
                txt_total_vnt.Text = total.ToString();
                productoSeleccionadoRow[4] = productosDisponibles - productosDeseados;
            }
        }

        private void btn_buscar_nomb_prod_vnt_Click(object sender, RoutedEventArgs e)
        {
            dtg_lista_de_prod_vnt.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = productoCN.buscarProductos(txtBuscar_nomb_prod_vnt.Text) });
        }

        private void dtg_lista_de_prod_vnt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView rowView = dtg_lista_de_prod_vnt.SelectedItem as DataRowView;
            if (rowView != null)
            {
                txt_nomb_prod_vnt.Text = rowView[1].ToString();
                productoSeleccionadoRow = rowView;
            }
        }

        private void btn_registrar_vnt_Click(object sender, RoutedEventArgs e)
        {
            List<DetalleVenta> detalles = dtg_detalle_vnt.Items.Cast<DetalleVenta>().ToList();
            ventaCN.insertarVenta(float.Parse(txt_total_vnt.Text), detalles);
            MessageBox.Show("Venta registrada exitosamente");
            limpiar();
        }

        private void limpiar()
        {
            productoSeleccionadoRow = null;
            dtg_detalle_vnt.ItemsSource = null;
            dtg_detalle_vnt.Items.Clear();
            dtg_lista_de_prod_vnt.ItemsSource = null;
            dtg_lista_de_prod_vnt.Items.Clear();
            txt_total_vnt.Text = "0";
            txt_cantidad.Clear();
            txt_nomb_prod_vnt.Clear();
        }
    }
}
