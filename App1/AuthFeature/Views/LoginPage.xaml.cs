using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using EuroTruck.AuthFeature.ViewModels;
using EuroTruck.AuthFeature.Views;
using System;
using System.Threading.Tasks;

namespace EuroTruck.AuthFeature.Views
{
    public sealed partial class LoginPage : Page
    {
        private readonly LoginViewModel viewModel = new LoginViewModel();

        public LoginPage()
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }

        private async void OnLoginClicked(object sender, RoutedEventArgs e)
        {
            viewModel.Password = passwordBox.Password;
            bool success = await viewModel.Login();

            if (success)
            {
                EuroTruck.MainWindow.AppFrame.Navigate(typeof(DashboardPage));
            }
            else
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = "Login Failed",
                    Content = "Invalid username or password.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot 
                };
                _ = await dialog.ShowAsync();
            }
        }

        private void OnRegisterNavigate(object sender, RoutedEventArgs e)
        {
            EuroTruck.MainWindow.AppFrame.Navigate(typeof(RegisterPage));
        }
    }
}
