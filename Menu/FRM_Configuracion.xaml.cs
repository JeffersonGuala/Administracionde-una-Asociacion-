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
using Capa_Comun_Aselec.Cache;

namespace Menu
{
    /// <summary>
    /// Lógica de interacción para FRM_Configuracion.xaml
    /// </summary>
    public partial class FRM_Configuracion : Window
    {
        private MainWindow padre;
        private CN_Semestre objetoCN = new CN_Semestre();
        private IEnumerable<Semestre> semestres;
        public FRM_Configuracion()
        {
            InitializeComponent();
        }
        public FRM_Configuracion(MainWindow parametro)
        {
            InitializeComponent();
            padre = parametro;
            semestres= objetoCN.listarSemestres();
            cc_semestres.Collection = semestres.Select(semestre => semestre.nombre);
            cmb_semestre.SelectedValue = Configuracion.SemestreActual;
            int maxYear = int.Parse(semestres.First().nombre.Split('-')[0]);
            cmb_semestre_year.ItemsSource = Enumerable.Range(maxYear, 10);
            CalendarDateRange rangoMinimo = new CalendarDateRange(DateTime.MinValue, semestres.First().fechaFin);
            dp_fechaInicio.BlackoutDates.Add(rangoMinimo);
            dp_fechaFin.BlackoutDates.Add(rangoMinimo);
            dp_fechaInicio.DisplayDate = semestres.First().fechaFin.AddDays(1);
            dp_fechaFin.DisplayDate = semestres.First().fechaFin.AddDays(1);
        }

        private void btn_cerrar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar el menú de configuración?", "Confirmación", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void cmb_semestre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            c_nuevoSemestre.Visibility = Visibility.Hidden;
            string text = (sender as ComboBox).SelectedItem as string;
            if (text == null)
            {
                c_nuevoSemestre.Visibility = Visibility.Visible;
            }
        }

        private void sp_fechaInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker dp = (sender as DatePicker);
            if (dp.SelectedDate.HasValue) {
                DateTime date = dp.SelectedDate.Value;
                CalendarDateRange rangoMinimo = new CalendarDateRange(DateTime.MinValue, date);
                dp_fechaFin.SelectedDate = null;
                dp_fechaFin.DisplayDate = date.AddDays(1);
                dp_fechaFin.BlackoutDates.Add(rangoMinimo);
            }
        }

        private void btn_guardar_cambios_Click(object sender, RoutedEventArgs e)
        {
            if(cmb_semestre.Text == "Nuevo Semestre")
            {
                string idSemestre = cmb_semestre_year.Text + "-" + cmb_semestre_nombre.Text;
                if(semestres.Any((val) => val.nombre == idSemestre))
                {
                    MessageBox.Show("Ese semestre ya existe");
                }
                else
                {
                    objetoCN.ingresarNuevoSemestre(idSemestre, dp_fechaInicio.SelectedDate.Value, dp_fechaFin.SelectedDate.Value);
                    Configuracion.SemestreActual = idSemestre;
                    padre.UsarSemestreDeConfiguracion();
                    this.Close();
                }
            } else
            {
                Configuracion.SemestreActual = cmb_semestre.SelectedItem.ToString();
                padre.UsarSemestreDeConfiguracion();
                this.Close();
            }
        }
    }
}
