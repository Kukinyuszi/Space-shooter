using Space_shooter.Logic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_shooter.Models
{
    public class Boss :  EnemyShip
    {

        public override Point Position { get; set; }
        public int Health { get; set; }
        public override EnemyEnum Name { get; set; }


        public Boss(System.Windows.Size area,int health) : base(area)
        {
            IsMoving = true;
            Position = new System.Drawing.Point((int)area.Width / 2, -40);
            Name = EnemyEnum.boss;
            Health = health;
        }
        
        public override void Move(System.Windows.Size area)
        {
            Point newposition;
            if (Position.Y <= area.Height / 5)
            {
                newposition = new Point(Position.X, Position.Y + 5);
                Position = newposition;
            }
            else IsMoving = false;
        }
    }
}
