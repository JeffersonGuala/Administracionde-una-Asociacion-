using System;
using System.Collections.Generic;
using Menu.Modelo_de_vista;
#pragma warning disable CS0105 // La directiva using para 'System' aparece previamente en este espacio de nombres
using System;
#pragma warning restore CS0105 // La directiva using para 'System' aparece previamente en este espacio de nombres
#pragma warning disable CS0105 // La directiva using para 'System.Collections.Generic' aparece previamente en este espacio de nombres
using System.Collections.Generic;
#pragma warning restore CS0105 // La directiva using para 'System.Collections.Generic' aparece previamente en este espacio de nombres
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
using Capa_de_negocios_ASELEC;

namespace Menu
{
    /// <summary>
    /// Lógica de interacción para UserControlMenuItem.xaml
    /// </summary>
    public partial class UserControlMenuItem : UserControl
    {
        MainWindow _context;
        public UserControlMenuItem(ItemMenu itemMenu, MainWindow context)
        {
            InitializeComponent();
            _context = context;

            ExpanderMenu.Visibility = itemMenu.SubItems == null ? Visibility.Collapsed : Visibility.Visible;
            ListViewItemMenu.Visibility = itemMenu.SubItems == null ? Visibility.Visible : Visibility.Collapsed;
          

            this.DataContext = itemMenu;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _context.SwitchScreen(((SubItem)((ListView)sender).SelectedItem).Screen);
            
        }

        private void ListViewItemMenu_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _context.SwitchScreen(((ItemMenu)((ListBoxItem)sender).DataContext).Screen);
        }
    }
}
