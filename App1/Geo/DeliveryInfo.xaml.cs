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
using System.Threading.Tasks;
using App1.Vlad.Model;
using App1.Geo.Model;
using App1.Geo.Data;
using App1.Geo.ViewModels;
using App1.Geo.Services;
using Microsoft.EntityFrameworkCore;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App1.Geo
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DeliveryInfo : Window
    {
        private readonly AppDbContext _dbContext;
        public DeliveryViewModel ViewModel { get; }

        public DeliveryInfo(Order order)
        {
            InitializeComponent();
            _dbContext = new AppDbContext();

            Console.WriteLine($"Initializing DeliveryInfo for order {order.OrderId}");

            // Initialize ViewModel first
            ViewModel = new DeliveryViewModel(new DeliveryService(_dbContext));

            // Get or create delivery for this order
            var delivery = _dbContext.Delivery
                .Include(d => d.Order)
                .FirstOrDefault(d => d.OrderId == order.OrderId) ?? new Delivery
                {
                    Manager = "Default Manager",
                    Departure = order.SourceCity,
                    Destination = order.DestinationCity,
                    Weight = (decimal)order.CargoWeight,
                    OrderId = order.OrderId,
                    Order = order
                };

            Console.WriteLine($"Found/created delivery with ID: {delivery.Id}");

            // Set the delivery and data context
            ViewModel.Delivery = delivery;
            
            // Set DataContext on the root Grid
            if (Content is Grid rootGrid)
            {
                rootGrid.DataContext = ViewModel;
                Console.WriteLine("DataContext set successfully");
            }
        }

        private async void SaveDeliveryDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Console.WriteLine("Starting to save delivery details...");
                Console.WriteLine($"Current delivery state - ID: {ViewModel.Delivery.Id}, OrderId: {ViewModel.Delivery.OrderId}");
                Console.WriteLine($"Driver: {ViewModel.Delivery.Driver}, TruckId: {ViewModel.Delivery.TruckId}");
                Console.WriteLine($"DepartureTime: {ViewModel.Delivery.DepartureTime}, ArrivalTime: {ViewModel.Delivery.ArrivalTime}");
                Console.WriteLine($"Distance: {ViewModel.Delivery.Distance}");

                await ViewModel.SaveDeliveryAsync();
                await ShowMessage("Delivery details saved successfully!");
                Console.WriteLine("Delivery saved successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving delivery: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                await ShowMessage($"Error: {ex.Message}");
            }
        }

        private void GenerateRoute_Click(object sender, RoutedEventArgs e)
        {
            ShowMessage("Route generation will be implemented later");
        }

        private void CalculatePricing_Click(object sender, RoutedEventArgs e)
        {
            ShowMessage("Pricing calculation will be implemented later");
        }

        private async Task ShowMessage(string message)
        {
            var dialog = new ContentDialog
            {
                Title = "Message",
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = Content.XamlRoot
            };
            await dialog.ShowAsync();
        }
    }
}
