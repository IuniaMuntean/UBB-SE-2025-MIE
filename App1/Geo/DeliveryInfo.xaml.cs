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
            
            _ = InitializeDeliveryAsync(order);
        }

        private async Task InitializeDeliveryAsync(Order order)
        {
            try
            {
                await _viewModel.LoadDeliveriesAsync();
                var existingDeliveries = _viewModel.GetAllDeliveries();

                var (manager, driver) = StaffManager.AssignStaff();

                var departureTime = StaffManager.FindNextAvailableTimeSlot(existingDeliveries, driver);
                var arrivalTime = departureTime.AddHours(2);

                _currentDelivery = new Delivery
                {
                    Departure = order.SourceCity,
                    Destination = order.DestinationCity,
                    Client = order.ClientName,
                    CargoType = order.CargoType,
                    Weight = (int)order.CargoWeight,
                    Manager = manager,
                    Driver = driver,
                    GoTime = departureTime.ToString("yyyy-MM-dd HH:mm"),
                    ArrTime = arrivalTime.ToString("yyyy-MM-dd HH:mm")
                };

                DataContext = _currentDelivery;

                await SaveDeliveryAsync();
            }
            catch (Exception ex)
            {
                var dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = $"Failed to initialize delivery: {ex.Message}",
                    CloseButtonText = "OK"
                };
                dialog.ShowAsync();
            }
        }
        
        private void GenerateRoute_Click(object sender, RoutedEventArgs e)
        {
            RouteGeneration.RouteGenerationWindow routeView = new RouteGeneration.RouteGenerationWindow();
            routeView.Activate();
        }
        
        private void CalculatePricing_Click(object sender, RoutedEventArgs e)
        {
            Calina.CalinaWindow calinaView = new Calina.CalinaWindow();
            calinaView.Activate();
        }
        
        private async Task SaveDeliveryAsync()
        {
            try
            {
                if (await _viewModel.DeliveryExistsAsync(_currentDelivery))
                {
                    var dialog = new ContentDialog
                    {
                        Title = "Information",
                        Content = "This delivery already exists in the system.",
                        CloseButtonText = "OK"
                    };
                    dialog.ShowAsync();
                }
                else
                {
                    await _viewModel.AddDeliveryAsync(_currentDelivery);
                    var dialog = new ContentDialog
                    {
                        Title = "Success",
                        Content = "Delivery information saved successfully.",
                        CloseButtonText = "OK"
                    };
                    dialog.ShowAsync();
                }
            }
            catch (Exception ex)
            {
                var dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = $"Failed to save delivery: {ex.Message}",
                    CloseButtonText = "OK"
                };
                dialog.ShowAsync();
            }
        }
    }
}
