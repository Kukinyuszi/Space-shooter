using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_shooter.Poverups
{
    public class RapidFirePowerup : Powerup
    {

        public override Type PowerupType { get { return Type.RapidFire; } }

        public RapidFirePowerup(System.Windows.Size area, int speed, System.Drawing.Point position) : base(area, speed, position)
        {
        }
    }
}
