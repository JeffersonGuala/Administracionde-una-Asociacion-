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
using System.Data;
using System.Threading;
using System.Text.RegularExpressions;

namespace Menu
{
    /// <summary>
    /// Lógica de interacción para Control_de_usuario_inventario_de_productos.xaml
    /// </summary>
    public partial class Control_de_usuario_inventario_de_productos : UserControl
    {
        private CN_Producto productoCN = new CN_Producto();
        private int productoSeleccionado;
        public Control_de_usuario_inventario_de_productos()
        {
            InitializeComponent();
            ListarTipoProductos();
            btn_agregar_prod.IsEnabled = true;
            btn_actualizar_prod.IsEnabled = false;
            btn_eliminar_prod.IsEnabled = false;
        }
        public void ListarTipoProductos()
        {
            cmb_tipo_de_prod.ItemsSource = productoCN.getTipoProductos();
            cmb_tipo_de_prod.DisplayMemberPath = "Tipo_de_Producto";
            cmb_tipo_de_prod.SelectedValuePath = "Id";
        }

        private void txtBuscar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBuscar_nomb_prod.Text))

            {
                txtBuscar_nomb_prod.Visibility = System.Windows.Visibility.Collapsed;

                txtBuscar_marca.Visibility = System.Windows.Visibility.Visible;

            }
        }

        private void txtBuscar_marca_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBuscar_marca.Visibility = System.Windows.Visibility.Collapsed;

            txtBuscar_nomb_prod.Visibility = System.Windows.Visibility.Visible;

            txtBuscar_nomb_prod.Focus();
        }

        private void btn_agregar_prod_Click(object sender, RoutedEventArgs e)
        {

            CN_Producto NProd = new CN_Producto();

            if (string.IsNullOrEmpty(txt_nomb_prod.Text))
            {

                MessageBox.Show("Verifique que se ha llenado correctamente el Nombre del producto", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else if (cmb_tipo_de_prod.SelectedIndex == -1)
            {
                MessageBox.Show("Verifique que se escogío correctamente el tipo del producto", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
            else if (string.IsNullOrEmpty(txt_marca_prod.Text))
            {
                MessageBox.Show("Verifique que se ha llenado correctamente la Marca del producto", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
            else if (string.IsNullOrEmpty(txt_stock_prod.Text) || Convert.ToInt32(txt_stock_prod.Text) < 1)
            {
                MessageBox.Show("Verifique que se ha llenado correctamente el Stock del producto", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
            else if (string.IsNullOrEmpty(txt_precio_total.Text))
            {
                MessageBox.Show("Verifique que se ha llenado correctamente el Precio Total de Compra del producto", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
            else if (string.IsNullOrEmpty(txt_pvp.Text))
            {
                MessageBox.Show("Verifique que se ha llenado correctamente el (P.V.P) del producto", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
            else
            {


                char separator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                productoCN.insertarProducto(
                    txt_nomb_prod.Text,
                    txt_marca_prod.Text,
                    float.Parse(txt_precio_total.Text.Replace(',', separator)),
                    float.Parse(txt_pvp.Text.Replace(',', separator)),
                    int.Parse(txt_stock_prod.Text),
                    Convert.ToInt32(cmb_tipo_de_prod.SelectedValue)
                );
                MessageBox.Show("     Producto " + txt_nomb_prod.Text + "\n Ingresado correctamente", "Ingreso de datos exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
                limpiarForm();
                cargarProductos();

            }
        }

        private void cargarProductos()
        {
            dtg_listado_productos.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = productoCN.listarProductos() });
        }

        private void limpiarForm()
        {
            txt_nomb_prod.Clear();
            txt_marca_prod.Clear();
            txt_precio_total.Clear();
            txt_pvp.Clear();
            txt_stock_prod.Clear();
            cmb_tipo_de_prod.Text = "-- Seleccione una opción --";
            lbl_error.Content = "";
            lbl_pvp1.Content = "";
            productoSeleccionado = 0;
        }

        private void btn_buscar_nomb_prod_Click(object sender, RoutedEventArgs e)
        {
            dtg_listado_productos.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = productoCN.buscarProductos(txtBuscar_nomb_prod.Text) });
            limpiarForm();
        }

        private void dtg_listado_productos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView rowView = dtg_listado_productos.SelectedItem as DataRowView;
            if (rowView != null)
            {
                char separator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                productoSeleccionado = Convert.ToInt32(rowView[0]);
                txt_nomb_prod.Text = rowView[1].ToString();
                txt_marca_prod.Text = rowView[2].ToString();
                txt_precio_total.Text = rowView[3].ToString().Replace(separator, ',');
                txt_stock_prod.Text = rowView[4].ToString();
                txt_pvp.Text = rowView[5].ToString().Replace(separator, ',');
                cmb_tipo_de_prod.SelectedValue = rowView[6].ToString();
                btn_agregar_prod.IsEnabled = false;
                btn_actualizar_prod.IsEnabled = true;
                btn_eliminar_prod.IsEnabled = true;
                cmb_tipo_de_prod.IsEnabled = false;
                txt_nomb_prod.IsEnabled = false;
                txt_marca_prod.IsEnabled = false;
                txt_stock_prod.IsEnabled = true;
                txt_precio_total.IsEnabled = true;
                txt_pvp.IsEnabled = true;

            }
            else
            {
                btn_agregar_prod.IsEnabled = true;
                btn_actualizar_prod.IsEnabled = false;
                btn_eliminar_prod.IsEnabled = false;

                cmb_tipo_de_prod.IsEnabled = true;
                txt_nomb_prod.IsEnabled = true;
                txt_marca_prod.IsEnabled = true;
                txt_stock_prod.IsEnabled = true;
                txt_precio_total.IsEnabled = true;
                txt_pvp.IsEnabled = true;
            }
        }

        private void btn_actualizar_prod_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_stock_prod.Text) || Convert.ToInt32(txt_stock_prod.Text) < 1)
            {
                MessageBox.Show("Verifique que se ha llenado correctamente el Stock del producto", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
            else if (string.IsNullOrEmpty(txt_precio_total.Text))
            {
                MessageBox.Show("Verifique que se ha llenado correctamente el Precio Total de Compra del producto", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
            else if (string.IsNullOrEmpty(txt_pvp.Text))
            {
                MessageBox.Show("Verifique que se ha llenado correctamente el (P.V.P) del producto", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
            else
            {
                char separator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                productoCN.actualizarProducto(
                    productoSeleccionado,
                    txt_nomb_prod.Text,
                    txt_marca_prod.Text,
                    float.Parse(txt_precio_total.Text.Replace(',', separator)),
                    float.Parse(txt_pvp.Text.Replace(',', separator)),
                    int.Parse(txt_stock_prod.Text),
                    Convert.ToInt32(cmb_tipo_de_prod.SelectedValue)
                );
                MessageBox.Show("     Producto " + txt_nomb_prod.Text + "\n Actualizado correctamente", "Actualización de datos exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
                limpiarForm();
                cargarProductos();

                btn_agregar_prod.IsEnabled = true;
                btn_actualizar_prod.IsEnabled = false;
                btn_eliminar_prod.IsEnabled = false;
                txt_nomb_prod.IsEnabled = true;
                txt_marca_prod.IsEnabled = true;
                cmb_tipo_de_prod.IsEnabled = true;
                return;
            }
        }

        private void btn_eliminar_prod_Click(object sender, RoutedEventArgs e)
        {
            if (productoSeleccionado > 0)
            {
                try
                {
                    productoCN.eliminarProducto(productoSeleccionado);
                    MessageBox.Show("Producto " + txt_nomb_prod.Text + " se ha eliminado correctamente ", "Eliminación correcta", MessageBoxButton.OK, MessageBoxImage.Information);
                    limpiarForm();
                    cargarProductos();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }


        private void txt_precio_total_TextChanged(object sender, TextChangedEventArgs e)
        {
            double pvp;
            int porcentaje_de_compra = 80;
            int porcentaje = 100;



            if (!string.IsNullOrEmpty(txt_precio_total.Text) && !string.IsNullOrEmpty(txt_stock_prod.Text))
            {
                try
                {
                    pvp = ((double.Parse(txt_precio_total.Text) / int.Parse(txt_stock_prod.Text)) / porcentaje_de_compra) * porcentaje;

                    double pvp_aux;

                    pvp_aux = Math.Round(pvp * 100.0) / 100.0;

                    lbl_pvp1.Content = "Cálculo aproximado de (P.V.P) = " + pvp_aux.ToString();

                }
                catch (Exception Ex)
                {
                    lbl_pvp1.Content = "Cálculo aproximado de (P.V.P) = Error por falta de datos ";
                }
            }
            else
            {
                lbl_pvp1.Content = "Error detectado por falta de datos";


            }
        }

        private void txt_precio_total_TextInput(object sender, TextCompositionEventArgs e)
        {
            Regex _regex = new Regex(@"^[0-9]+(\,)?([0-9]{1,2})?$");
            TextBox textBox = sender as TextBox;
            bool handler = _regex.IsMatch(textBox.Text + e.Text);
            e.Handled = !handler;
        }

        private void txt_pvp_TextInput(object sender, TextCompositionEventArgs e)
        {
            Regex _regex = new Regex(@"^[0-9]+(\,)?([0-9]{1,2})?$");
            TextBox textBox = sender as TextBox;
            bool handler = _regex.IsMatch(textBox.Text + e.Text);
            e.Handled = !handler;
        }



        private void txt_stock_prod_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void txt_precio_total_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex _regex = new Regex(@"^[0-9]+(\,)?([0-9]{1,2})?$");
            TextBox textBox = sender as TextBox;
            bool handler = _regex.IsMatch(textBox.Text + e.Text);
            e.Handled = !handler;
        }

        private void txt_pvp_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex _regex = new Regex(@"^[0-9]+(\,)?([0-9]{1,2})?$");
            TextBox textBox = sender as TextBox;
            bool handler = _regex.IsMatch(textBox.Text + e.Text);
            e.Handled = !handler;
        }
    }
}
