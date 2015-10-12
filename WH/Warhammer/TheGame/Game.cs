using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Warhammer.Helpers;
using Warhammer.Models;


namespace Warhammer.TheGame
{
    public class Game
    {
        bool _rotating = false;
        TextBlock infobarp1;
        TextBlock infobarp2;
        TextBlock infobarp3;
        TextBlock StatusBar;
        Canvas botcanvas;
        Canvas maincanvas;
        Polyline Unit = new Polyline();
        Polyline SelectedModel;
        MouseDevice MouseDev = InputManager.Current.PrimaryMouseDevice;
        int HOWMANYRATS = 10;
        bool IsPlacement = false;
        Clan_Rat UnitToPlace;
        Rectangle movementreach;
        List<Polyline> SightCones = new List<Polyline>();
        GamePhases Phase;
        int Round = 0;


        public Game(Canvas botcanvas, Canvas maincanvas, TextBlock statusbar, TextBlock infobarp1, TextBlock infobarp2, TextBlock infobarp3)
        {
            this.StatusBar = statusbar;
            this.botcanvas= botcanvas;
            this.maincanvas= maincanvas;
            this.infobarp1 = infobarp1;
            this.infobarp2 = infobarp2;
            this.infobarp3 = infobarp3;
        }

        public void StartUp()
        {
            //hook up mouseevents canvas
            maincanvas.MouseLeftButtonUp += maincanvas_MouseLeftButtonUp;
            maincanvas.MouseRightButtonDown += maincanvas_MouseRightButtonDown;
            maincanvas.MouseRightButtonUp += maincanvas_MouseRightButtonUp;
            maincanvas.MouseMove += maincanvas_MouseMove;

            // create new Rat

            List<Polyline> rectList = new List<Polyline>();
            List<Clan_Rat> ArmyList = new List<Clan_Rat>();
            for (int i = 0; i <= HOWMANYRATS; i++)
            {
                var mdummy = new Clan_Rat(i, maincanvas);
                ArmyList.Add(mdummy);
                Polyline rdummy = mdummy.ModelShape;
                //hookup mouse events
                rdummy.MouseLeftButtonDown += Rectangle_MouseLeftButtonDown;
                rdummy.MouseRightButtonDown += Rectangle_MouseRightButtonDown;
                rectList.Add(rdummy);
            }

            for (int i = 0; i < rectList.Count; i++)
            {
                botcanvas.Children.Add(rectList[i]);
                var posi = new Point(25 * i + 5, 5);
                TranslateTransform tt = new TranslateTransform(posi.X - 10, posi.Y - 10);
                rectList[i].RenderTransform = tt;
            }
            Round = 1;
            GamePhase(GamePhases.PlacementPhase);
        }

        private void Rectangle_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _rotating = true;

        }

