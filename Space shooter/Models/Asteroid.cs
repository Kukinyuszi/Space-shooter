using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_shooter.Logic
{
    public class Asteroid
    {

        private int speed;

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public bool IsHit { get; set; }

        public Point Position { get => position; set => position = value; }

        static Random r = new Random();
        Point position;
        public Asteroid(System.Windows.Size area, int speed)
        {
            this.speed = speed;
            Position = new System.Drawing.Point(r.Next(25, (int)area.Width - 25), -25);
        }

        public bool Move(System.Windows.Size area)
        {
            System.Drawing.Point newposition =
                new System.Drawing.Point(position.X,position.Y+speed);
            if (newposition.X >= 0 &&
                newposition.X <= area.Width &&
                newposition.Y <= area.Height
                )
            {
                Position = newposition;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

