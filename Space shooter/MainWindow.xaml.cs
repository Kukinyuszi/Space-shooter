using Space_shooter.Logic;
using Space_shooter.Poverups;
using Space_shooter.Renderer;
using Space_shooter.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;
using System.Xml.Linq;

namespace Space_shooter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpaceShooterLogic logic;
        public int score;
        public int health = 100;
        private int wait = 0;
        public MainWindow()
        {
            StartGame();
        }
        private void StartGame()
        {
            logic = new SpaceShooterLogic();
            MainMenuWindow mmw = new MainMenuWindow(logic);

            if (mmw.ShowDialog() == false) this.Close();
        }
        private void Dt_Tick(object? sender, EventArgs e)
        {
            Paused();
            UIHelper();
            logic.TimeStep();

        }

        private void Paused()
        {
            if (logic.paused)
            {
                pausedbox.Content = "Paused";
            }
            else
            {
                pausedbox.Content = null;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            logic.GameOver += Logic_GameOver;
            logic.PowerUpPickedUp += PowerUpPickedUp;
            display.SetupModel(logic);
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(20);
            dt.Tick += Dt_Tick;
            dt.Start();
            display.SetupSizes(new Size(MyCanvas.ActualWidth, MyCanvas.ActualHeight));
            logic.SetupSizes(new System.Windows.Size(MyCanvas.ActualWidth, MyCanvas.ActualHeight));
        }

        private void Logic_GameOver(object? sender, EventArgs e)
        {
            healthText.Content = $"Health: 0";
            var result = MessageBox.Show("Game Over");
            if (result == MessageBoxResult.OK)
            {
                this.Close();

            }
        }
        private void UIHelper()
        {


            scoreText.Content = logic.score;
            highscoreText.Content = $"Highscore: {logic.HighScore}";
            if (logic.Godmode) healthText.Content = "∞";
            else healthText.Content = logic.health;
            if (logic.wpn > 0) weapochatbox.Content = $"Weapon: {logic.wpn * (1000 / 60) / 1000}";
            else weapochatbox.Content = null;
            if (logic.strong) strongchatbox.Content = $"Strength: {logic.strng * (1000 / 60) / 1000}";
            else strongchatbox.Content = null;
            if (logic.rapid) rapidchatbox.Content = $"Rapid fire: {logic.rpd * (1000 / 60) / 1000}";
            else rapidchatbox.Content = null;

            if (logic.rapid)
            {
                rapidText.Content = new Image { Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "/Images/fast.jpg")),Height = 50,Width = 50};
            }
            else
            {
                rapidText.Content = null;
            }
            if (logic.strong)
            {
                strongText.Content = new Image { Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "/Images/strong.jpg")), Height = 50, Width = 50 };
            }
            else
            {
                strongText.Content = null;
            }
            if (logic.shield)
            {
                shieldText.Content = new Image { Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "/Images/shield.jpg")), Height = 50, Width = 50 };
            }
            else
            {
                shieldText.Content = null;
            }
            switch (logic.Player.Weapon)
            {
                case Poverups.WeaponPowerup.WeaponType.Doubleshooter:
                    weaponText.Content = new Image { Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "/Images/double.jpg")), Height = 50, Width = 50 };
                    break;
                case Poverups.WeaponPowerup.WeaponType.Tripplehooter:
                    weaponText.Content = new Image { Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "/Images/tripple.jpg")), Height = 50, Width = 50 };
                    break;
                case Poverups.WeaponPowerup.WeaponType.Biggerammo:
                    weaponText.Content = new Image { Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "/Images/bfg.jpg")), Height = 50, Width = 50 };
                    break;
                case Poverups.WeaponPowerup.WeaponType.None:
                    weaponText.Content = null;
                    break;
                default:
                    break;
            }
        }
        private void PowerUpPickedUp(object? sender, EventArgs e)
        {
            if (sender != null)
            {
                var powerup = (sender as Powerup);
                switch (powerup.PowerupType)
                {
                    case Powerup.Type.ExtraScore:
                        chatbox.Content = "Coin picked up";
                        break;
                    case Powerup.Type.MoreHealth:
                        chatbox.Content = "Health picked up";
                        break;
                    case Powerup.Type.RapidFire:
                        chatbox.Content = "Rapid fire picked up";
                        break;
                    case Powerup.Type.Shield:
                        chatbox.Content = "Shield picked up";
                        break;
                    case Powerup.Type.Stronger:
                        chatbox.Content = "Strength picked up";
                        break;
                    case Powerup.Type.Weapon:
                        chatbox.Content = "Weapon picked up";
                        break;
                    case Powerup.Type.None:
                        break;
                    default:
                        break;
                }

                wait = 50;
            }


        }
        private void _PowerupPickedUp()
        {
            if (wait != 0)
            {
                wait--;
            }
            else
            {
                chatbox.Content = null;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (logic != null)
            {
                display.SetupSizes(new Size(MyCanvas.ActualWidth, MyCanvas.ActualHeight));
                logic.SetupSizes(new System.Windows.Size(MyCanvas.ActualWidth, MyCanvas.ActualHeight));
            }

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                logic.Controldown(SpaceShooterLogic.Controls.Left);
            }
            else if (e.Key == Key.Right)
            {
                logic.Controldown(SpaceShooterLogic.Controls.Right);
            }
            else if (e.Key == Key.Space)
            {
                logic.Controldown(SpaceShooterLogic.Controls.Shoot);
            }
            else if (e.Key == Key.Escape)
            {
                logic.Controldown(SpaceShooterLogic.Controls.Escape);
            }
            else if (e.Key == Key.G)
            {
                logic.Controldown(SpaceShooterLogic.Controls.G);
            }
            else if (e.Key == Key.O)
            {
                logic.Controldown(SpaceShooterLogic.Controls.O);
            }
            else if (e.Key == Key.D)
            {
                logic.Controldown(SpaceShooterLogic.Controls.D);
            }
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                logic.Controlup(SpaceShooterLogic.Controls.Left);
            }
            else if (e.Key == Key.Right)
            {
                logic.Controlup(SpaceShooterLogic.Controls.Right);
            }
            else if (e.Key == Key.Space)
            {
                logic.Controlup(SpaceShooterLogic.Controls.Shoot);
            }
        }
    }

}
