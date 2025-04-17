using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using App1.Calina;
using App1.Iunia_Fabi.View;

namespace App1.AuthFeature.Views
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
            App1.MainWindow.AppFrame.Navigate(typeof(LoginPage));
        }

        private void btnGraphView_Click(object sender, RoutedEventArgs e)
        {
            // opens graph view
            GraphView graphView = new GraphView(1, 5);
            graphView.Activate();
        }

        private void btnProfitCalc_Click(object sender, RoutedEventArgs e)
        {
            Calina.ProfitCalcView profitCalcView = new Calina.ProfitCalcView();
            profitCalcView.Activate();
        }

        private void btnVlad_Click(object sender, RoutedEventArgs e)
        {
            Vlad.VladWindow v = new Vlad.VladWindow();
            v.Activate();
        }
    }
}
