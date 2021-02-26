using System;
using System.Collections.Generic;

using System.IO;
using System.Reflection;
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
using System.Windows.Xps.Packaging;
using Capa_de_negocios_ASELEC;

namespace Menu
{
    /// <summary>
    /// Lógica de interacción para FRM_Ayuda_de_usuario.xaml
    /// </summary>
    public partial class FRM_Ayuda_de_usuario : Window
    {
        private MainWindow padre;
       
        public FRM_Ayuda_de_usuario(MainWindow parametro)
        {
            InitializeComponent();
            padre = parametro;

        }
        public FRM_Ayuda_de_usuario()
        {
            InitializeComponent();


        }

        private void GetDocument()
        {
            
            string fileName = Environment.CurrentDirectory.GetFilePath("Manuales_de_usuario\\MANUAL DEL SOFTWARE.xps");
           // System.Diagnostics.Debugger.Break();
            XpsDocument doc = new XpsDocument(fileName, FileAccess.Read);
           
            document_viewer_ayuda.Document = doc.GetFixedDocumentSequence();
        }

        private void document_viewer_ayuda_Loaded(object sender, RoutedEventArgs e)
        {
            GetDocument();
        }
    }
        
    }


