using App1.Vlad.Model;
using App1.Vlad.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App1.Vlad
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VladWindow : Window
    {
        public OrderViewModel ViewModel { get; } = new(); 

        public VladWindow()
        {
            this.InitializeComponent();
            _ = ViewModel.LoadOrdersAsync(); 
        }

        private void AddOrder_Click_Toma(object sender, RoutedEventArgs e)
        {
            Toma.TomaWindow t = new Toma.TomaWindow();
            t.Activate();
            t.Closed += async (s, e) => {
                await ViewModel.LoadOrdersAsync();
            };
        }

        private async void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement)?.DataContext is Order order)
            {
                await ViewModel.DeleteOrderAsync(order);
            }
        }

        private async void UpdateOrder_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement)?.DataContext is Order order)
            {
                await ViewModel.UpdateOrderAsync(order);
            }
        }

        private void ViewOrder_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement)?.DataContext is Order order)
            {
                Geo.DeliveryInfo geoView = new Geo.DeliveryInfo(order);
                geoView.Activate();
            }
        }
    }
}
