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
    /// Lógica de interacción para Control_de_usuario_Administracion_Estudiantes.xaml
    /// </summary>
    public partial class Control_de_usuario_Administracion_Estudiantes : UserControl
    {

        private int idEst = 0;
        CN_Estudiante objetoCN = new CN_Estudiante();
        public Control_de_usuario_Administracion_Estudiantes()
        {
            InitializeComponent();
            ListarTipodeAportacion();
            ListarCarrera();
       
        }
       

        public void ListarTipodeAportacion()
        {
            CN_Estudiante objCNPro = new CN_Estudiante();
            cmb_Estado_de_aportacion.ItemsSource = objCNPro.GetTipodeAportacion();
            this.cmb_Estado_de_aportacion.DisplayMemberPath = " Tipodeaportacion";
            this.cmb_Estado_de_aportacion.SelectedValuePath = "Id_TA";
        }
        public void ListarCarrera()
        {
            CN_Estudiante objCNPro = new CN_Estudiante();
            cmb_carrera_admin_estudiantes.ItemsSource = objCNPro.GetCarrera();
            this.cmb_carrera_admin_estudiantes.DisplayMemberPath = "nombreCarrera";
            this.cmb_carrera_admin_estudiantes.SelectedValuePath = "Id";
            
        }

        private void cmb_Estado_de_aportacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_Estado_de_aportacion.Text != "Aportante")
              {
                  txt_Valor_de_aportacion.Text = "20,00";
              }

             if (cmb_Estado_de_aportacion.Text != "No Aportante" )
              {
                  txt_Valor_de_aportacion.Text = "0,00";
              }

           
        }
        private void limpiar()
        {
            cmb_Estado_de_aportacion.Text="--Seleccione una opción--";
            txt_nombre_admin_estudiantes.Clear();
            txt_cedula_admin_estudiantes.Clear();
            cmb_carrera_admin_estudiantes.Text = "--Seleccione una carrera--";
            txt_correo_admin_estudiantes.Clear();
            txt_telefono_admin_estudiantes.Clear();
            txt_Valor_de_aportacion.Clear();
        }

        private void limpiar1() {

            cmb_Estado_de_aportacion.Text = "--Seleccione una opción--";
            txt_nombre_admin_estudiantes.Clear();
            txt_cedula_admin_estudiantes.Clear();
            cmb_carrera_admin_estudiantes.Text = "--Seleccione una carrera--";
            txt_correo_admin_estudiantes.Clear();
            txt_telefono_admin_estudiantes.Clear();
            txt_Valor_de_aportacion.Clear();
            txtBuscar_marca.Text = "--Ingrese el nombre del estudiante";
            txtBuscar_adminestudiantes.Text = "";


        }

        private void btn_agregar_admin_estudiantes_Click(object sender, RoutedEventArgs e)
        {
            CN_Estudiante NEST = new CN_Estudiante();

            if (Validacion_Identificacion.VerificaIdentificacion(txt_cedula_admin_estudiantes.Text) == false || txt_cedula_admin_estudiantes.Text.Length < 10)
            {
                MessageBox.Show("Verifique que la cédula ingresada sea válida", "Error en validación de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
            else if (string.IsNullOrEmpty(txt_nombre_admin_estudiantes.Text))
            {
                MessageBox.Show("Verifique que se ha llenado correctamente el Nombre del estudiante","Error en ingreso de datos",MessageBoxButton.OK,MessageBoxImage.Information);
                return;
            }
            else if (cmb_Estado_de_aportacion.SelectedIndex == -1)
            {
                MessageBox.Show("Verifique que se escogío correctamente el Estado de Aportación","Error en ingreso de datos",MessageBoxButton.OK,MessageBoxImage.Information);
                return;

            }
            else if (cmb_carrera_admin_estudiantes.SelectedIndex == -1)
            {
                MessageBox.Show("Verifique que se escogío correctamente la Carrera","Error en ingreso de datos",MessageBoxButton.OK,MessageBoxImage.Information);
                return;

            }
            else if (Regex.IsMatch(txt_correo_admin_estudiantes.Text,
                 @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"))
            {
                
            }
            else {

                MessageBox.Show("Verifique que el correo ingresado sea correcto","Error en validación de datos",MessageBoxButton.OK,MessageBoxImage.Information);
                return;


            } if (string.IsNullOrEmpty(txt_telefono_admin_estudiantes.Text))
            {
                MessageBox.Show("Verifique que ha llenado correctamente el Teléfono del estudiante","Error en ingreso de datos",MessageBoxButton.OK,MessageBoxImage.Information);
                return;

            }
       
            else if (Validacion_Identificacion.VerificaIdentificacion(txt_cedula_admin_estudiantes.Text)== true)
            {
                try
                { 
                    char separator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    int id =  NEST.Insertar(Convert.ToInt32(cmb_Estado_de_aportacion.SelectedValue),txt_nombre_admin_estudiantes.Text, txt_cedula_admin_estudiantes.Text, Convert.ToInt32(cmb_carrera_admin_estudiantes.SelectedValue),txt_correo_admin_estudiantes.Text,txt_telefono_admin_estudiantes.Text,txt_Valor_de_aportacion.Text.Replace(',', separator));
                    MessageBox.Show("     Estudiante " + txt_nombre_admin_estudiantes.Text + "\n Ingresado correctamente", "Ingreso de datos exitoso", MessageBoxButton.OK, MessageBoxImage.Information); ;

                    limpiar();
                }
                catch (Exception)
                {
                    MessageBox.Show("El estudiante ya existe", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btn_actualizar_admin_estudiantes_Click(object sender, RoutedEventArgs e)
        {
            CN_Estudiante NEST = new CN_Estudiante();

            if (Regex.IsMatch(txt_correo_admin_estudiantes.Text,
                 @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
                 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"))
            {

            }
            else
            {

                MessageBox.Show("Verifique que el correo ingresado sea correcto", "Error en validación de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;


            }
            if (string.IsNullOrEmpty(txt_telefono_admin_estudiantes.Text))
            {
                MessageBox.Show("Verifique que ha llenado correctamente el Teléfono del estudiante", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
            else if (cmb_Estado_de_aportacion.SelectedIndex == -1)
            {
                MessageBox.Show("Verifique que se escogío correctamente el Estado de Aportación", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
            else if (cmb_carrera_admin_estudiantes.SelectedIndex == -1)
            {
                MessageBox.Show("Verifique que se escogío correctamente la Carrera", "Error en ingreso de datos", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
            else
            {
                try
                {
                    char separator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    NEST.Actualizar(Convert.ToInt32(cmb_Estado_de_aportacion.SelectedValue), txt_nombre_admin_estudiantes.Text, txt_cedula_admin_estudiantes.Text, Convert.ToInt32(cmb_carrera_admin_estudiantes.SelectedValue), txt_correo_admin_estudiantes.Text, txt_telefono_admin_estudiantes.Text, txt_Valor_de_aportacion.Text.Replace(',', separator), idEst);
                    MessageBox.Show("     Estudiante " + txt_nombre_admin_estudiantes.Text + "\n Actualizado correctamente", "Actualización de datos exitoso", MessageBoxButton.OK, MessageBoxImage.Information);

                    dtg_admin_estudiantes.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = new DataTable() });
                    limpiar1();
                    btn_agregar_admin_estudiantes.IsEnabled = true;
                    btn_actualizar_admin_estudiantes.IsEnabled = false;
                    btn_eliminar_admin_estudiantes.IsEnabled = false;
                    txt_nombre_admin_estudiantes.IsEnabled = true;
                    txt_cedula_admin_estudiantes.IsEnabled = true;
                } catch (Exception)
                {
                    MessageBox.Show("El estudiante ya existe", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }


        }

        private void btn_eliminar_admin_estudiantes_Click(object sender, RoutedEventArgs e)
        {

            if (dtg_admin_estudiantes.SelectedItem == null)
            {
                MessageBox.Show("Verifique si se selecciono algún campo");


            }else if (cmb_Estado_de_aportacion.Text=="Aportante"){

                MessageBox.Show("Estudiante " + txt_nombre_admin_estudiantes.Text + " es aportante " + "\n       no puede ser eliminado", "Error de eliminación", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {

                objetoCN.EliminarEst(idEst);
                MessageBox.Show("Estudiante " + txt_nombre_admin_estudiantes.Text + " se ha eliminado correctamente ", "Eliminación correcta", MessageBoxButton.OK, MessageBoxImage.Information);
                dtg_admin_estudiantes.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = new DataTable() });
                limpiar();
            }
        }

        private void btn_buscar_admin_estudiantes_Click(object sender, RoutedEventArgs e)
        {
            dtg_admin_estudiantes.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = objetoCN.Buscar(txtBuscar_adminestudiantes.Text) });
            limpiar();


        }




        private void txtBuscar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBuscar_adminestudiantes.Text))

            {
                txtBuscar_adminestudiantes.Visibility = System.Windows.Visibility.Collapsed;

                txtBuscar_marca.Visibility = System.Windows.Visibility.Visible;

            }
        }

        private void txtBuscar_marca_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBuscar_marca.Visibility = System.Windows.Visibility.Collapsed;

            txtBuscar_adminestudiantes.Visibility = System.Windows.Visibility.Visible;

            txtBuscar_adminestudiantes.Focus();
        }

        private void txt_cedula_admin_estudiantes_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void txt_telefono_admin_estudiantes_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void txt_nombre_admin_estudiantes_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[A-Za-zÑñÁáÉéÍíÓóÚúÜü]"))
            {
                e.Handled = true;
            }
        }

        private void dtg_admin_estudiantes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CultureInfo ci = new CultureInfo("en-us");

            DataRowView rowView = dtg_admin_estudiantes.SelectedItem as DataRowView;
                if (dtg_admin_estudiantes.SelectedItem != null)
                {
                }
                if (rowView != null)
                {
                    char separator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                    cmb_Estado_de_aportacion.SelectedValue = rowView[1].ToString();
                    txt_nombre_admin_estudiantes.Text = rowView[4].ToString();
                    txt_cedula_admin_estudiantes.Text = rowView[5].ToString();
                    cmb_carrera_admin_estudiantes.SelectedValue = rowView[6].ToString();
                    txt_correo_admin_estudiantes.Text = rowView[8].ToString();
                    txt_telefono_admin_estudiantes.Text = rowView[9].ToString();
                    txt_Valor_de_aportacion.Text = rowView[10].ToString().Replace(separator, ','); 
                    idEst = Convert.ToInt32(rowView[0].ToString());

                    btn_agregar_admin_estudiantes.IsEnabled = false;
                    btn_actualizar_admin_estudiantes.IsEnabled = true;
                    btn_eliminar_admin_estudiantes.IsEnabled = true;
                

                    cmb_Estado_de_aportacion.IsEnabled = true;
                    txt_nombre_admin_estudiantes.IsEnabled = false;
                    txt_cedula_admin_estudiantes.IsEnabled = false;
                    cmb_carrera_admin_estudiantes.IsEnabled = true;
                    txt_telefono_admin_estudiantes.IsEnabled = true;
                    txt_correo_admin_estudiantes.IsEnabled = true;
                    txt_Valor_de_aportacion.IsEnabled = false;

                }
                else
                {
                btn_agregar_admin_estudiantes.IsEnabled = true;
                btn_actualizar_admin_estudiantes.IsEnabled = false;
                btn_eliminar_admin_estudiantes.IsEnabled = false;


                cmb_Estado_de_aportacion.IsEnabled = true;
                txt_nombre_admin_estudiantes.IsEnabled = true;
                txt_cedula_admin_estudiantes.IsEnabled = true;
                cmb_carrera_admin_estudiantes.IsEnabled = true;
                txt_telefono_admin_estudiantes.IsEnabled = true;
                txt_correo_admin_estudiantes.IsEnabled = true;
                txt_Valor_de_aportacion.IsEnabled = false;

            }

            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            btn_agregar_admin_estudiantes.IsEnabled = true;
            btn_actualizar_admin_estudiantes.IsEnabled = false;
            btn_eliminar_admin_estudiantes.IsEnabled = false;
        }

        private void txt_Valor_de_aportacion_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex _regex = new Regex(@"^[0-9]+(\,)?([0-9]{1,2})?$");
            TextBox textBox = sender as TextBox;
            bool handler = _regex.IsMatch(textBox.Text + e.Text);
            e.Handled = !handler;
        }

        
    }
}
