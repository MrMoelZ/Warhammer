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

        //turn dummy
        public void Rectangle_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Polyline thismodel = sender as Polyline;
            Clan_Rat thisobj = thismodel.Tag as Clan_Rat;
            thisobj.Orientation = 138;
            thisobj.RotateModel(thismodel);
        }


        public void maincanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var posi = e.GetPosition(maincanvas);
            TranslateTransform tt = new TranslateTransform(posi.X - 10, posi.Y - 10);

            switch (Phase)
            {
                case GamePhases.PlacementPhase:
                    if (IsPlacement)
                    {
                        var model = UnitToPlace.ModelShape;
                        model.RenderTransform = tt;
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
                            SelectedModel.RenderTransform = tt;
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
                case GamePhases.CombatPhase_FightRound:
                    break;
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
            var r = obj.ModelShape;
            if (obj.HasSightCone)
            {
                maincanvas.Children.Remove(obj.SightCone);
                obj.HasSightCone = false;
            }
            else
            {
                var SightCone = new Polyline();
                var posi = r.TranslatePoint(new Point(0, 0), maincanvas);
                var endposi = new Point(posi.X - 100, posi.Y - 100);
                SightCone.Points.Add(posi);
                SightCone.Points.Add(endposi);
                posi = new Point(posi.X + 20, posi.Y);
                endposi = new Point(posi.X + 100, posi.Y - 100);
                SightCone.Points.Add(endposi);
                SightCone.Points.Add(posi);
                var brush = new SolidColorBrush();
                brush.Color = Colors.Blue;
                brush.Opacity = 1.0;
                SightCone.Stroke = brush;
                brush.Opacity = 0.3;
                SightCone.Fill = brush;
                maincanvas.Children.Add(SightCone);
                SightCones.Add(SightCone);
                obj.SightCone = SightCone;
                obj.HasSightCone = true;
            }
        }
    }
}