        private void maincanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_rotating)
            {
                var posi = e.GetPosition(maincanvas);
                rotate(posi);
            }
        }

        public void NextPhase()
        {
            if (Phase == GamePhases.EndOfTurn)
            {
                ++Round;
                GamePhase(Phase=GamePhases.MovementPhase_StartOfTurn);
            }
            else
            {
                GamePhase(++Phase);
            }
        }

        public enum GamePhases
        {
            PlacementPhase = 0,
            MovementPhase_StartOfTurn = 1,
            MovementPhase_Charge ,
            MovementPhase_CompulsoryMoves,
            MovementPhase_RemainingMoves,
            MagicPhase_WindsOfMagic,
            MagicPhase_Cast,
            MagicPhase_Dispel,
            MagicPhase_SpellResolution,
            MagicPhase_NextSpell,
            ShootingPhase_NominateUnit,
            ShootingPhase_ChooseTarget,
            ShootingPhase_RollToHit,
            ShootingPhase_RollToWound,
            ShootingPhase_TakeSavingThrow,
            ShootingPhase_RemoveCasualties,
            ShootingPhase_NextShooting,
            CombatPhase_FightRound,
            CombatPhase_CalculateResult,
            CombatPhase_BreakTest,
            CombatPhase_FleeAndPursue,
            EndOfTurn
        }

        public void GamePhase(GamePhases phase)
        {
            infobarp1.Text = ""+phase.ToString();
            infobarp2.Text = "Turn "+Round.ToString();
            infobarp3.Text = "";
            
        }

        
        public void maincanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            _rotating = true;
        }

        //turn dummy
        public void rotate(Point posi)
        {
            var thismodel = SelectedModel;
            var thisobj = thismodel.Tag as Clan_Rat;
            thisobj.RotateModel(thismodel, posi);
        }

        public void maincanvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            _rotating = false;
        }


        public void maincanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var posi = e.GetPosition(maincanvas);

            switch (Phase)
            {
                case GamePhases.PlacementPhase:
                    if (IsPlacement)
                    {
                        var model = UnitToPlace.ModelShape;
                        Clan_Rat thisobj = model.Tag as Clan_Rat;
                        thisobj.MoveTo(posi.X - 10, posi.Y - 10);
                        //model.RenderTransform = tt;
                        botcanvas.Children.Remove(model);
                        maincanvas.Children.Add(model);
                        //model.Stroke = new SolidColorBrush(Colors.Black);
                        IsPlacement = false;
                    }
                    break;
                case GamePhases.MovementPhase_StartOfTurn:
                    break;
                case GamePhases.MovementPhase_Charge:
                    break;
                case GamePhases.MovementPhase_CompulsoryMoves:
                    break;
                case GamePhases.MovementPhase_RemainingMoves:
                    {
                        if (SelectedModel != null)
                        {
                            var model=SelectedModel.Tag as Clan_Rat;
                            model.MoveTo(posi.X - 10, posi.Y - 10);
                        }
                    }
                    break;
                case GamePhases.MagicPhase_WindsOfMagic:
                    break;
                case GamePhases.MagicPhase_Cast:
                    break;
                case GamePhases.MagicPhase_Dispel:
                    break;
                case GamePhases.MagicPhase_SpellResolution:
                    break;
                case GamePhases.MagicPhase_NextSpell:
                    break;
                case GamePhases.ShootingPhase_NominateUnit:
                    break;
                case GamePhases.ShootingPhase_ChooseTarget:
                    break;
                case GamePhases.ShootingPhase_RollToHit:
                    break;
                case GamePhases.ShootingPhase_RollToWound:
                    break;
                case GamePhases.ShootingPhase_TakeSavingThrow:
                    break;
                case GamePhases.ShootingPhase_RemoveCasualties:
                    break;
                case GamePhases.ShootingPhase_NextShooting:
                    break;
                //case GamePhases.CombatPhase_FightRound:
                //    // take every enemy unit and check if posi is in Zone of Influence (ZOI)
                //    foreach (var unit in EnemyUnits)
                //    {
                //        if(unit.IsInZOI(posi))
                //        {
                //            // 1.) calculate Distance (is it possible to reach enemy)
                //            // 2.) roll to check if enemy is actually reached
                //            // 3.) set marker for combat phase
                //        }
                //    }
                //    break;
                case GamePhases.CombatPhase_CalculateResult:
                    break;
                case GamePhases.CombatPhase_BreakTest:
                    break;
                case GamePhases.CombatPhase_FleeAndPursue:
                    break;
                case GamePhases.EndOfTurn:
                    break;
                default:
                    break;
            }            
        }

        public void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Polyline thismodel = sender as Polyline;
            Clan_Rat thisobj = thismodel.Tag as Clan_Rat;
            //deselect model
            if (SelectedModel != null)
            {
                SelectedModel.Stroke = new SolidColorBrush(Colors.Black);
            }
            SelectedModel = thismodel;
            //Parent botcanvas => placement mode
            if (thismodel.Parent == botcanvas)
            {
                IsPlacement = true;
                UnitToPlace = thisobj;
            }
            //parent maincanvas
            else if (thismodel.Parent == maincanvas)
            {
                //ShowMovementReach(thisobj.ModelShape);
                if (Keyboard.GetKeyStates(Key.LeftAlt) == KeyStates.Down || Keyboard.GetKeyStates(Key.LeftCtrl) == KeyStates.Down)
                {
                    ShowSightCone(thisobj);
                }
            }
            thisobj.ModelShape.Stroke = new SolidColorBrush(Colors.Red);
            double X = Math.Round(thisobj.ModelShape.TranslatePoint(new Point(0, 0), maincanvas).X, 0);
            double Y = Math.Round(thisobj.ModelShape.TranslatePoint(new Point(0, 0), maincanvas).Y, 0);
            StatusBar.Text = String.Format(thisobj.Name + '\n' + thisobj.Description + "\nID: " + thisobj.ID + "\nOrientation: " + thisobj.Orientation.ToString()
                + "\nPosi: " + X + ", " + Y);
        }

        private void ShowMovementReach(Polyline r)
        {
            if (movementreach != null)
            {
                maincanvas.Children.Remove(movementreach);
            }
            var posi = r.TranslatePoint(new Point(0, 0), maincanvas);
            //change posi to match orientation
            movementreach = CreateShape.Create <Rectangle>(posi, Colors.Red, 200, 20, 0.5);
            maincanvas.Children.Add(movementreach);
        }

        private void ShowSightCone(_Skaven obj)
        {
            obj.ToggleSightline();
        }
    }
}
