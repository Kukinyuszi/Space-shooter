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
using static Space_shooter.Logic.ISettings;

namespace Space_shooter
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        bool music;
        public Difficulty difficulty;
        public SettingsWindow(ISettings settings)
        {
            InitializeComponent();
            this.DataContext = settings;
        }

        public bool Music { get => music; set => music = value; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in MyStackpanel.Children)
            {
                if (item is StackPanel)
                {
                    foreach (var item2 in (item as StackPanel).Children)
                    {

                        if (item2 is TextBox t) t.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                        else if (item2 is CheckBox c) c.GetBindingExpression(CheckBox.IsCheckedProperty).UpdateSource();
                        //else if (item2 is ComboBox l)
                        //{
                        //    switch (l.SelectedItem)
                        //    {
                        //        case "Easy":
                        //            difficulty = Difficulty.Easy;
                        //            break;
                        //        case "Medium":
                        //            difficulty = Difficulty.Medium;
                        //            break;
                        //        case "Hard":
                        //            difficulty = Difficulty.Hard;
                        //            break;
                        //        case "Custom":
                        //            difficulty = Difficulty.Custom;
                        //            break;
                        //        default:
                        //            break;
                        //    }
                        //}
                    }
                }

            }
            this.DialogResult = true;
        }
    }
}
