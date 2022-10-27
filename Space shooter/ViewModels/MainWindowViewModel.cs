using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Space_shooter.Logic;
using Space_shooter.Models;
using Space_shooter.Poverups;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Space_shooter.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        private SpaceShooterLogic logic = new SpaceShooterLogic();
        private int score;
        private string health;
        private string poveruppopup;
        private int wait;
        private DispatcherTimer PopupTimer;
        private int highscore;



        public int Score
        {
            get { return score; }
            set
            {
                SetProperty(ref score, value);
            }
        }
        public int HighScore
        {
            get { return highscore; }
            set
            {
                SetProperty(ref highscore, value);
            }
        }

        public string Health
        {
            get
            {
                if (logic.Health == 99999) return "∞";
                else return logic.Health.ToString();
            }
            set
            {
                SetProperty(ref health, value);
            }
        }
        public string PoverupPopup
        {
            get { return poveruppopup; }
            set { SetProperty(ref poveruppopup, value); }
        }


        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }




        public MainWindowViewModel() : this(IsInDesignMode ? null : Ioc.Default.GetService<SpaceShooterLogic>())
        {

        }
        public MainWindowViewModel(SpaceShooterLogic logic)
        {
            //logic.PowerUpPickedUp += PowerUpPickedUp;
            //PopupTimer = new DispatcherTimer();
            //PopupTimer.Interval = TimeSpan.FromSeconds(1);
            //PopupTimer.Tick += PopupTick;
        }

        private void PopupTick(object? sender, EventArgs e)
        {
            if (wait == 0)
            {
                poveruppopup = null;
                PopupTimer.Stop();
            }
            wait--;
        }

        private void PowerUpPickedUp(object? sender, EventArgs e)
        {
            if (sender != null)
            {
                var powerup = (sender as Powerup);
                switch (powerup.PowerupType)
                {
                    case Powerup.Type.ExtraScore:
                        poveruppopup = "Coin picked up";
                        break;
                    case Powerup.Type.MoreHealth:
                        poveruppopup = "Health picked up";
                        break;
                    case Powerup.Type.RapidFire:
                        poveruppopup = "Rapid fire picked up";
                        break;
                    case Powerup.Type.Shield:
                        poveruppopup = "Shield picked up";
                        break;
                    case Powerup.Type.Stronger:
                        poveruppopup = "Strength picked up";
                        break;
                    case Powerup.Type.Weapon:
                        poveruppopup = "Weapon picked up";
                        break;
                    case Powerup.Type.None:
                        break;
                    default:
                        break;
                }
                wait = 6;
                PopupTimer.Start();
            }

        }
    }
}
