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
using App1.Geo.Model;
using App1.Geo.ViewModels;
using App1.Vlad.Model;

namespace App1.Geo
{
    public sealed partial class DeliveryInfo : Window
    {
        private readonly DeliveryViewModel _viewModel = new();
        private Delivery _currentDelivery;
        
        public DeliveryInfo(Order order)
        {
            this.InitializeComponent();
            
            _currentDelivery = new Delivery
            {
                Departure = order.SourceCity,
                Destination = order.DestinationCity,
                Client = order.ClientName,
                CargoType = order.CargoType,
                Weight = (int)order.CargoWeight,
                Manager = "Default Manager",
                Driver = "Unassigned",      
                GoTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm"), 
                ArrTime = "Pending"         
            };
            
            DataContext = _currentDelivery;
            
            _ = _viewModel.LoadDeliveriesAsync();
        }
        
        private void GenerateRoute_Click(object sender, RoutedEventArgs e)
        {
            
            var dialog = new ContentDialog
            {
                Title = "Route Generation",
                Content = "Route generation feature will be implemented here.",
                CloseButtonText = "OK"
            };
            
            dialog.ShowAsync();
        }
        
        private void CalculatePricing_Click(object sender, RoutedEventArgs e)
        {
            Calina.CalinaWindow calinaView = new Calina.CalinaWindow();
            calinaView.Activate();
        }
        
        private async void SaveDelivery_Click(object sender, RoutedEventArgs e)
        {
            await _viewModel.AddDeliveryAsync(_currentDelivery);
            
            var dialog = new ContentDialog
            {
                Title = "Success",
                Content = "Delivery information saved successfully.",
                CloseButtonText = "OK"
            };
            
            dialog.ShowAsync();
            
            this.Close();
        }
    }
}
