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
using Capa_de_negocios_ASELEC;
using System.Runtime.InteropServices;
using Capa_Comun_Aselec;

namespace Menu
{
    /// <summary>
    /// Lógica de interacción para FRM_Autenticacion.xaml
    /// </summary>
    public partial class FRM_Autenticacion : Window
    {
        public FRM_Autenticacion()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea Salir del Sistema?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void Logout(object sender, EventArgs e)
        {
            passbox_contraseña_usuario.Password = "";
            txt_Login_usuario.Text = "";
            text_error.Visibility= Visibility.Hidden;
        }
       
        private void btn_ingresar_Click(object sender, RoutedEventArgs e)
        {
            if (txt_Login_usuario.Text != "Usuario" && txt_Login_usuario.Text.Length > 2)
            {
                if (passbox_contraseña_usuario.Password.Length > 2)
                {
                    CN_Usuario user = new CN_Usuario();
                    var validLogin = user.LoginUser(txt_Login_usuario.Text, passbox_contraseña_usuario.Password);
                    if (validLogin == true)
                    {
                        MainWindow mainMenu = new MainWindow();
                        MessageBox.Show("Bienvenido " + Usuario_cache.Nombre,"LOGIN CORRECTO", MessageBoxButton.OK, MessageBoxImage.Information);
                        mainMenu.Show();
                        mainMenu.Closing += Logout;
                        this.Hide();
                        this.Close();
                    }
                    else
                    {
                        msgError("Nombre de usuario incorrecto o contraseña incorrecta. \n   Por favor inténtelo nuevamente.");
                        passbox_contraseña_usuario_MARCA.Text = "CONTRASEÑA";
                        txt_Login_usuario.Focus();
                    }
                }
                else msgError("Por favor ingrese la contraseña");
            }
            else msgError("Por favor ingrese el usuario");
        }

        private void msgError(string msg)
        {
            text_error.Text = "    " + msg;
            this.text_error.Visibility = Visibility.Visible;
        }

        private void passbox_contraseña_usuario_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(passbox_contraseña_usuario.Password))

            {
                passbox_contraseña_usuario.Visibility = System.Windows.Visibility.Collapsed;

                passbox_contraseña_usuario_MARCA.Visibility = System.Windows.Visibility.Visible;

            }
        }

        private void passbox_contraseña_usuario_MARCA_GotFocus(object sender, RoutedEventArgs e)
        {
            passbox_contraseña_usuario_MARCA.Visibility = System.Windows.Visibility.Collapsed;

            passbox_contraseña_usuario.Visibility = System.Windows.Visibility.Visible;

            passbox_contraseña_usuario.Focus();
        }

        private void txt_Login_usuario_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_Login_usuario.Text))

            {
                txt_Login_usuario.Visibility = System.Windows.Visibility.Collapsed;

                txt_Login_usuario_MARCA.Visibility = System.Windows.Visibility.Visible;

            }
        }

        private void txt_Login_usuario_MARCA_GotFocus(object sender, RoutedEventArgs e)
        {
            txt_Login_usuario_MARCA.Visibility = System.Windows.Visibility.Collapsed;

            txt_Login_usuario.Visibility = System.Windows.Visibility.Visible;

            txt_Login_usuario.Focus();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
