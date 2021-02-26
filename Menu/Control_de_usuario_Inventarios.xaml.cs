using System;
using System.Collections.Generic;
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
using System.Data;
using System.Text.RegularExpressions;

namespace Menu
{
    /// <summary>
    /// Lógica de interacción para Control_de_usuario_Inventarios.xaml
    /// </summary>
    public partial class Control_de_usuario_Inventarios : UserControl
    {
        private CN_Articulo objetoCN = new CN_Articulo();
        private int articuloSeleccionado;
        public Control_de_usuario_Inventarios()
        {
            InitializeComponent();
            ListarTipos();
            btn_agregar_art.IsEnabled = true;
            btn_actualizar_art.IsEnabled = false;
            btn_eliminar_art.IsEnabled = false;
        }

        public void ListarTipos()
        {
            cmb_tipo_art.ItemsSource = objetoCN.getTipoArticulo();
            cmb_tipo_art.DisplayMemberPath = "Tipo_de_Producto";
            cmb_tipo_art.SelectedValuePath = "Id";
        }

        private void txtBuscar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBuscar_nomb_articulo.Text))

            {
                txtBuscar_nomb_articulo.Visibility = System.Windows.Visibility.Collapsed;

                txtBuscar_marca.Visibility = System.Windows.Visibility.Visible;

            }
        }

        private void txtBuscar_marca_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBuscar_marca.Visibility = System.Windows.Visibility.Collapsed;

            txtBuscar_nomb_articulo.Visibility = System.Windows.Visibility.Visible;

            txtBuscar_nomb_articulo.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CN_Articulo NArt = new CN_Articulo();

            if (string.IsNullOrEmpty(txt_nomb_articulo.Text))
            {

                MessageBox.Show("Verifique que se ha llenado correctamente el Nombre del artículo", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else if (cmb_tipo_art.SelectedIndex == -1)
            {
                MessageBox.Show("Verifique que se escogío correctamente el tipo del artículo", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
            else if (string.IsNullOrEmpty(txt_marca_art.Text))
            {
                MessageBox.Show("Verifique que se ha llenado correctamente la Marca del artículo", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
            else if (string.IsNullOrEmpty(txt_cantidad_art.Text) || Convert.ToInt32(txt_cantidad_art.Text) < 1)
            {
                MessageBox.Show("Verifique que se ha llenado correctamente la Cantidad del artículo", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
            else if (string.IsNullOrEmpty(txt_costo_total.Text))
            {
                MessageBox.Show("Verifique que se ha llenado correctamente el Precio Total de Compra del artículo", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
            else if (string.IsNullOrEmpty(txt_costo_art.Text))
            {
                MessageBox.Show("Verifique que se ha llenado correctamente el Costo por Hora del artículo", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
            else
            {


                char separator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                objetoCN.insertarArticulo(
                    txt_nomb_articulo.Text,
                    txt_marca_art.Text,
                    float.Parse(txt_costo_total.Text.Replace(',', separator)),
                    float.Parse(txt_costo_art.Text.Replace(',', separator)),
                    int.Parse(txt_cantidad_art.Text),
                    Convert.ToInt32(cmb_tipo_art.SelectedValue)
                );
                MessageBox.Show("Artículo " + txt_nomb_articulo.Text + "\n Ingresado correctamente", "Ingreso de datos exitoso", MessageBoxButton.OK, MessageBoxImage.Information); ;
                limpiarForm();
                cargarArticulos();
            }
        }

        private void limpiarForm()
        {
            txt_nomb_articulo.Clear();
            txt_marca_art.Clear();
            txt_costo_total.Clear();
            txt_costo_art.Clear();
            txt_cantidad_art.Clear();
            cmb_tipo_art.Text = "-- Seleccione una opción --";
            lbl_error.Content = "";
            articuloSeleccionado = 0;
        }

        private void cargarArticulos()
        {
            dtg_articulos.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = objetoCN.listarArticulos() });
        }

        private void btn_buscar_nomb_articulo_Click(object sender, RoutedEventArgs e)
        {
            dtg_articulos.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = objetoCN.buscarArticulo(txtBuscar_nomb_articulo.Text) });
            limpiarForm();
        }

        private void dtg_articulos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView rowView = dtg_articulos.SelectedItem as DataRowView;
            if (rowView != null)
            {
                char separator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                articuloSeleccionado = Convert.ToInt32(rowView[0]);
                txt_nomb_articulo.Text = rowView[1].ToString();
                txt_marca_art.Text = rowView[2].ToString();
                txt_costo_total.Text = rowView[3].ToString().Replace(separator, ',');
                txt_cantidad_art.Text = rowView[4].ToString();
                txt_costo_art.Text = rowView[5].ToString().Replace(separator, ',');
                cmb_tipo_art.SelectedValue = rowView[6].ToString();
                btn_agregar_art.IsEnabled = false;
                btn_actualizar_art.IsEnabled = true;
                btn_eliminar_art.IsEnabled = true;
                cmb_tipo_art.IsEnabled = false;
                txt_nomb_articulo.IsEnabled = false;
                txt_marca_art.IsEnabled = false;
                txt_cantidad_art.IsEnabled = true;
                txt_costo_total.IsEnabled = true;
                txt_costo_art.IsEnabled = true;
            }
            else
            {
                btn_agregar_art.IsEnabled = true;
                btn_actualizar_art.IsEnabled = false;
                btn_eliminar_art.IsEnabled = false;

                cmb_tipo_art.IsEnabled = true;
                txt_nomb_articulo.IsEnabled = true;
                txt_marca_art.IsEnabled = true;
                txt_cantidad_art.IsEnabled = true;
                txt_costo_total.IsEnabled = true;
                txt_costo_art.IsEnabled = true;
            }
        }

        private void btn_actualizar_art_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_cantidad_art.Text) || Convert.ToInt32(txt_cantidad_art.Text) < 1)
            {
                MessageBox.Show("Verifique que se ha llenado correctamente la Cantidad del artículo", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
            else if (string.IsNullOrEmpty(txt_costo_total.Text))
            {
                MessageBox.Show("Verifique que se ha llenado correctamente el Precio Total de Compra del artículo", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
            else if (string.IsNullOrEmpty(txt_costo_art.Text))
            {
                MessageBox.Show("Verifique que se ha llenado correctamente el Costo por Hora del artículo", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
            else
            {
                char separator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                objetoCN.actualizarArticulo(
                    articuloSeleccionado,
                    txt_nomb_articulo.Text,
                    txt_marca_art.Text,
                    float.Parse(txt_costo_total.Text.Replace(',', separator)),
                    float.Parse(txt_costo_art.Text.Replace(',', separator)),
                    int.Parse(txt_cantidad_art.Text),
                    Convert.ToInt32(cmb_tipo_art.SelectedValue)
                );
                MessageBox.Show("     Artículo " + txt_nomb_articulo.Text + "\n Actualizado correctamente", "Actualización de datos exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
                limpiarForm();
                cargarArticulos();

                btn_agregar_art.IsEnabled = true;
                btn_actualizar_art.IsEnabled = false;
                btn_eliminar_art.IsEnabled = false;
                txt_nomb_articulo.IsEnabled = true;
                txt_marca_art.IsEnabled = true;
                cmb_tipo_art.IsEnabled = true;
                return;
            }
        }

        private void btn_eliminar_art_Click(object sender, RoutedEventArgs e)
        {
            if (articuloSeleccionado > 0)
            {
                try
                {
                    objetoCN.eliminarArticulo(articuloSeleccionado);
                    MessageBox.Show("Artículo " + txt_nomb_articulo.Text + " se ha eliminado correctamente ", "Eliminación correcta", MessageBoxButton.OK, MessageBoxImage.Information);
                    limpiarForm();
                    cargarArticulos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void txt_cantidad_art_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void txt_costo_art_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex _regex = new Regex(@"^[0-9]+(\,)?([0-9]{1,2})?$");
            TextBox textBox = sender as TextBox;
            bool handler = _regex.IsMatch(textBox.Text + e.Text);
            e.Handled = !handler;
        }



    }
}
