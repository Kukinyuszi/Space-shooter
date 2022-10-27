using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_shooter.Poverups.Weapons
{
    public class TrippleShooter : WeaponPowerup
    {
        public override WeaponType TypeofWeapon { get { return WeaponType.Tripplehooter; } }
        public TrippleShooter(System.Windows.Size area, int speed, System.Drawing.Point position) : base(area, speed, position)
        {
        }

    }
}
