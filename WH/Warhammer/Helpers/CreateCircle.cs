using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Warhammer.Helpers
{
    public static class CreateCircle
    {
        public static Ellipse Create(Point posi, Color col, int height=20, int width=20)
        {
            Ellipse eli = new Ellipse();
            var brush = new SolidColorBrush();
            brush.Opacity = 0.5;
            brush.Color = col;
            eli.Fill = brush;
            eli.Height = height;
            eli.Width = width;
            Canvas.SetTop(eli, posi.Y);
            Canvas.SetLeft(eli,posi.X);
            return eli;
        }
    }
}
