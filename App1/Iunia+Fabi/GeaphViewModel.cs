using App1.Iunia_Fabi.Model;
using App1.Iunia_Fabi.Service;
using App1.Iunia_Fabi.View;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Iunia_Fabi
{
    internal class GraphViewModel
    {
        public GraphService graphService = new();
        GraphView graphView = new GraphView();

        public GraphViewModel() {

            graphService.InsertCityDB("City 1", 0, 0);
            graphService.InsertCityDB("City 2", 200, 200);
            graphService.InsertCityDB("City 3", 150, 0);
            graphService.InsertCityDB("City 4", 50, 250);
            graphService.InsertCityDB("City 5", 300, 340);

            graphService.InsertRoadDB(1, 2, 1);
            graphService.InsertRoadDB(2, 3, 1);
            graphService.InsertRoadDB(3, 4, 1);
            graphService.InsertRoadDB(2, 4, 1);
            graphService.InsertRoadDB(4, 5, 1);
        }

        private void DrawCircles(Canvas canvas, List<(int x, int y)> coordinates)
        {
            foreach (var (x, y) in coordinates)
            {
                Ellipse circle = new Ellipse
                {
                    Width = 50,
                    Height = 50,
                    Fill = new SolidColorBrush(Microsoft.UI.Colors.Red)
                };
                Canvas.SetLeft(circle, x - 25); // Center the circle based on its radius
                Canvas.SetTop(circle, y - 25);

                canvas.Children.Add(circle);
            }

            graphView.Content = canvas; // Set the canvas as the content of the window
        }
        private void DrawLine(Canvas canvas, Brush brush, List<(int x, int y)> points)
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                var start = points[i];
                var end = points[i + 1];

                Line line = new Line
                {
                    X1 = start.x,
                    Y1 = start.y,
                    X2 = end.x,
                    Y2 = end.y,
                    Stroke = brush,
                    StrokeThickness = 2
                };

                canvas.Children.Add(line);
            }
        }
        private void DrawLines(Canvas canvas, Brush brush, List<((int x, int y) start, (int x, int y) end)> lines)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                var start = lines[i].start;
                var end = lines[i].end;

                Line line = new Line
                {
                    X1 = start.x,
                    Y1 = start.y,
                    X2 = end.x,
                    Y2 = end.y,
                    Stroke = brush,
                    StrokeThickness = 2
                };

                canvas.Children.Add(line);
            }
        }

    }
}
