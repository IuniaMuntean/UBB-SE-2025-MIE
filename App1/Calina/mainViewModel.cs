// ViewModels/MainViewModel.cs
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace App1.Calina
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly PricingCalculator pricingCalculator = new();
        private readonly ProfitCalculator profitCalculator = new();

        private float _calculatedPrice;
        public float CalculatedPrice
        {
            get => _calculatedPrice;
            set { _calculatedPrice = value; OnPropertyChanged(); }
        }

        private float _calculatedProfit;
        public float CalculatedProfit
        {
            get => _calculatedProfit;
            set { _calculatedProfit = value; OnPropertyChanged(); }
        }

        public Route CurrentRoute { get; set; } = new();

        public void Calculate()
        {
            CalculatedPrice = pricingCalculator.CalculatePrice(CurrentRoute);
            CalculatedProfit = profitCalculator.CalculateProfit(CurrentRoute);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
