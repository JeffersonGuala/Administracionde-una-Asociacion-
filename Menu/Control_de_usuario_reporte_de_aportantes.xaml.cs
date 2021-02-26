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
using Capa_Comun_Aselec.Cache;
using Capa_de_negocios_ASELEC;

namespace Menu
{
    /// <summary>
    /// Lógica de interacción para Control_de_usuario_reporte_de_aportantes.xaml
    /// </summary>
    public partial class Control_de_usuario_reporte_de_aportantes : UserControl
    {
        public Control_de_usuario_reporte_de_aportantes()
        {
            InitializeComponent();
        }
        private void ReportViewer_Load(object sender, EventArgs e)
        {
            // Gastos
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSourceAportaciones = new
            Microsoft.Reporting.WinForms.ReportDataSource();
            Db_Asociacion2020BDataSet datasetAportaciones = new Db_Asociacion2020BDataSet();
            datasetAportaciones.BeginInit();
            reportDataSourceAportaciones.Name = "DataSet1";
            reportDataSourceAportaciones.Value = datasetAportaciones.EstudiantesAportantes;
            this._reportViewer.LocalReport.DataSources.Add(reportDataSourceAportaciones);
            this._reportViewer.LocalReport.ReportPath = "..\\..\\ReportAfiliados.rdlc";
            datasetAportaciones.EndInit();
            Db_Asociacion2020BDataSetTableAdapters.EstudiantesAportantesTableAdapter
            estudiantesAportantesTableAdapter = new
            Db_Asociacion2020BDataSetTableAdapters.EstudiantesAportantesTableAdapter();
            estudiantesAportantesTableAdapter.ClearBeforeFill = true;
            estudiantesAportantesTableAdapter.FillBy(datasetAportaciones.EstudiantesAportantes, Configuracion.SemestreActual);
            _reportViewer.RefreshReport();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _reportViewer.Reset();
            ReportViewer_Load(sender, e);
        }
    }
}
