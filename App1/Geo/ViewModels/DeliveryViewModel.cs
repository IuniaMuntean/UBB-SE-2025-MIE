using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using App1.Geo.Model;
using App1.Geo.Services;
using App1.Vlad.Model;

namespace App1.Geo.ViewModels
{
    public class DeliveryViewModel : INotifyPropertyChanged
    {
        private readonly DeliveryService _deliveryService;
        private Delivery _delivery;

        public DeliveryViewModel(DeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
            _delivery = new Delivery();
        }

        public Delivery Delivery
        {
            get => _delivery;
            set
            {
                _delivery = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Order));
            }
        }

        public Order Order
        {
            get => _delivery?.Order;
            set
            {
                if (_delivery != null)
                {
                    _delivery.Order = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Id
        {
            get => _delivery.Id;
            set
            {
                _delivery.Id = value;
                OnPropertyChanged();
            }
        }

        public string Manager
        {
            get => _delivery.Manager;
            set
            {
                _delivery.Manager = value;
                OnPropertyChanged();
            }
        }

        public string Departure
        {
            get => _delivery.Departure;
            set
            {
                _delivery.Departure = value;
                OnPropertyChanged();
            }
        }

        public string Destination
        {
            get => _delivery.Destination;
            set
            {
                _delivery.Destination = value;
                OnPropertyChanged();
            }
        }

        public decimal Distance
        {
            get => _delivery.Distance;
            set
            {
                _delivery.Distance = value;
                OnPropertyChanged();
            }
        }

        public string Driver
        {
            get => _delivery.Driver;
            set
            {
                _delivery.Driver = value;
                OnPropertyChanged();
            }
        }

        public DateTime DepartureTime
        {
            get => _delivery.DepartureTime;
            set
            {
                _delivery.DepartureTime = value;
                OnPropertyChanged();
            }
        }

        public DateTime ArrivalTime
        {
            get => _delivery.ArrivalTime;
            set
            {
                _delivery.ArrivalTime = value;
                OnPropertyChanged();
            }
        }

        public int TruckId
        {
            get => _delivery.TruckId;
            set
            {
                _delivery.TruckId = value;
                OnPropertyChanged();
            }
        }

        public decimal Weight
        {
            get => _delivery.Weight;
            set
            {
                _delivery.Weight = value;
                OnPropertyChanged();
            }
        }

        public int OrderId
        {
            get => _delivery.OrderId;
            set
            {
                _delivery.OrderId = value;
                OnPropertyChanged();
            }
        }

        public string CargoStatus
        {
            get
            {
                if (Order == null)
                    return "No order assigned";
                return $"Order #{Order.OrderId} - {Order.CargoType}";
            }
        }

        public async Task LoadDeliveryAsync(int deliveryId)
        {
            Delivery = await _deliveryService.GetDeliveryByIdAsync(deliveryId);
            OnPropertyChanged(nameof(CargoStatus));
        }

        public async Task LoadDeliveryByOrderAsync(int orderId)
        {
            Delivery = await _deliveryService.GetDeliveryByOrderIdAsync(orderId);
            OnPropertyChanged(nameof(CargoStatus));
        }

        public async Task SaveDeliveryAsync()
        {
            if (Delivery.Id == 0)
            {
                Delivery = await _deliveryService.CreateDeliveryAsync(Delivery);
            }
            else
            {
                Delivery = await _deliveryService.UpdateDeliveryAsync(Delivery);
            }
        }

        public async Task DeleteDeliveryAsync()
        {
            if (Delivery.Id != 0)
            {
                await _deliveryService.DeleteDeliveryAsync(Delivery.Id);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
