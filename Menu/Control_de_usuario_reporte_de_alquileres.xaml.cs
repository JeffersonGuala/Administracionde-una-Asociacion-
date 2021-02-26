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
    /// Lógica de interacción para Control_de_usuario_reporte_de_alquileres.xaml
    /// </summary>
    public partial class Control_de_usuario_reporte_de_alquileres : UserControl
    {
        public Control_de_usuario_reporte_de_alquileres()
        {
            InitializeComponent();
        }
        private void ReportViewer_Load(object sender, EventArgs e)
        {
            // Gastos
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSourcePrestamos = new
            Microsoft.Reporting.WinForms.ReportDataSource();
            Db_Asociacion2020BDataSet dataset = new Db_Asociacion2020BDataSet();
            dataset.BeginInit();
            reportDataSourcePrestamos.Name = "DataSetPrestamo";
            reportDataSourcePrestamos.Value = dataset.Prestamos;
            this._reportViewer.LocalReport.DataSources.Add(reportDataSourcePrestamos);
            this._reportViewer.LocalReport.ReportPath = "..\\..\\ReportPrestamos.rdlc";
            dataset.EndInit();
            Db_Asociacion2020BDataSetTableAdapters.PrestamosTableAdapter
            tblDevolucionesTableAdapter = new
            Db_Asociacion2020BDataSetTableAdapters.PrestamosTableAdapter();
            tblDevolucionesTableAdapter.ClearBeforeFill = true;
            tblDevolucionesTableAdapter.FillBy(dataset.Prestamos, Configuracion.SemestreActual);
            _reportViewer.RefreshReport();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _reportViewer.Reset();
            ReportViewer_Load(sender, e);
        }

        private void btn_actualizar_Click(object sender, RoutedEventArgs e)
        {
            _reportViewer.Reset();
            ReportViewer_Load(sender, e);
        }
    }
}
