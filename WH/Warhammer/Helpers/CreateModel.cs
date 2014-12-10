using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Warhammer.Models;

namespace Warhammer.Helpers
{
    public static class CreateModel
    {
        

        public static Polyline Create(Color col, double height = 20, double width = 20, double opacity = 1.0)
        {
            Polyline model = new Polyline();
            Point Start = new Point(0, 0);
            double hsize = height;
            double wsize = width;

            var offset = wsize * (1.0 / 3.0);
            model.Points.Add(new Point(Start.X, Start.Y));
            model.Points.Add(new Point(Start.X + offset, Start.Y));
            offset = wsize * (1.0 / 2.0);
            model.Points.Add(new Point(Start.X + offset, Start.Y - 3));
            offset = wsize * (2.0 / 3.0);
            model.Points.Add(new Point(Start.X + offset, Start.Y));
            model.Points.Add(new Point(Start.X + wsize, Start.Y));
            model.Points.Add(new Point(Start.X + wsize, Start.Y + hsize));
            model.Points.Add(new Point(Start.X, Start.Y + hsize));
            var brush = new SolidColorBrush();
            brush.Opacity = opacity;
            brush.Color = col;
            model.Fill = brush;
            return model;
        }
    }
}
