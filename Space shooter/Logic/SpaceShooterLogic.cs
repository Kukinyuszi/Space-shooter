using Microsoft.Toolkit.Mvvm.Messaging;
using Space_shooter.Models;
using Space_shooter.Poverups;
using Space_shooter.Poverups.Weapons;
using Space_shooter.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Space_shooter.Logic.EnemyShip;
using static Space_shooter.Logic.ISettings;
using Point = System.Drawing.Point;

namespace Space_shooter.Logic
{
    public class SpaceShooterLogic : IGameModel, ISettings
    {
        public enum Controls
        {
            Left, Right, Shoot, Escape, G, O, D
        }
        public event EventHandler Changed, GameOver, PowerUpPickedUp;
        public int score = 0, health = 100, rpd = 0, strng = 0, wpn = 0;
        private int asteroidspeed = 5, firerate = 30, poweruprate = 40, enemyshottimechange = 60, bossshottimechange = 40,bosshealth, enemyshottimer = 0, bossshottimer = 0, playershottimer = 0, highscore, enemiescount;
        private bool godmode, sound;
        public bool shield, left, right, shoot, paused, g, o, d, rapid, strong, weapontime;
        private string playername = "Type here your name";
        System.Windows.Size area;


        public List<Laser> Lasers { get; set; }
        public List<Asteroid> Asteroids { get; set; }
        public List<EnemyShip> EnemyShips { get; set; }
        public List<Powerup> Powerups { get; set; }
        public List<Powerup> PlayerPowerups { get; set; }
        public Player Player { get; set; }
        public Boss Boss { get; set; }
        public int Health { get { return health; } }
        public int Score { get { return score; } }
        public int HighScore { get { return highscore; } }
        public string PlayerName { get { return playername; } set { playername = value; } }

        public int Asteroidspeed { get => asteroidspeed; set => asteroidspeed = value; }
        public int Firerate { get => firerate; set => firerate = value; }
        public int Poweruprate { get => poweruprate; set => poweruprate = value; }
        public int Enemyshottimechange { get => enemyshottimechange; set => enemyshottimechange = value; }
        public int Bossshottimechange { get => bossshottimechange; set => bossshottimechange = value; }
        public bool Godmode { get => godmode; set => godmode = value; }
        public bool Sound { get => sound; set => sound = value; }
        public Difficulty Difficultyness { get { return Difficulty.Medium; } set { Difficultyness = value; } }

      

        static Random random = new Random();


        public void SetupSizes(System.Windows.Size area)
        {
            this.area = area;
            highscore = new ScoreBoardService().GetHighScore();
            SetupDifficulty();
            Lasers = new List<Laser>();
            Asteroids = new List<Asteroid>();
            EnemyShips = new List<EnemyShip>();
            Powerups = new List<Powerup>();
            PlayerPowerups = new List<Powerup>();
            Player = new Player(new System.Drawing.Point((int)area.Width / 2, (int)area.Height - 50));
            Asteroids.Add(new Asteroid(area, Asteroidspeed));
            SetupEnemyes(area);

        }



        public SpaceShooterLogic()
        {
        }
        public void Controldown(Controls control)
        {
            switch (control)
            {
                case Controls.Left:
                    left = true;
                    if (g || o || d) g = o = d = false;
                    break;
                case Controls.Right:
                    right = true;
                    if (g || o || d) g = o = d = false;
                    break;
                case Controls.Shoot:
                    shoot = true;
                    if (g || o || d) g = o = d = false;
                    break;
                case Controls.Escape:
                    if(!paused) paused = true;
                    else paused = false;
                    break;
                case Controls.G:
                    g = true;
                    break;
                case Controls.O:
                    if (g) o = true;
                    else{ d = false; g = false; }
                    break;
                case Controls.D:
                    if (g && o) 
                    { 
                        d = true;
                        Godmode = !Godmode;
                        if (!Godmode) health = 100;
                    }
                    else g = o = false;
                    break;

                default:
                    break;
            }
            Changed?.Invoke(this, null);
        }
        public void Controlup(Controls control)
        {
            switch (control)
            {
                case Controls.Left:
                    left = false;
                    break;
                case Controls.Right:
                    right = false;
                    break;
                case Controls.Shoot:
                    shoot = false;
                    break;
                default:
                    break;
            }
            Changed?.Invoke(this, null);
        }

