using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Space_shooter.Poverups.WeaponPowerup;

namespace Space_shooter.Logic
{
    public class Player :IShip
    {

        Point position;
        public bool left;
        public Point Position { get => position; set => position = value; }
        public WeaponType Weapon { get; set; }
        public bool IsDead { get; set; }
        public bool IsMoving { get; set; }

        public Player(Point position)
        {
            IsMoving = false;
            this.position = position;
            Weapon = WeaponType.None;
        }
        public void Move(System.Windows.Size area)
        {
            if (left) MoveLeft(area);
            else MoveRight(area);
        }
        public void MoveLeft(System.Windows.Size area)
        {
            Point newposition = new System.Drawing.Point(position.X -10, position.Y);
            if (newposition.X >= 0)
            {
                Position = newposition;
            }
        }
        public void MoveRight(System.Windows.Size area)
        {
            Point newposition = new System.Drawing.Point(position.X + 10, position.Y);
            if (newposition.X <= area.Width)
            {
                Position = newposition;
            }
        }
    }
}
