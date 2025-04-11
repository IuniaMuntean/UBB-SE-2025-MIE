using App1.Geo.Model;
using App1.Geo.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Geo.ViewModels
{
    public class DeliveryViewModel
    {
        private readonly Delivery _delivery;
        private readonly List<Delivery> _existingDeliveries;

        public DeliveryViewModel(Delivery delivery, List<Delivery> existingDeliveries)
        {
            _delivery = delivery;
            _existingDeliveries = existingDeliveries;
        }

        public int Id => _delivery.Id;
        public string Manager => _delivery.Manager;
        public string Departure => _delivery.Departure;
        public string Destination => _delivery.Destination;
        public int Distance => _delivery.Distance;
        public string Driver
        {
            get
            {
                foreach (var d in _existingDeliveries)
                {
                    if (d.Driver == _delivery.Driver)
                    {
                        if (TimesOverlap(d.GoTime, d.ArrTime, _delivery.GoTime, _delivery.ArrTime))
                        {
                            return "Driver unavailable";
                        }
                    }
                }
                return _delivery.Driver;
            }
        }
        public string DepartureTime => _delivery.GoTime;
        public string ArrivalTime => _delivery.ArrTime;

        public int TruckId => _delivery.Truck;

        public string CargoWeight
        {
            get
            {
                if (_delivery.Weight > 44000)
                {
                    return "Violates regulations";
                }
                return _delivery.Weight.ToString();
            }
        }

        public string CargoStatus
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_delivery.CargoIds))
                    return "No cargo assigned";

                var cargoList = _delivery.CargoIds.Split(',').Where(id => !string.IsNullOrWhiteSpace(id)).ToList();

                if (cargoList.Count < 5)
                    return "Insufficient cargo (minimum 5 required)";

                return $"Cargo OK ({cargoList.Count} items)";
            }
        }
        private bool TimesOverlap(string start1, string end1, string start2, string end2)
        {
            if (DateTime.TryParse(start1, out var s1) &&
                DateTime.TryParse(end1, out var e1) &&
                DateTime.TryParse(start2, out var s2) &&
                DateTime.TryParse(end2, out var e2))
            {
                return s1 < e2 && s2 < e1; 
            }

            return false;
        }

    }

}
