using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_shooter.Poverups
{
    public class ShieldPowerup : Powerup
    {
        public override Type PowerupType { get { return Type.Shield; } }

        public ShieldPowerup(System.Windows.Size area, int speed, System.Drawing.Point position) : base(area, speed, position)
        {
        }
    }
}
