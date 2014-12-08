using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Warhammer.Helpers
{
    public static class CreateLine
    {
        public static Line Create(Point start, Point end, Color col)
        {
            Line line = new Line();
            line.X1 = start.X;
            line.Y1 = start.Y;
            line.X2 = end.X;
            line.Y2 = end.Y;
            var brush = new SolidColorBrush();
            brush.Opacity = 1.0;
            brush.Color = col;
            line.Stroke = brush;
            
            return line;
        }
    }
}
