using System.Drawing;

namespace Space_shooter.Logic
{
    internal interface IShip
    {
        public Point Position { get; set; }
        public bool IsDead { get; set; }
        public bool IsMoving { set; get; }
        public void Move(System.Windows.Size area);
    }
}