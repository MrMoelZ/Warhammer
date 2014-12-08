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
    public static class CreateRect
    {

        public static Rectangle Create(Point posi, Color col, int height = 20, int width = 20, double opacity=1.0)
        {
            Rectangle rect = new Rectangle();
            rect.Height = height;
            rect.Width = width;
            var brush = new SolidColorBrush();
            brush.Opacity = opacity;
            brush.Color = col;
            rect.Fill = brush;
            Canvas.SetTop(rect,posi.Y);
            Canvas.SetLeft(rect,posi.X);
            return rect;
        }
    }
}
