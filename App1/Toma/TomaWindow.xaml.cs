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
using App1.Vlad.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Windowing;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App1.Toma
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TomaWindow : Window
    {
        private readonly AppDbContext _dbContext;

        public TomaWindow()
        {
            this.InitializeComponent();
            _dbContext = new AppDbContext();

            // Set window size
            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            appWindow.Resize(new Windows.Graphics.SizeInt32(600, 500));
        }

        private async void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(ClientNameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(CargoTypeTextBox.Text) ||
                    string.IsNullOrWhiteSpace(SourceCityTextBox.Text) ||
                    string.IsNullOrWhiteSpace(DestinationCityTextBox.Text))
                {
                    await ShowMessage("Please fill in all required fields.");
                    return;
                }

                // Parse weight
                if (!double.TryParse(CargoWeightTextBox.Text, out double weight))
                {
                    await ShowMessage("Please enter a valid weight.");
                    return;
                }

                // Create new order
                var order = new Order
                {
                    ClientName = ClientNameTextBox.Text,
                    CargoType = CargoTypeTextBox.Text,
                    CargoWeight = weight,
                    SourceCity = SourceCityTextBox.Text,
                    DestinationCity = DestinationCityTextBox.Text
                };

                // Add to database
                await _dbContext.Orders.AddAsync(order);
                await _dbContext.SaveChangesAsync();

                await ShowMessage("Order added successfully!");
                ClearForm();
                // Close the window after successful order creation
                this.Close();
            }
            catch (DbUpdateException ex)
            {
                await ShowMessage($"Error adding order: {ex.Message}");
            }
            catch (Exception ex)
            {
                await ShowMessage($"Error adding order: {ex.Message}");
            }
        }

        private async void UpdateOrder_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement)?.DataContext is Order order)
            {
                // Implement update logic here
            }
        }

        private async void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as FrameworkElement)?.DataContext is Order order)
            {
                // Implement delete logic here
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

        private void ClearForm()
        {
            ClientNameTextBox.Text = string.Empty;
            CargoTypeTextBox.Text = string.Empty;
            CargoWeightTextBox.Text = string.Empty;
            SourceCityTextBox.Text = string.Empty;
            DestinationCityTextBox.Text = string.Empty;
        }

        private async Task ShowMessage(string message)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Message",
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.Content.XamlRoot
            };

            await dialog.ShowAsync();
        }
    }
}