        public void TimeStep()
        {
            
            if (!paused)
            {
                System.Windows.Size size = new System.Windows.Size((int)area.Width, (int)area.Height);
                Rect playerrect = new Rect(Player.Position.X - 15, Player.Position.Y - 12, 30, 25);
                if (Boss == null && EnemyShips.Count > 0) EnemyShipsMovement(size);
                else
                {
                    Boss.Move(size);
                    IsBossDead(size);
                }

                if (DifficultyByScore() && Boss == null) SetupNewBoss(size);

                LasersMovement(size);
                AsteroidsMovement(size);
                HitCheck(size);

                for (int i = 0; i < Lasers.Count; i++)
                {
                    var laser = Lasers[i];
                    Rect laserrect = LaserFromWhom(laser);

                    AsteroidsCollison(size, playerrect, laser, i, laserrect);

                    if (Boss == null) EnemyShipsCollisions(size, laser, laserrect, i);

                    else if (Boss != null) BossCollisions(size, laserrect, laser, i);

                    if (Collide(size, laserrect, playerrect) && !laser.Fromplayer)
                    {
                        Lasers[i].IsHit = true;
                        if (shield) shield = false;
                        else  health -= 10;  
                    }
                }
                if (EnemyShips.Count > 0) EnemiesShoot();

                if (Boss != null && (bossshottimer == 0 || bossshottimer == Bossshottimechange / 2)) NewEnemyShoot(Boss as EnemyShip);

                PowerupPickup(size, playerrect);
                PlayerInteractions(size);
                CountersTimeEllapses();
                SeeIfGameEnds();

                Changed?.Invoke(this, null);

                if (godmode)
                {
                    health = 99999;
                }
            }
        }
        private void SetupDifficulty()
        {
            switch (Difficultyness)
            {
                case Difficulty.Easy:
                    enemiescount = 1;
                    asteroidspeed = 5;
                    firerate = 25;
                    poweruprate = 50;
                    enemyshottimechange = 70;
                    bossshottimechange = 60;
                    bosshealth = 300;
                    break;
                case Difficulty.Medium:
                    enemiescount = 2;
                    asteroidspeed = 5;
                    firerate = 30;
                    poweruprate = 40;
                    enemyshottimechange = 60;
                    bossshottimechange = 40;
                    bosshealth = 400;
                    break;
                case Difficulty.Hard:
                    enemiescount = 3;
                    asteroidspeed = 7;
                    firerate = 30;
                    poweruprate = 30;
                    enemyshottimechange = 50;
                    bossshottimechange = 40;
                    bosshealth = 500;
                    break;
                case Difficulty.Custom:
                    break;
                default:
                    break;
            }
        }

        private void HitCheck(System.Windows.Size size)
        {
            for (int i = 0; i < Asteroids.Count; i++)
            {
                if (Asteroids[i].IsHit) 
                {
                    Asteroids.RemoveAt(i);
                    Asteroids.Add(new Asteroid(size, Asteroidspeed));
                }
            }

            for (int i = 0; i < EnemyShips.Count; i++)
            {
                if (EnemyShips[i].IsHit)
                {
                    EnemyShips.RemoveAt(i);
                    EnemyShips.Add(new EnemyShip(size));
                }
            }

            for (int i = 0; i < Lasers.Count; i++)
            {
                if (Lasers[i].IsHit) { Lasers.RemoveAt(i); }
            }
        }

        private void SetupNewBoss(System.Windows.Size size)
        {
            Boss = new Boss(size, bosshealth);
            EnemyShips.Clear();
        }

        private void IsBossDead(System.Windows.Size size)
        {
            if (Boss.Health <= 0)
            {
                score += 50;
                Boss = null;
                SetupEnemyes(size);
            }
        }

        private Rect LaserFromWhom(Laser laser)
        {
            if (laser.Fromplayer)
            {
                if (laser.Big) return new Rect(laser.Position.X - (laser.Ammosize / 2), laser.Position.Y - (laser.Ammosize / 2), laser.Ammosize, laser.Ammosize);
                return new Rect(laser.Position.X - (laser.Ammosize / 2), laser.Position.Y - (laser.Ammosize / 2), laser.Ammosize, laser.Ammosize);
            }
            else return new Rect(laser.Position.X - (laser.Ammosize / 2), laser.Position.Y - (laser.Ammosize / 2), laser.Ammosize, laser.Ammosize);
        }

