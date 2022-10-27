using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_shooter.Poverups
{
    public class WeaponPowerup : Powerup
    {
        public override Type PowerupType { get { return Type.Weapon; } }
        public virtual WeaponType TypeofWeapon { get { return WeaponType.None; } }
        public WeaponPowerup(System.Windows.Size area, int speed, System.Drawing.Point position) : base(area, speed, position)
        {
        }
        public enum WeaponType
        {
            Doubleshooter, Tripplehooter, Biggerammo, None
        }

    }
}
