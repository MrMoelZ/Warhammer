using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Warhammer.Helpers;

namespace Warhammer.Models
{
    public abstract class _Skaven
    {
        #region FIELDS
        private double _orientation;
        private Canvas maincanvas;
        private TransformGroup transforms = new TransformGroup();
        private TranslateTransform translateTransform = new TranslateTransform(0, 0);
        private RotateTransform rotateTransform = new RotateTransform(0, 0, 0);
        #endregion

        #region PROPERTIES
        public int ID { get; set; }
        public Attributes Attr { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Polyline ModelShape { get; set; }
        public static int Counter = 0;
        public bool HasSightCone { get; set; }
        public Polyline SightCone { get; set; }
        public Point Center { get; set; }
        #endregion

        #region CONSTRUCTOR(S)
        public _Skaven(int i, Canvas maincanvas)
        {
            this.maincanvas = maincanvas;
            ++Counter;
            ID = Counter;
            ModelShape = this.Create(i);
            Name = "Skaven";
            Random rnd = new Random(i);
            this.Orientation = rnd.Next(360);
        }
        #endregion

        #region METHODS

        public double Orientation
        {
            get
            {
                return _orientation;
            }

            set
            {
                if (!(value > 360 || value < 0))
                {
                    _orientation = value;
                    if (value != 0)
                    {
                        //RotateModel(this.ModelShape);
                    }
                }
            }
        }

        public void ToggleSightline()
        {
            //SightLine
            if (SightCone == null)
            {
                SightCone = new Polyline();
                var posi = new Point(0, 0);
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
            }
            SightCone.Visibility = (SightCone.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible);
            SightCone.RenderTransform = transforms;
        }

        public void MoveTo(double x, double y)
        {
            translateTransform.X = x;
            translateTransform.Y = y;
            rotateTransform.CenterX = translateTransform.X + 10;
            rotateTransform.CenterY = translateTransform.Y + 10;
            this.ModelShape.RenderTransform = transforms;
        }

        public void RotateModel(Polyline model, Point p)
        {
            var x = translateTransform.X + 10;
            var y = translateTransform.Y + 10;

            float xDiff = (float)(p.X - x);
            float yDiff = (float)(p.Y - y);
            yDiff *= -1;    // y-Koordinate negieren, da die y-Achse nach unten zeigt  
            

            //var angle = Math.Atan(yDiff / xDiff) * 180.0 / Math.PI;
            //if (xDiff < 0) angle += 180.0;
            ////if (yDiff < 0) angle += 90.0;

            var zaehler = yDiff;
            var nenner = Math.Sqrt((xDiff * xDiff) + (yDiff * yDiff));
            var rad = xDiff < 0 ? 2 * Math.PI - Math.Acos(zaehler / nenner) : Math.Acos(zaehler / nenner);
            var angle = rad * 180 / Math.PI;

            //MessageBox.Show(angle.ToString());
            rotateTransform.Angle = angle;

            rotateTransform.CenterX = x;
            rotateTransform.CenterY = y;
            this.ModelShape.RenderTransform = transforms;
        }
        
        public Polyline Create(int i)
        {
            var ret = CreateModel.Create(Colors.Black, 20.0, 20.0);
            ret.Tag = this;
            transforms.Children.Add(translateTransform);
            transforms.Children.Add(rotateTransform);
            ret.RenderTransform = transforms;
            return ret;
        }
        #endregion
    }
}