        private void BossCollisions(System.Windows.Size size, Rect laserrect, Laser laser, int i)
        {
            
            Rect bossrect = new Rect(Boss.Position.X - 50, Boss.Position.Y - 80, 100, 160);
            if (Collide(size, laserrect, bossrect) && laser.Fromplayer)
            {
                Lasers[i].IsHit = true;
                if (strong || Lasers[i].Big) Boss.Health -= 30;
                else Boss.Health -= 10;
            }
        }

        private void EnemyShipsCollisions(System.Windows.Size size, Laser laser, Rect laserrect, int i)
        {
            for (int j = 0; j < EnemyShips.Count; j++)
            {
                var enemy = EnemyShips[j];

                Rect enemyrect = new Rect(enemy.Position.X - 25, enemy.Position.Y - 20, 50, 40);
                if (Collide(size, laserrect, enemyrect) && laser.Fromplayer)
                {
                    score += 30;
                    EnemyShips[j].IsHit = true;
                    if (!strong) Lasers[i].IsHit = true;
                }
            }
        }

        private void AsteroidsCollison(System.Windows.Size size, Rect playerrect, Laser laser, int j, Rect laserrect)
        {
            for (int i = 0; i < Asteroids.Count; i++)
            {
                var asteroid = Asteroids[i];
                Rect asteroidrect = new Rect(asteroid.Position.X - 25, asteroid.Position.Y - 20, 50, 40);
                if (Collide(size, laserrect, asteroidrect) && laser.Fromplayer)
                {
                    PowerupDrop(size, asteroid);
                    score += 10;
                    Asteroids[i].IsHit = true;
                    if (!strong) Lasers[j].IsHit = true;
                }
                else if (Collide(size, playerrect, asteroidrect))
                {
                    Asteroids[i].IsHit = true;
                    if (shield) shield = false;
                    else  health -= 10; 
                }
            }
        }

        private void EnemiesShoot()
        {
            for (int i = 0; i < EnemyShips.Count; i++)
            {
                if (enemyshottimer == 0)
                {
                    NewEnemyShoot(EnemyShips[i]);
                }
            }
        }

        private void AsteroidsMovement(System.Windows.Size size)
        {
            for (int i = 0; i < Asteroids.Count; i++)
            {
                bool inside = Asteroids[i].Move(size);
                if (!inside)
                {
                    Asteroids.RemoveAt(i);
                    Asteroids.Add(new Asteroid(size, Asteroidspeed));
                }
            }
        }

        private void SetupEnemyes(System.Windows.Size area)
        {
            for (int i = 0; i < enemiescount; i++)
            {
                EnemyShips.Add(new EnemyShip(area));
            }
        }

        private void NewPlayerShoot()
        {
            Point playerpositiontemp = new System.Drawing.Point(Player.Position.X, Player.Position.Y - 25);
            switch (Player.Weapon)
            {
                case Poverups.WeaponPowerup.WeaponType.Doubleshooter:
                    Lasers.Add(new Laser(playerpositiontemp, new Vector(-1, -5), true, false));
                    Lasers.Add(new Laser(playerpositiontemp, new Vector(1, -5), true, false));
                    break;
                case Poverups.WeaponPowerup.WeaponType.Tripplehooter:
                    Lasers.Add(new Laser(new Point(playerpositiontemp.X - 20, playerpositiontemp.Y), new Vector(0, -5), true, false));
                    Lasers.Add(new Laser(playerpositiontemp, new Vector(0, -5), true, false));
                    Lasers.Add(new Laser(new Point(playerpositiontemp.X + 20, playerpositiontemp.Y), new Vector(0, -5), true, false));
                    break;
                case Poverups.WeaponPowerup.WeaponType.Biggerammo:
                    Lasers.Add(new Laser(playerpositiontemp, new Vector(0, -5), true, true));
                    break;
                case Poverups.WeaponPowerup.WeaponType.None:
                    Lasers.Add(new Laser(playerpositiontemp, new Vector(0, -5), true, false));
                    break;
                default:
                    break;
            }
        }

