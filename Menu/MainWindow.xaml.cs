using Menu.Modelo_de_vista;
using MaterialDesignThemes.Wpf;
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
using System.Windows.Threading;
using Capa_de_negocios_ASELEC;
using Transitions;
using Capa_Comun_Aselec;
using Capa_Comun_Aselec.Cache;

namespace Menu
{

    public partial class MainWindow : Window
    {
        FRM_Configuracion conf = new FRM_Configuracion();
        FRM_Ayuda_de_usuario ayud = new FRM_Ayuda_de_usuario();

        public MainWindow()
        {
            InitializeComponent();
            security();
            LoadUserData();
            CargarSemestreActual();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            LBLFECHA.Content = DateTime.Now.ToString("dddd, dd MMMM yyyy");

            if (Usuario_cache.TipoUsuario == 0 || Usuario_cache.TipoUsuario == 1)
            {
                var menuAlquiler = new List<SubItem>();
                menuAlquiler.Add(new SubItem("Gestión de \n prestamos", new Control_de_usuario_gestion_prestamos()));
                menuAlquiler.Add(new SubItem("Gestión de \n devoluciones", new Control_de_usuario_gestion_de_devolucion()));
                var item1 = new ItemMenu("Alquileres", menuAlquiler, PackIconKind.Contract);
                ItemMenu item2 = new ItemMenu("Ventas", new Control_de_usuario_ventas(), PackIconKind.CashRegister);

                Menu.Children.Add(new UserControlMenuItem(item1, this));
                Menu.Children.Add(new UserControlMenuItem(item2, this));
            }
            if (Usuario_cache.TipoUsuario == 0)
            {
                habilitarConfiguracion();
                var menuInventarios = new List<SubItem>();
                menuInventarios.Add(new SubItem("Administración de \n productos de venta", new Control_de_usuario_inventario_de_productos()));
                menuInventarios.Add(new SubItem("Administración de \n artículos de alquiler", new Control_de_usuario_Inventarios()));
                menuInventarios.Add(new SubItem("Administración de \n de bienes", new Control_de_usuario_Gastos()));
                var item0 = new ItemMenu("Inventarios", menuInventarios, PackIconKind.ClipboardCheck);
                var menuEstudiantes = new List<SubItem>();
                menuEstudiantes.Add(new SubItem("Administración de estudiantes", new Control_de_usuario_Administracion_Estudiantes()));
                menuEstudiantes.Add(new SubItem("Lista de estudiantes", new Control_de_usuario_lista_de_estudiantes()));
                var item4 = new ItemMenu("Estudiantes", menuEstudiantes, PackIconKind.AccountEdit);
                var menuReportes = new List<SubItem>();
                menuReportes.Add(new SubItem("Reporte de Ventas", new Control_de_usuario_reporte_de_ventas()));
                menuReportes.Add(new SubItem("Reporte de Alquileres", new Control_de_usuario_reporte_de_alquileres()));
                menuReportes.Add(new SubItem("Reporte de Aportaciones", new Control_de_usuario_reporte_de_aportantes()));
                menuReportes.Add(new SubItem("Reporte de Gastos", new Control_de_usuario_reporte_de_gastos()));
                menuReportes.Add(new SubItem("Reporte Total", new Control_de_usuario_reporte_total()));
                var item5 = new ItemMenu("Reportes", menuReportes, PackIconKind.FileChart);
                Menu.Children.Insert(0, new UserControlMenuItem(item0, this));
                Menu.Children.Insert(3, new UserControlMenuItem(item4, this));
                Menu.Children.Insert(4, new UserControlMenuItem(item5, this));
            }

            if (Usuario_cache.TipoUsuario == 2)
            {
                ItemMenu item7 = new ItemMenu("Control de usuarios", new Control_de_Usuarios(), PackIconKind.User);
                Menu.Children.Add(new UserControlMenuItem(item7, this));

            }


        }

        private void habilitarConfiguracion()
        {
            Button configButton = new Button();
            configButton.Content = "Configuración";
            configButton.Click += btn_configuracion_Click;
            menuItems.Children.Insert(0, configButton);
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            LBLhORA.Content = DateTime.Now.ToLongTimeString();
        }

        internal void SwitchScreen(object sender)
        {
            var screen = ((UserControl)sender);

            if (screen != null)
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);
            }
        }
      

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_configuracion_Click(object sender, RoutedEventArgs e)
        {
            conf = new FRM_Configuracion(this);
            conf.ShowDialog();
        }

        private void btn_ayuda_usuario_Click(object sender, RoutedEventArgs e)
        {
            ayud = new FRM_Ayuda_de_usuario(this);
            ayud.ShowDialog();
        }

        private void btn_cerrar_sesion_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar su sesión actual?", "Confirmación", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();
                FRM_Autenticacion login = new FRM_Autenticacion();
                login.Show();
            }
        }

        private void btn_bienvenido_Click(object sender, RoutedEventArgs e)
        {
            this.StackPanelMain.Children.Clear();
            Control_de_bienvenida lg = new Control_de_bienvenida();
            this.StackPanelMain.Children.Add(lg);
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
            lbl_tipo_de_usuario.Content = Usuario_cache.NombreLog;
            lbl_tipo_de_usuario1.Content = Usuario_cache.NombreLog;
            lbl_nombre_usuario.Content = Usuario_cache.Nombre;
        }

        public void CargarSemestreActual()
        {
            CN_Semestre semestreCN = new CN_Semestre();
            Semestre[] semestres = semestreCN.listarSemestres().ToArray();
            Configuracion.SemestreActual = semestres[0].nombre;
            lbl_semestre.Content = semestres[0].nombre;
        }

        public void UsarSemestreDeConfiguracion()
        {
            lbl_semestre.Content = Configuracion.SemestreActual;
        }
    }

}


