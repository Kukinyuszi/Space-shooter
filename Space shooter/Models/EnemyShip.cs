using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_shooter.Logic
{
    public class EnemyShip : IShip
    {

        public enum EnemyEnum
        {
            one,two,three,four,boss
        }

        private Point position;
        private bool left;
        private EnemyEnum name;

        public virtual Point Position { get => position; set => position = value; }
        public virtual EnemyEnum Name { get => name; set => name = value; }
        public virtual bool IsHit { get; set; }
        public bool IsDead { get; set; }
        public virtual bool IsMoving { get; set; }

        static Random random = new Random();

        public EnemyShip(System.Windows.Size area)
        {
            Position = new System.Drawing.Point(random.Next(25, (int)area.Width - 25), -40);
            IsMoving = true;
            int temp = random.Next(4);
            switch (temp)
            {
                case 0:
                    name = EnemyEnum.one;
                    break;
                case 1:
                    name = EnemyEnum.two;
                    break;
                case 2:
                    name = EnemyEnum.three;
                    break;
                case 3:
                    name = EnemyEnum.four;
                    break;
                default:
                    break;
            }
        }

        public virtual void Move(System.Windows.Size area)
        {
            Point newposition;
            if (position.Y <= area.Height / 5)
            {
                newposition = new Point(position.X, position.Y + 5);
                position = newposition;
            }
            else
            {
                IsMoving = false;

                if (left)
                {
                    newposition = new Point(position.X - 1, position.Y);
                    if (newposition.X <= 25)
                    {
                        left = false;
                    }
                    if (newposition.X >= 0 && newposition.X <= area.Width && newposition.Y >= area.Height / 5 && newposition.Y <= area.Height / 3)
                    {
                        position = newposition;
                    }
                }
                else
                {
                    newposition = new Point(position.X + 1, position.Y);
                    if (newposition.X >= (int)area.Width - 25)
                    {
                        left = true;
                    }
                    if (newposition.X >= 0 && newposition.X <= area.Width && newposition.Y >= area.Height / 5 && newposition.Y <= area.Height / 3)
                    {
                        position = newposition;
                    }

                }
            }
        }


    }
}