        private void NewEnemyShoot(EnemyShip enemyship)
        {
            Point enemyshippositiontemp = new System.Drawing.Point(enemyship.Position.X, enemyship.Position.Y + 23);
            Point bosspositiontemp = new System.Drawing.Point(enemyship.Position.X, enemyship.Position.Y + 60);
            double x = ((Player.Position.X) - enemyship.Position.X) / 40;
            double y = ((Player.Position.Y - 40) - enemyship.Position.Y + 23) / 40;

            int x1;
            if (enemyship.Position.X < area.Width / 2) x1 = random.Next(4);
            else x1 = random.Next(-4, 0);

            switch (enemyship.Name)
            {
                case EnemyShip.EnemyEnum.one:
                    Lasers.Add(new Laser(enemyshippositiontemp, new Vector(0, 5), false, false));
                    break;
                case EnemyShip.EnemyEnum.two:
                    Lasers.Add(new Laser(enemyshippositiontemp, new Vector(1, 5), false, false));
                    Lasers.Add(new Laser(enemyshippositiontemp, new Vector(-1, 5), false, false));
                    break;
                case EnemyShip.EnemyEnum.three:
                    Lasers.Add(new Laser(enemyshippositiontemp, new Vector(x, y), false, false));
                    break;
                case EnemyShip.EnemyEnum.four:
                    Lasers.Add(new Laser(enemyshippositiontemp, new Vector(x1, 10), false, false));
                    break;
                case EnemyShip.EnemyEnum.boss:
                    if(bossshottimer == Bossshottimechange / 2) Lasers.Add(new Laser(bosspositiontemp, new Vector(Math.Round(x) * 1.5, Math.Round(y) * 1.5), false, false));
                    else
                    {
                        Lasers.Add(new Laser(bosspositiontemp, new Vector((x * 2) - 2, y * 1.5), false, false));
                        Lasers.Add(new Laser(bosspositiontemp, new Vector(x * 2 + random.Next(-1, 2), y * 1.5), false, false));
                        Lasers.Add(new Laser(bosspositiontemp, new Vector((x * 2) + 2, y * 1.5), false, false));
                    }
                    break;
                default:
                    break;
            }
        }

