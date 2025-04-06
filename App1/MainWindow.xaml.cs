using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using App1.AuthFeature.Views;

namespace App1
{
    public sealed partial class MainWindow : Window
    {
        public static Frame AppFrame { get; private set; }

        public MainWindow()
        {
            this.InitializeComponent();
            AppFrame = MainFrame;
            AppFrame.Navigate(typeof(LoginPage));
        }
    }
}
