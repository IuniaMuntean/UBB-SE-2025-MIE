using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml.Shapes;
using App1.Model;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App1
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            setSize();
            Graph mockGraph = new Graph();
            mockGraph.add(new City(1, "City 1", 0, 0));
            mockGraph.add(new City(2, "City 2", 200, 200));
            mockGraph.add(new City(3, "City 3", 150, 0));
            mockGraph.add(new City(4, "City 4", 50, 250));
            mockGraph.add(new City(5, "City 5", 300, 340));

            mockGraph.add(new Road(1, 2, 1));
            mockGraph.add(new Road(2, 3, 1));
            mockGraph.add(new Road(3, 4, 1));
            mockGraph.add(new Road(2, 4, 1));
            mockGraph.add(new Road(4, 5, 1));


            //mockGraph.path(1, 4).Select(city => (city.x, city.y)).ToList()
            Canvas canvas = new Canvas
            {
                Width = 600,
                Height = 600,
                Background = new SolidColorBrush(Microsoft.UI.Colors.LightGray)
            };
            DrawCircles(canvas, mockGraph.Cities().Select(city => (city.x, city.y)).ToList());
            DrawLines(canvas, new SolidColorBrush(Microsoft.UI.Colors.Black), mockGraph.Roads().Select(road => ((mockGraph.City(road.start).x, mockGraph.City(road.start).y), (mockGraph.City(road.end).x, mockGraph.City(road.end).y))).ToList());
            DrawLine(canvas, new SolidColorBrush(Microsoft.UI.Colors.Red), mockGraph.path(1, 5).Select(id => (mockGraph.City(id).x, mockGraph.City(id).y)).ToList());
        }

        private void setSize()
        {
            IntPtr hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Win32Interop.GetWindowIdFromWindow(hwnd);
            AppWindow appWindow = AppWindow.GetFromWindowId(windowId);
            appWindow.Resize(new SizeInt32(600, 600));
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            myButton.Content = "Clicked";
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

            this.Content = canvas; // Set the canvas as the content of the window
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
        private void DrawLines(Canvas canvas, Brush brush, List<((int x, int y) start,(int x, int y) end)> lines)
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
