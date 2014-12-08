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
        private double _orientation;
        private Canvas maincanvas;

        public int ID { get; set; }
        public Attributes Attr { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Polyline ModelShape { get; set; }
        public static int Counter = 0;
        public bool HasSightCone { get; set; }
        public Polyline SightCone { get; set; }
        public Point Center { get; set; }

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

        public void RotateModel(Polyline model)
        {
            var posi = this.ModelShape.TranslatePoint(new Point(0, 0), maincanvas);
            var x = posi.X;
            var y = posi.Y;
            Line line=CreateLine.Create(new Point(x, y), new Point(x + 2.0, y + 2.0), Colors.LawnGreen);
            maincanvas.Children.Add(line);

            MessageBox.Show("Point: " + x + " " + y);
            RotateTransform rt = new RotateTransform(this.Orientation,x,x);
            
            this.ModelShape.RenderTransform = rt;
        }

        public _Skaven(int i, Canvas maincanvas)
        {
            this.maincanvas=maincanvas;
            ++Counter;
            ID = Counter;
            ModelShape = this.Create(i);
            Name = "Skaven";
            Random rnd = new Random(i);
            this.Orientation = rnd.Next(360);
        }


        public Polyline Create(int i)
        {
            var ret = CreateModel.Create(Colors.Black, 20.0, 20.0);
            ret.Tag = this;
            return ret;
        }
    }
}
