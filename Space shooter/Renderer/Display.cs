using Space_shooter.Logic;
using Space_shooter.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static Space_shooter.Poverups.WeaponPowerup;

namespace Space_shooter.Renderer
{
    internal class Display : FrameworkElement
    {
        Size area;
        IGameModel model;
        int backgroundcounter = 0;
        private List<Explosion> Explodings = new List<Explosion>();

        public void SetupSizes(Size area)
        {
            this.area = area;
            this.InvalidateVisual();
        }

        public void SetupModel(IGameModel model)
        {
            this.model = model;
            this.model.Changed += (sender, eventargs) => this.InvalidateVisual();
        }

        public Brush SpaceBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "space.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exaust1Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship5/Exhaust/Normal_flight/Exhaust1/exhaust1.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exaust2Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship5/Exhaust/Normal_flight/Exhaust1/exhaust2.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exaust3Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship5/Exhaust/Normal_flight/Exhaust1/exhaust3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exaust4Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship5/Exhaust/Normal_flight/Exhaust1/exhaust04.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush TurboExaust1Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship5/Exhaust/Turbo_flight/Exhaust1/exhaust1.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush TurboExaust2Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship5/Exhaust/Turbo_flight/Exhaust1/exhaust2.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush TurboExaust3Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship5/Exhaust/Turbo_flight/Exhaust1/exhaust3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush TurboExaust4Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship5/Exhaust/Turbo_flight/Exhaust1/exhaust4.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Ship6Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship6/Ship6.png"), UriKind.RelativeOrAbsolute)));
            }
        }

        public Brush ShipBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship5/Ship5.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush enemy1Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship1/Ship1.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush enemy2Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship2/Ship2.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush enemy3Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship3/Ship3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush enemy4Brush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Ship4/Ship4.png"), UriKind.RelativeOrAbsolute)));
            }
        }

        public Brush AsteroidBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "asteroid.png"), UriKind.RelativeOrAbsolute)));
            }
        }

        public Brush DoubleShooterBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "double.jpg"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush TrippleShooterBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "tripple.jpg"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush BFGBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "bfg.jpg"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush HealthBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "health.jpg"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush CoinBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "coin.jpg"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush FastBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "fast.jpg"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush ShieldBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "shield.jpg"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush RapidBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_5.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush StrongBrush
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "strong.jpg"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot1Brush1
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot1/shot1_1.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot1Brush2
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot1/shot1_2.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot1Brush3
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot1/shot1_3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot1Brush4
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot1/shot1_4.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot1Brush5
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot1/shot1_asset.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exploding1
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Explosions/Explosion2/Explosion2_1.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exploding2
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Explosions/Explosion2/Explosion2_2.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exploding3
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Explosions/Explosion2/Explosion2_3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exploding4
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Explosions/Explosion2/Explosion2_4.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exploding5
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Explosions/Explosion2/Explosion2_5.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exploding6
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Explosions/Explosion2/Explosion2_6.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exploding7
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Explosions/Explosion2/Explosion2_7.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exploding8
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Explosions/Explosion2/Explosion2_8.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exploding9
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Explosions/Explosion2/Explosion2_9.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exploding10
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Explosions/Explosion2/Explosion2_10.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Exploding11
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Explosions/Explosion2/Explosion2_11.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot1Brushexp1
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot1/shot1_exp0.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot1Brushexp2
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot1/shot1_exp1.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot1Brushexp3
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot1/shot1_exp2.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot1Brushexp4
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot1/shot1_exp3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot1Brushexp5
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot1/shot1_exp4.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brushexp1
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_exp1.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brushexp2
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_exp2.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brushexp3
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_exp3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brushexp4
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_exp4.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brushexp5
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_exp5.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brushexp6
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_exp6.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brushexp7
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_exp7.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brushexp8
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_exp8.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brush1
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_1.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brush2
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_2.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brush3
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brush4
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_4.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot2Brush5
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot4/shot4_asset.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot3Brush1
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_1.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot3Brush2
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_2.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot3Brush3
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_3.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot3Brush4
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_4.png"), UriKind.RelativeOrAbsolute)));
            }
        }
        public Brush Shot3Brush5
        {
            get
            {
                return new ImageBrush(new BitmapImage(new Uri(Path.Combine("Images", "PNG_Parts&Spriter_Animation/Shots/Shot6/shot6_asset.png"), UriKind.RelativeOrAbsolute)));
            }
        }


        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (area.Width > 0 && area.Height > 0 && model != null)
            {
                Rect background0 = new Rect(0, backgroundcounter, area.Width, area.Height);
                Rect background1 = new Rect(0, background0.Y - area.Height, area.Width, area.Height);

                drawingContext.DrawRectangle(SpaceBrush, null, background0);
                drawingContext.DrawRectangle(SpaceBrush, null, background1);

                backgroundcounter++;
                if (backgroundcounter > area.Height) backgroundcounter = 0;

                foreach (var item in model.Asteroids)
                {
                    if (item.IsHit)
                    {
                        Explodings.Add(new Explosion(item.Position, 11, true, false));
                    }
                    else drawingContext.DrawEllipse(AsteroidBrush, null, new Point(item.Position.X, item.Position.Y), 25, 25);

                }
                foreach (var item in model.Powerups)
                {
                    switch (item.PowerupType)
                    {
                        case Poverups.Powerup.Type.ExtraScore:
                            drawingContext.DrawEllipse(CoinBrush, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                            break;
                        case Poverups.Powerup.Type.MoreHealth:
                            drawingContext.DrawEllipse(HealthBrush, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                            break;
                        case Poverups.Powerup.Type.RapidFire:
                            drawingContext.DrawEllipse(FastBrush, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                            break;
                        case Poverups.Powerup.Type.Shield:
                            drawingContext.DrawEllipse(ShieldBrush, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                            break;
                        case Poverups.Powerup.Type.Stronger:
                            drawingContext.DrawEllipse(StrongBrush, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                            break;
                        case Poverups.Powerup.Type.Weapon:
                            switch ((item as Poverups.WeaponPowerup).TypeofWeapon)
                            {
                                case Poverups.WeaponPowerup.WeaponType.Doubleshooter:
                                    drawingContext.DrawEllipse(DoubleShooterBrush, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                                    break;
                                case Poverups.WeaponPowerup.WeaponType.Tripplehooter:
                                    drawingContext.DrawEllipse(TrippleShooterBrush, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                                    break;
                                case Poverups.WeaponPowerup.WeaponType.Biggerammo:
                                    drawingContext.DrawEllipse(BFGBrush, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                                    break;
                                case Poverups.WeaponPowerup.WeaponType.None:
                                    drawingContext.DrawEllipse(Brushes.AntiqueWhite, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                                    break;
                            }

                            break;
                        case Poverups.Powerup.Type.None:
                            drawingContext.DrawEllipse(Brushes.AntiqueWhite, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                            break;
                        default:
                            break;
                    }
                }
                foreach (var item in model.Lasers)
                {
                    if (item.IsHit)
                    {
                        if(item.Fromplayer)Explodings.Add(new Explosion(item.Position, 8, item.Fromplayer, true));
                        else Explodings.Add(new Explosion(item.Position, 5, item.Fromplayer,true));
                    }
                    else
                    {
                        drawingContext.PushTransform(new RotateTransform(item.Angle, item.Position.X, item.Position.Y));
                        if (item.Fromplayer)
                        {

                            switch (item.Counter)
                            {
                                case < 2:
                                    if (item.Big) drawingContext.DrawEllipse(Shot2Brush1, null, new Point(item.Position.X + 40, item.Position.Y), item.Ammosize + 70, item.Ammosize + 70);
                                    else drawingContext.DrawEllipse(Shot2Brush1, null, new Point(item.Position.X + 45, item.Position.Y), item.Ammosize + 50, item.Ammosize + 50);
                                    break;
                                case < 4:
                                    if (item.Big) drawingContext.DrawEllipse(Shot2Brush2, null, new Point(item.Position.X + 30, item.Position.Y), item.Ammosize + 50, item.Ammosize + 70);
                                    else drawingContext.DrawEllipse(Shot2Brush2, null, new Point(item.Position.X + 35, item.Position.Y), item.Ammosize + 50, item.Ammosize + 50);
                                    break;
                                case < 6:
                                    if (item.Big) drawingContext.DrawEllipse(Shot2Brush3, null, new Point(item.Position.X + 20, item.Position.Y), item.Ammosize + 70, item.Ammosize + 70);
                                    else drawingContext.DrawEllipse(Shot2Brush3, null, new Point(item.Position.X + 25, item.Position.Y), item.Ammosize + 50, item.Ammosize + 50);
                                    break;
                                case < 8:
                                    if (item.Big)
                                    {
                                        drawingContext.DrawEllipse(Shot2Brush4, null, new Point(item.Position.X + 10, item.Position.Y), item.Ammosize + 70, item.Ammosize + 70);
                                        drawingContext.DrawEllipse(Shot2Brush5, null, new Point(item.Position.X + 10, item.Position.Y), item.Ammosize + 70, item.Ammosize + 70);
                                    }
                                    else
                                    {
                                        drawingContext.DrawEllipse(Shot2Brush4, null, new Point(item.Position.X + 10, item.Position.Y), item.Ammosize + 50, item.Ammosize + 50);
                                        drawingContext.DrawEllipse(Shot2Brush5, null, new Point(item.Position.X + 5 , item.Position.Y), item.Ammosize + 50, item.Ammosize + 50);
                                    }
                                    break;
                                case > 8:
                                    if (item.Big) drawingContext.DrawEllipse(Shot2Brush5, null, new Point(item.Position.X, item.Position.Y), item.Ammosize + 70, item.Ammosize + 70);
                                    else drawingContext.DrawEllipse(Shot2Brush5, null, new Point(item.Position.X, item.Position.Y), item.Ammosize + 50, item.Ammosize + 50);
                                    break;
                                default:
                                    break;
                            }
                            drawingContext.Pop();

                        }

                        else
                        {
                            switch (item.Counter)
                            {
                                case < 2:
                                    drawingContext.DrawEllipse(Shot1Brush1, null, new Point(item.Position.X + 35, item.Position.Y), item.Ammosize + 30, item.Ammosize + 30);
                                    break;
                                case < 4:
                                    drawingContext.DrawEllipse(Shot1Brush2, null, new Point(item.Position.X + 25, item.Position.Y), item.Ammosize + 30, item.Ammosize + 30);
                                    break;
                                case < 6:
                                    drawingContext.DrawEllipse(Shot1Brush3, null, new Point(item.Position.X + 15, item.Position.Y), item.Ammosize + 30, item.Ammosize + 30);
                                    break;
                                case < 8:
                                    drawingContext.DrawEllipse(Shot1Brush4, null, new Point(item.Position.X + 5, item.Position.Y), item.Ammosize + 30, item.Ammosize + 30);
                                    drawingContext.DrawEllipse(Shot1Brush5, null, new Point(item.Position.X, item.Position.Y), item.Ammosize + 30, item.Ammosize + 30);
                                    break;
                                case > 8:
                                    drawingContext.DrawEllipse(Shot1Brush5, null, new Point(item.Position.X, item.Position.Y), item.Ammosize + 30, item.Ammosize + 30);
                                    break;
                                default:
                                    break;
                            }
                            drawingContext.Pop();
                        }
                    }

                }
                
                
                //drawingContext.DrawText(FormatText($"Score:{model.Score}"), new Point(5, 5));
                //drawingContext.DrawText(FormatText($"Health:{model.Health}"), new Point(150, 5));
                //drawingContext.DrawText(FormatText($"HighScore:{model.HighScore}"), new Point(5, 30));
                foreach (var item in model.EnemyShips)
                {
                    if (item.IsHit)
                    {
                        Explodings.Add(new Explosion(item.Position, 11, true, false));
                    }
                    else
                    {
                        if(item.IsMoving)
                        {
                            switch (backgroundcounter % 8)
                            {
                                case < 2:
                                    drawingContext.DrawEllipse(Exaust1Brush, null, new Point(item.Position.X, item.Position.Y -50 ), 20, 20);
                                    break;
                                case < 4:
                                    drawingContext.DrawEllipse(Exaust2Brush, null, new Point(item.Position.X, item.Position.Y -50), 20, 20);
                                    break;
                                case < 6:
                                    drawingContext.DrawEllipse(Exaust3Brush, null, new Point(item.Position.X, item.Position.Y -50), 20, 20);
                                    break;
                                case < 8:
                                    drawingContext.DrawEllipse(Exaust4Brush, null, new Point(item.Position.X, item.Position.Y - 50), 20, 20);
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            switch (backgroundcounter % 8)
                            {
                                case <2:
                                    drawingContext.DrawEllipse(TurboExaust1Brush, null, new Point(item.Position.X, item.Position.Y - 50), 20, 20);
                                    break;
                                case <4:
                                    drawingContext.DrawEllipse(TurboExaust2Brush, null, new Point(item.Position.X, item.Position.Y - 50), 20, 20);
                                    break;
                                case <6:
                                    drawingContext.DrawEllipse(TurboExaust3Brush, null, new Point(item.Position.X, item.Position.Y - 50), 20, 20);
                                    break;
                                case <8:
                                    drawingContext.DrawEllipse(TurboExaust4Brush, null, new Point(item.Position.X, item.Position.Y - 50), 20, 20);
                                    break;
                                default:
                                    break;
                            }
                        }
                        switch (item.Name)
                        {
                            case EnemyShip.EnemyEnum.one:
                                drawingContext.DrawEllipse(enemy1Brush, null, new Point(item.Position.X, item.Position.Y), 40, 40);
                                break;
                            case EnemyShip.EnemyEnum.two:
                                drawingContext.DrawEllipse(enemy4Brush, null, new Point(item.Position.X, item.Position.Y), 40, 40);
                                break;
                            case EnemyShip.EnemyEnum.three:
                                drawingContext.DrawEllipse(enemy2Brush, null, new Point(item.Position.X, item.Position.Y), 40, 40);
                                break;
                            case EnemyShip.EnemyEnum.four:
                                drawingContext.DrawEllipse(enemy3Brush, null, new Point(item.Position.X, item.Position.Y), 40, 40);
                                break;
                            default:
                                break;
                        }
                    }

                }

                if (model.Player != null) drawingContext.DrawEllipse(ShipBrush, null, new Point(model.Player.Position.X, model.Player.Position.Y), 40, 40);

                if (model.Boss != null) drawingContext.DrawEllipse(Ship6Brush, null, new Point(model.Boss.Position.X, model.Boss.Position.Y), 100, 100);

                for (int i = 0; i < Explodings.Count; i++)
                {
                    var item = Explodings[i];
                    if(item.IsLaser)
                    {
                        if (item.FromPlayer)
                        {
                            switch (item.Counter)
                            {
                                case 1:
                                    drawingContext.DrawEllipse(Shot2Brushexp8, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                    break;
                                case 2:
                                    drawingContext.DrawEllipse(Shot2Brushexp7, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                    break;
                                case 3:
                                    drawingContext.DrawEllipse(Shot2Brushexp6, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                    break;
                                case 4:
                                    drawingContext.DrawEllipse(Shot2Brushexp5, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                    break;
                                case 5:
                                    drawingContext.DrawEllipse(Shot2Brushexp4, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                    break;
                                case 6:
                                    drawingContext.DrawEllipse(Shot2Brushexp3, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                    break;
                                case 7:
                                    drawingContext.DrawEllipse(Shot2Brushexp2, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                    break;
                                default:
                                    drawingContext.DrawEllipse(Shot2Brushexp1, null, new Point(item.Position.X, item.Position.Y), 30, 30);
                                    break;
                            }
                        }
                        else
                        {
                            switch (item.Counter)
                            {
                                case 1:
                                    drawingContext.DrawEllipse(Shot1Brushexp5, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                                    break;
                                case 2:
                                    drawingContext.DrawEllipse(Shot1Brushexp4, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                                    break;
                                case 3:
                                    drawingContext.DrawEllipse(Shot1Brushexp3, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                                    break;
                                case 4:
                                    drawingContext.DrawEllipse(Shot1Brushexp2, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                                    break;
                                default:
                                    drawingContext.DrawEllipse(Shot1Brushexp1, null, new Point(item.Position.X, item.Position.Y), 25, 25);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        switch (item.Counter)
                        {
                            case 1:
                                drawingContext.DrawEllipse(Exploding11, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                break;
                            case 2:
                                drawingContext.DrawEllipse(Exploding10, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                break;
                            case 3:
                                drawingContext.DrawEllipse(Exploding9, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                break;
                            case 4:
                                drawingContext.DrawEllipse(Exploding8, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                break;
                            case 5:
                                drawingContext.DrawEllipse(Exploding7, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                break;
                            case 6:
                                drawingContext.DrawEllipse(Exploding6, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                break;
                            case 7:
                                drawingContext.DrawEllipse(Exploding5, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                break;
                            case 8:
                                drawingContext.DrawEllipse(Exploding4, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                break;
                            case 9:
                                drawingContext.DrawEllipse(Exploding3, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                break;
                            case 10:
                                drawingContext.DrawEllipse(Exploding2, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                break;
                            default:
                                drawingContext.DrawEllipse(Exploding1, null, new Point(item.Position.X, item.Position.Y), 50, 50);
                                break;
                        }
                    }

                    item.Counter--;
                    if (item.Counter == 0) Explodings.RemoveAt(i);
                }
                

            }

        }
        private FormattedText FormatText(string text)
        {
            return new FormattedText(text, CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Verdana"), 20, Brushes.White);
        }

    }
}
