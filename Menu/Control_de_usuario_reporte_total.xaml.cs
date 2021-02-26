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
    /// Lógica de interacción para Control_de_usuario_reporte_total.xaml
    /// </summary>
    public partial class Control_de_usuario_reporte_total : UserControl
    {
        public Control_de_usuario_reporte_total()
        {
            InitializeComponent();
        }
        private void ReportViewer_Load(object sender, EventArgs e)
        {
            // Gastos
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new
            Microsoft.Reporting.WinForms.ReportDataSource();
            Db_Asociacion2020BDataSet dataset = new Db_Asociacion2020BDataSet();
            dataset.BeginInit();
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = dataset.tblGasto;
            this._reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this._reportViewer.LocalReport.ReportPath = "..\\..\\ReportTotal.rdlc";
            dataset.EndInit();
            Db_Asociacion2020BDataSetTableAdapters.tblGastoTableAdapter
            gastosTableAdapter = new
            Db_Asociacion2020BDataSetTableAdapters.tblGastoTableAdapter();
            gastosTableAdapter.ClearBeforeFill = true;
            gastosTableAdapter.FillBy(dataset.tblGasto, Configuracion.SemestreActual);

            //Articulos
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSourceArticulos = new
            Microsoft.Reporting.WinForms.ReportDataSource();
            Db_Asociacion2020BDataSet datasetArticulos = new Db_Asociacion2020BDataSet();
            datasetArticulos.BeginInit();
            reportDataSourceArticulos.Name = "DataSet2";
            reportDataSourceArticulos.Value = datasetArticulos.tblArticulo;
            this._reportViewer.LocalReport.DataSources.Add(reportDataSourceArticulos);
            this._reportViewer.LocalReport.ReportPath = "..\\..\\ReportTotal.rdlc";
            datasetArticulos.EndInit();
            Db_Asociacion2020BDataSetTableAdapters.tblArticuloTableAdapter
            articulosTableAdapter = new
            Db_Asociacion2020BDataSetTableAdapters.tblArticuloTableAdapter();
            articulosTableAdapter.ClearBeforeFill = true;
            articulosTableAdapter.FillBy(datasetArticulos.tblArticulo, Configuracion.SemestreActual);

            //Productos
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSourceProductos = new
            Microsoft.Reporting.WinForms.ReportDataSource();
            Db_Asociacion2020BDataSet datasetProductos = new Db_Asociacion2020BDataSet();
            datasetProductos.BeginInit();
            reportDataSourceProductos.Name = "DataSet3";
            reportDataSourceProductos.Value = datasetProductos.tblProductos;
            this._reportViewer.LocalReport.DataSources.Add(reportDataSourceProductos);
            this._reportViewer.LocalReport.ReportPath = "..\\..\\ReportTotal.rdlc";
            datasetArticulos.EndInit();
            Db_Asociacion2020BDataSetTableAdapters.tblProductosTableAdapter
            productosTableAdapter = new
            Db_Asociacion2020BDataSetTableAdapters.tblProductosTableAdapter();
            productosTableAdapter.ClearBeforeFill = true;
            productosTableAdapter.FillBy(datasetProductos.tblProductos, Configuracion.SemestreActual);

            // Estudiante
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSourceAportaciones = new
            Microsoft.Reporting.WinForms.ReportDataSource();
            Db_Asociacion2020BDataSet datasetAportaciones = new Db_Asociacion2020BDataSet();
            datasetAportaciones.BeginInit();
            reportDataSourceAportaciones.Name = "DataSet4";
            reportDataSourceAportaciones.Value = datasetAportaciones.EstudiantesAportantes;
            this._reportViewer.LocalReport.DataSources.Add(reportDataSourceAportaciones);
            this._reportViewer.LocalReport.ReportPath = "..\\..\\ReportTotal.rdlc";
            datasetAportaciones.EndInit();
            Db_Asociacion2020BDataSetTableAdapters.EstudiantesAportantesTableAdapter
            estudiantesAportantesTableAdapter = new
            Db_Asociacion2020BDataSetTableAdapters.EstudiantesAportantesTableAdapter();
            estudiantesAportantesTableAdapter.ClearBeforeFill = true;
            estudiantesAportantesTableAdapter.FillBy(datasetAportaciones.EstudiantesAportantes, Configuracion.SemestreActual);

            // Prestamos
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSourcePrestamos = new
            Microsoft.Reporting.WinForms.ReportDataSource();
            Db_Asociacion2020BDataSet datasetPrestamos = new Db_Asociacion2020BDataSet();
            datasetPrestamos.BeginInit();
            reportDataSourcePrestamos.Name = "DataSetPrestamo";
            reportDataSourcePrestamos.Value = datasetPrestamos.Prestamos;
            this._reportViewer.LocalReport.DataSources.Add(reportDataSourcePrestamos);
            this._reportViewer.LocalReport.ReportPath = "..\\..\\ReportTotal.rdlc";
            datasetPrestamos.EndInit();
            Db_Asociacion2020BDataSetTableAdapters.PrestamosTableAdapter
            tblDevolucionesTableAdapter = new
            Db_Asociacion2020BDataSetTableAdapters.PrestamosTableAdapter();
            tblDevolucionesTableAdapter.ClearBeforeFill = true;
            tblDevolucionesTableAdapter.FillBy(datasetPrestamos.Prestamos, Configuracion.SemestreActual);

            // Ventas
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSourceVentas = new
            Microsoft.Reporting.WinForms.ReportDataSource();
            Db_Asociacion2020BDataSet datasetVentas = new Db_Asociacion2020BDataSet();
            datasetVentas.BeginInit();
            reportDataSourceVentas.Name = "VentasDataSet";
            reportDataSourceVentas.Value = datasetVentas.Ventas;
            this._reportViewer.LocalReport.DataSources.Add(reportDataSourceVentas);
            this._reportViewer.LocalReport.ReportPath = "..\\..\\ReportTotal.rdlc";
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
