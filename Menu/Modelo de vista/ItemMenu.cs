
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Menu.Modelo_de_vista
{
    public class ItemMenu
    {
        public ItemMenu(string header, List<SubItem> subItems, PackIconKind icon)
        {
            Header = header;
            SubItems = subItems;
            Icon = icon;
        }

        public ItemMenu(string header, UserControl screen, PackIconKind icon)
        {
            Header = header;
            Screen = screen;
            Icon = icon;
        }
        public ItemMenu( Button b ,UserControl screen)
        {
            Button = b;
            Screen = screen;
            
        }

        public string Header { get; private set; }
        public PackIconKind Icon { get; private set; }
        public List<SubItem> SubItems { get; private set; }
        public Button Button { get; }
        public UserControl Screen { get; private set; }
    }
}