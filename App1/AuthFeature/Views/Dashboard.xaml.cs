using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace EuroTruck.AuthFeature.Views
{
    public sealed partial class DashboardPage : Page
    {
        public DashboardPage()
        {
            this.InitializeComponent();
        }

        private void OnLogoutClicked(object sender, RoutedEventArgs e)
        {
            // Navigate back to login page
            EuroTruck.MainWindow.AppFrame.Navigate(typeof(LoginPage));
        }
    }
}
