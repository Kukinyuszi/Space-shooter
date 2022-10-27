using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_shooter.Models
{
    public class Explosion
    {
        public Point Position { get; set; }
        public int Counter { get; set; }
        public bool FromPlayer { get; set; }
        public bool IsLaser { get; set; }

        public Explosion(Point position, int counter,bool fromplayer, bool islaser)
        {
            Position = position;
            Counter = counter;
            FromPlayer = fromplayer;
            IsLaser = islaser;
        }
    }
}
