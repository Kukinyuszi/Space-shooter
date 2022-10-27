using Space_shooter.Models;
using Space_shooter.Poverups;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_shooter.Logic
{
    public interface IGameModel
    {

        event EventHandler Changed;

        Player Player { get; set; }
        Boss Boss { get; set; }
        List<Laser> Lasers { get; set; }
        List<Asteroid> Asteroids { get; set; }
        List<EnemyShip> EnemyShips { get; set; }
        List<Powerup> Powerups { get; set; }
        List<Powerup> PlayerPowerups { get; set; }

        int Health { get; }
        int Score { get; }
        int HighScore { get; }

    }
}
