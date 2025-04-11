using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Geo.Data;
using App1.Geo.Model;
using Microsoft.EntityFrameworkCore;


namespace App1.Geo.Services
{
    public class DeliveryService
    {
        private readonly AppDbContext _context = new();

        public async Task<List<Delivery>> GetDeliveryAsync() => await _context.Delivery.ToListAsync();

        public async Task<Delivery> GetDeliveryByIdAsync(int deliveryId)
        {
            return await _context.Delivery.FindAsync(deliveryId);
        }
    }
}
