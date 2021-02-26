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
    /// Lógica de interacción para Control_de_usuario_reporte_de_ventas.xaml
    /// </summary>
    public partial class Control_de_usuario_reporte_de_ventas : UserControl
    {
        public Control_de_usuario_reporte_de_ventas()
        {
            InitializeComponent();
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            // Gastos
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSourceVentas = new
            Microsoft.Reporting.WinForms.ReportDataSource();
            Db_Asociacion2020BDataSet datasetVentas = new Db_Asociacion2020BDataSet();
            datasetVentas.BeginInit();
            reportDataSourceVentas.Name = "VentasDataSet";
            reportDataSourceVentas.Value = datasetVentas.Ventas;
            this._reportViewer.LocalReport.DataSources.Add(reportDataSourceVentas);
            this._reportViewer.LocalReport.ReportPath = "..\\..\\ReportVentas.rdlc";
            datasetVentas.EndInit();
            Db_Asociacion2020BDataSetTableAdapters.VentasTableAdapter
            ventasTableAdapter = new
            Db_Asociacion2020BDataSetTableAdapters.VentasTableAdapter();
            ventasTableAdapter.ClearBeforeFill = true;
            ventasTableAdapter.FillBy(datasetVentas.Ventas, Configuracion.SemestreActual);
            _reportViewer.RefreshReport();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _reportViewer.Reset();
            ReportViewer_Load(sender, e);
        }
    }
}
