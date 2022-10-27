using Space_shooter.Logic;
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

namespace Space_shooter
{
    /// <summary>
    /// Interaction logic for PlayerSettingsWindow.xaml
    /// </summary>
    public partial class PlayerSettingsWindow : Window
    {
        public PlayerSettingsWindow(ISettings logic)
        {
            this.DataContext = logic;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in PlayerGrid.Children)
            {
                if (item is TextBox t)
                {
                    if (t.Text != "" && t.Text != "Type here your name")
                    {
                        t.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                        this.DialogResult = true;
                    }
                }
            }
        }
    }
}
