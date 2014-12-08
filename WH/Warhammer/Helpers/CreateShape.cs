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
    public static class CreateShape
    {
        
        public static T Create<T>(Point posi, Color col, int height = 20, int width = 20, double opacity = 1.0) where T : Shape, new()
        {
            var shape = new T();
            shape.Height = height;
            shape.Width = width;
            var brush = new SolidColorBrush();
            brush.Opacity = opacity;
            brush.Color = col;
            shape.Fill = brush;
            Canvas.SetTop(shape, posi.Y);
            Canvas.SetLeft(shape, posi.X);
            return shape;
        }
    }
}
