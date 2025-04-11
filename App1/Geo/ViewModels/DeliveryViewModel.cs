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
using Microsoft.UI.Xaml.Controls;

namespace App1.Geo.ViewModels
{
    public class DeliveryViewModel
    {
        private  List<Delivery> _deliveryCollection { get; } = new List<Delivery>();
        private readonly DeliveryService _deliveryService = new();

        public async Task LoadDeliveriesAsync()
        {
            _deliveryCollection.Clear();
            var deliveries = await _deliveryService.GetDeliveryAsync();
            _deliveryCollection.AddRange(deliveries);
        }
        public async Task AddDeliveryAsync(Delivery delivery)
        {
            await _deliveryService.AddDelivery(delivery);
            _deliveryCollection.Add(delivery);
        }

        public async Task DeleteDeliveryAsync(Delivery delivery)
        {
            await _deliveryService.DeleteDelivery(delivery);
            _deliveryCollection.Remove(delivery);
        }

        public async Task UpdateDeliveryAsync(Delivery updatedDelivery)
        {
            var index = _deliveryCollection.FindIndex(d => d.Id == updatedDelivery.Id);
            if (index >= 0)
            {
                _deliveryCollection[index] = updatedDelivery;
            }

            await _deliveryService.AddDelivery(updatedDelivery); 
        }

        public List<Delivery> GetAllDeliveries()
        {
            return new List<Delivery>(_deliveryCollection);
        }

        public Delivery GetDeliveryById(int id)
        {
            return _deliveryCollection.FirstOrDefault(d => d.Id == id);
        }


    }

}