        private void CountersTimeEllapses()
        {
            rapid = strong = weapontime = false;
            Player.Weapon = WeaponPowerup.WeaponType.None;

            for (int i = 0; i < PlayerPowerups.Count; i++)
            {
                var powerup = PlayerPowerups[i];
                switch (powerup.PowerupType)
                {
                    case Powerup.Type.RapidFire:
                        rapid = true;
                        rpd = powerup.Counter;
                        break;
                    case Powerup.Type.Stronger:
                        strong = true;
                        strng = powerup.Counter;
                        break;
                    case Powerup.Type.Weapon:
                        wpn = powerup.Counter;
                        switch ((powerup as WeaponPowerup).TypeofWeapon)
                        {
                            case WeaponPowerup.WeaponType.Doubleshooter:
                                Player.Weapon = WeaponPowerup.WeaponType.Doubleshooter;
                                break;
                            case WeaponPowerup.WeaponType.Tripplehooter:
                                Player.Weapon = WeaponPowerup.WeaponType.Tripplehooter;
                                break;
                            case WeaponPowerup.WeaponType.Biggerammo:
                                Player.Weapon = WeaponPowerup.WeaponType.Biggerammo;
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                powerup.Counter = powerup.Counter - 1;
                if (powerup.Counter <= 0) PlayerPowerups.RemoveAt(i);
            }
            if (enemyshottimer == 0) enemyshottimer = Enemyshottimechange;
            if (enemyshottimer > 0) enemyshottimer--;
            if (bossshottimer == 0) bossshottimer = Bossshottimechange;
            if (bossshottimer > 0) bossshottimer--;

            if (playershottimer > 0) playershottimer--;
        }

        private void SeeIfGameEnds()
        {
            if (health <= 0)
            {
                health = 0;
                new ScoreBoardService().SaveNewScore(Score,PlayerName);
                GameOver?.Invoke(this, null);
            }
        }

        private void PlayerInteractions(System.Windows.Size size)
        {
            if (left)
            {
                Player.left = true;
                Player.Move(size);
            }

            if (right)
            {
                Player.left = false;
                Player.Move(size);
            }

            if (shoot && playershottimer <= 0)
            {
                NewPlayerShoot();
                if (rapid)
                {
                    playershottimer = 10;
                }
                else
                {
                    playershottimer = Firerate;
                }
            }
        }

        private void PowerupPickup(System.Windows.Size size, Rect playerrect)
        {
            for (int i = 0; i < Powerups.Count; i++)
            {
                bool inside = Powerups[i].Move(size);
                if (!inside)
                {
                    Powerups.RemoveAt(i);
                }
                else
                {
                    Rect poweruprect = new Rect(Powerups[i].Position.X - 25, Powerups[i].Position.Y - 20, 50, 40);
                    if (poweruprect.IntersectsWith(playerrect))
                    {

                        object obj = Powerups[i] as object;
                        switch (Powerups[i].PowerupType)
                        {
                            case Powerup.Type.ExtraScore:
                                score += 30;
                                PowerUpPickedUp?.Invoke(obj, null);
                                break;
                            case Powerup.Type.MoreHealth:
                                health += 20;
                                PowerUpPickedUp?.Invoke(obj, null);
                                break;
                            case Powerup.Type.RapidFire:
                                PlayerPowerups.Add(Powerups[i]);
                                PowerUpPickedUp?.Invoke(obj, null);
                                break;
                            case Powerup.Type.Shield:
                                shield = true;
                                PowerUpPickedUp?.Invoke(obj, null);
                                break;
                            case Powerup.Type.Stronger:
                                PlayerPowerups.Add(Powerups[i]);
                                PowerUpPickedUp?.Invoke(obj, null);
                                break;
                            case Powerup.Type.Weapon:
                                PlayerPowerups.Add(Powerups[i]);
                                PowerUpPickedUp?.Invoke(obj, null);
                                break;
                            default:
                                break;
                        }
                        Powerups.RemoveAt(i);
                    }
                }
            }
        }

        private bool Collide(System.Windows.Size size, Rect rect1, Rect rect2)
        {
            if (rect1.IntersectsWith(rect2))
            {
                return true;
            }
            else return false;
        }

        private void PowerupDrop(System.Windows.Size size, Asteroid asteroid)
        {
            if (random.Next(100) < Poweruprate)
            {
                int temprand = random.Next(101);
                Point position = asteroid.Position;
                switch (temprand)
                {
                    case < 25:
                        Powerups.Add(new ExtraScorePowerup(size, Asteroidspeed, position));
                        break;
                    case < 50:
                        Powerups.Add(new MoreHealthPowerup(size, Asteroidspeed, position));
                        break;
                    case < 65:
                        Powerups.Add(new RapidFirePowerup(size, Asteroidspeed, position));
                        break;
                    case < 80:
                        Powerups.Add(new ShieldPowerup(size, Asteroidspeed, position));
                        break;
                    case < 90:
                        Powerups.Add(new StrongerPowerup(size, Asteroidspeed, position));
                        break;
                    default:
                        int tempweaponrand = random.Next(3);
                        switch (tempweaponrand)
                        {
                            case 0:
                                Powerups.Add(new BiggerAmmo(size, Asteroidspeed, position));
                                break;
                            case 1:
                                Powerups.Add(new Shotgun(size, Asteroidspeed, position));
                                break;
                            case 2:
                                Powerups.Add(new TrippleShooter(size, Asteroidspeed, position));
                                break;
                            default:
                                break;
                        }
                        break;
                }
            }
        }

        private void EnemyShipsMovement(System.Windows.Size size)
        {
            foreach (var item in EnemyShips)
            {
                item.Move(size);
            }
        }

        private bool DifficultyByScore()
        {

            if (score > 1000 && score % 1000 <= 50)
            {
                return true;
            }
            return false;
        }

        private void LasersMovement(System.Windows.Size size)
        {
            for (int i = 0; i < Lasers.Count; i++)
            {
                Lasers[i].Counter++;
                bool inside = Lasers[i].Move(size);
                if (!inside)
                {
                    Lasers.RemoveAt(i);
                }
            }
        }
    }
}
