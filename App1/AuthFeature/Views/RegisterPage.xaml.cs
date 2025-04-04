using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using EuroTruck.AuthFeature.ViewModels;
using EuroTruck.AuthFeature.Views;

namespace EuroTruck.AuthFeature.Views
{
    public sealed partial class RegisterPage : Page
    {
        private readonly RegisterViewModel viewModel = new RegisterViewModel();

        public RegisterPage()
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }

        private void OnRegisterClicked(object sender, RoutedEventArgs e)
        {
            viewModel.Password = passwordBox.Password;
            viewModel.ConfirmPassword = confirmBox.Password;
            viewModel.RegisterCommand.Execute(null);
        }

        private void OnLoginNavigate(object sender, RoutedEventArgs e)
        {
            EuroTruck.MainWindow.AppFrame.Navigate(typeof(LoginPage));
        }
    }
}
