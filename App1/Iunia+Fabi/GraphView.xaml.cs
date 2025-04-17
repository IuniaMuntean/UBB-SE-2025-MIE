using App1.Iunia_Fabi;
using App1.Iunia_Fabi.Model;
using App1.Iunia_Fabi.Service;
using LinqToDB;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Graphics;

namespace App1.Iunia_Fabi.View
{
    public sealed partial class GraphView : Window
    {
        private GraphService graphService;

        public GraphView( int startCityID, int endCityID)
        {
            this.InitializeComponent();
            setSize();
            
            graphService = new GraphService();

            //graphService.InsertCityDB("City 1", 0, 0);
            //graphService.InsertCityDB("City 2", 200, 200);
            //graphService.InsertCityDB("City 3", 150, 0);
            //graphService.InsertCityDB("City 4", 50, 250);
            //graphService.InsertCityDB("City 5", 300, 340);

            //graphService.InsertRoadDB(1, 2, 1);
            //graphService.InsertRoadDB(2, 3, 1);
            //graphService.InsertRoadDB(3, 4, 1);
            //graphService.InsertRoadDB(2, 4, 1);
            //graphService.InsertRoadDB(4, 5, 1);



            graphService.InsertCityDB("Bucharest", 210, 451);
            graphService.InsertCityDB("Cluj-Napoca", 124, 149);
            graphService.InsertCityDB("Iasi", 297, 85);
            graphService.InsertCityDB("Constanta", 500, 484);
            graphService.InsertCityDB("Timisoara", 0, 264);
            graphService.InsertCityDB("Brasov", 161, 194);
            graphService.InsertCityDB("Craiova", 78, 435);
            graphService.InsertCityDB("Galati", 436, 277);
            graphService.InsertCityDB("Oradea", 66, 0);
            graphService.InsertCityDB("Ploiesti", 203, 384);

            graphService.InsertRoadDB(1, 2, (float)3.6);
            graphService.InsertRoadDB(1, 3, (float)3.2);
            graphService.InsertRoadDB(1, 4, (float)2.7);
            graphService.InsertRoadDB(1, 5, (float)4.6);
            graphService.InsertRoadDB(1, 6, (float)1.8);
            graphService.InsertRoadDB(1, 7, (float)1.2);
            graphService.InsertRoadDB(1, 8, (float)2.1);
            graphService.InsertRoadDB(1, 9, (float)4.7);
            graphService.InsertRoadDB(1, 10, (float)6.2);
            graphService.InsertRoadDB(2, 3, (float)3.1);
            graphService.InsertRoadDB(2, 4, (float)3.5);
            graphService.InsertRoadDB(2, 5, (float)1.8);
            graphService.InsertRoadDB(2, 6, (float)1.9);
            graphService.InsertRoadDB(2, 7, (float)2.3);
            graphService.InsertRoadDB(2, 8, (float)2.4);
            graphService.InsertRoadDB(2, 9, (float)1.1);
            graphService.InsertRoadDB(2, 10, (float)2.5);
            graphService.InsertRoadDB(3, 4, (float)3.4);
            graphService.InsertRoadDB(3, 5, (float)2.2);
            graphService.InsertRoadDB(3, 6, (float)2.6);
            graphService.InsertRoadDB(3, 7, (float)3.7);
            graphService.InsertRoadDB(3, 8, (float)9.3);
            graphService.InsertRoadDB(3, 9, (float)3.7);
            graphService.InsertRoadDB(3, 10, (float)3.1);
            graphService.InsertRoadDB(4, 5, (float)4.8);
            graphService.InsertRoadDB(4, 6, (float)2.9);
            graphService.InsertRoadDB(4, 7, (float)3.9);
            graphService.InsertRoadDB(4, 8, (float)9.5);
            graphService.InsertRoadDB(4, 9, (float)3.9);
            graphService.InsertRoadDB(4, 10, (float)3.3);


            Canvas canvas = new Canvas
            {
                Width = 600,
                Height = 600,
                Background = new SolidColorBrush(Microsoft.UI.Colors.LightGray)
            };

            DrawCircles(canvas, graphService.Graph.Cities().Select(city => (city.x, city.y, city.name)).ToList());

            var path = Fabi__Path_Finding.Path(graphService.Graph, startCityID, endCityID);
            DrawLines(canvas, new SolidColorBrush(Microsoft.UI.Colors.Black), graphService.Graph.Roads().Select(road => ((graphService.Graph.City(road.start).x, graphService.Graph.City(road.start).y), (graphService.Graph.City(road.end).x, graphService.Graph.City(road.end).y))).ToList());
            DrawLine(canvas, new SolidColorBrush(Microsoft.UI.Colors.Yellow), path.Select(id => (graphService.Graph.City(id).x, graphService.Graph.City(id).y)).ToList());
        }

        private void setSize()
        {
            IntPtr hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Win32Interop.GetWindowIdFromWindow(hwnd);
            AppWindow appWindow = AppWindow.GetFromWindowId(windowId);
            appWindow.Resize(new SizeInt32(1000, 900));
        }


        private void DrawCircles(Canvas canvas, List<(float x, float y, string name)> coordinates)
        {
            foreach (var (x, y, name) in coordinates)
            {
                Ellipse circle = new Ellipse
                {
                    Width = 50,
                    Height = 50,
                    Fill = new SolidColorBrush(Microsoft.UI.Colors.Red)
                };
                Canvas.SetLeft(circle, x - 25); // Center the circle based on its radius
                Canvas.SetTop(circle, y - 25);

                TextBlock label = new TextBlock
                {
                    Text = name,
                    Foreground = new SolidColorBrush(Microsoft.UI.Colors.Black),
                    FontSize = 14
                };
                Canvas.SetLeft(label, x - 10); 
                Canvas.SetTop(label, y + 30); 

                canvas.Children.Add(circle);
                canvas.Children.Add(label);
            }

            this.Content = canvas; // Set the canvas as the content of the window
        }
        private void DrawLine(Canvas canvas, Brush brush, List<(float x, float y)> points)
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
        private void DrawLines(Canvas canvas, Brush brush, List<((float x, float y) start,(float x, float y) end)> lines)
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
