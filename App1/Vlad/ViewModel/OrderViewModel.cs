using App1.Vlad.Model;
using App1.Vlad.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace App1.Vlad.ViewModel
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Order> Orders { get; } = new();
        private readonly OrderService _orderService = new();

        public async Task LoadOrdersAsync()
        {
            Orders.Clear();
            var orders = await _orderService.GetOrdersAsync();
            foreach (var order in orders)
                Orders.Add(order);
        }

        public async Task AddOrderAsync(Order order)
        {
            await _orderService.AddOrderAsync(order);
            Orders.Add(order);
        }

        public async Task DeleteOrderAsync(Order order)
        {
            await _orderService.DeleteOrderAsync(order.OrderId);
            Orders.Remove(order);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            await _orderService.UpdateOrderAsync(order);
            Orders[Orders.IndexOf(order)] = order;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
