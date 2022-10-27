using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO.Packaging;
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

namespace Space_shooter.Logic
{

    public class Laser
    {

        public System.Drawing.Point Position { get; set; }
        private int counter;

        public Vector Laservector { get; set; }
        public double Angle { 
            get
            {
                double temp = Vector.AngleBetween(Laservector, new Vector(0, 1));
                if (Laservector.X < 0) return Math.Abs(temp) + 90;
                else return -temp + 90;
            }
        }

        public int Speed { get; set; }
        public bool Fromplayer { get; set; }
        public bool Big { get; set; }
        public bool IsHit { get; set; }
        public int Ammosize
        {
            get
            {
                if (Big) return 30;
                else return 5;
            }
        }

        public int Counter { get => counter; set => counter = value; }

        public Laser(System.Drawing.Point position, Vector laservector, bool fromplayer, bool big)
        {
            Position = position;
            Laservector = laservector;
            Fromplayer = fromplayer;
            Big = big;
        }


        public bool Move(System.Windows.Size area)
        {
            System.Drawing.Point newposition =
                new System.Drawing.Point(Position.X + (int)Laservector.X,
                Position.Y + (int)Laservector.Y);
            if (newposition.X >= 0 &&
                newposition.X <= area.Width &&
                newposition.Y >= 0 &&
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
