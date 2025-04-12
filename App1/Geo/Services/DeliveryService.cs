using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using App1.Vlad.Model;
using App1.Geo.Model;
using App1.Geo.Data;
using System.Diagnostics;

namespace App1.Geo.Services
{
    public class DeliveryService
    {
        private readonly AppDbContext _context;

        public DeliveryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Delivery>> GetDeliveriesAsync()
        {
            return await _context.Delivery
                .Include(d => d.Order)
                .OrderByDescending(d => d.CreatedAt)
                .ToListAsync();
        }

        public async Task<Delivery> GetDeliveryByIdAsync(int deliveryId)
        {
            return await _context.Delivery
                .Include(d => d.Order)
                .FirstOrDefaultAsync(d => d.Id == deliveryId);
        }

        public async Task<Delivery> GetDeliveryByOrderIdAsync(int orderId)
        {
            return await _context.Delivery
                .Include(d => d.Order)
                .FirstOrDefaultAsync(d => d.OrderId == orderId);
        }

        public async Task<Delivery> CreateDeliveryAsync(Delivery delivery)
        {
            Debug.WriteLine($"Creating new delivery: OrderId={delivery.OrderId}, Driver={delivery.Driver}, TruckId={delivery.TruckId}, Distance={delivery.Distance}");
            Debug.WriteLine($"Delivery details: Driver={delivery.Driver}, TruckId={delivery.TruckId}, Distance={delivery.Distance}");
            Debug.WriteLine($"DepartureTime={delivery.DepartureTime}, ArrivalTime={delivery.ArrivalTime}");
            
            // Ensure all DateTime values are in UTC
            delivery.CreatedAt = DateTime.UtcNow;
            delivery.UpdatedAt = DateTime.UtcNow;
            delivery.DepartureTime = delivery.DepartureTime.ToUniversalTime();
            delivery.ArrivalTime = delivery.ArrivalTime.ToUniversalTime();
            
            try
            {
                // Check if the order exists
                var existingOrder = await _context.Orders.FindAsync(delivery.OrderId);
                if (existingOrder == null)
                {
                    throw new InvalidOperationException($"Order with ID {delivery.OrderId} does not exist.");
                }

                // Set the order reference
                delivery.Order = existingOrder;
                
                Debug.WriteLine("Attempting to add delivery to context...");
                await _context.Delivery.AddAsync(delivery);
                Debug.WriteLine("Delivery added to context, attempting to save changes...");
                await _context.SaveChangesAsync();
                Debug.WriteLine($"Successfully created delivery with ID: {delivery.Id}");
                return delivery;
            }
            catch (DbUpdateException dbEx)
            {
                Debug.WriteLine("Database Update Exception:");
                Debug.WriteLine($"Message: {dbEx.Message}");
                Debug.WriteLine($"Entity Entries:");
                foreach (var entry in dbEx.Entries)
                {
                    Debug.WriteLine($"Entity: {entry.Entity.GetType().Name}");
                    Debug.WriteLine($"State: {entry.State}");
                    foreach (var prop in entry.Properties)
                    {
                        Debug.WriteLine($"Property: {prop.Metadata.Name}, Value: {prop.CurrentValue}");
                    }
                }
                if (dbEx.InnerException != null)
                {
                    Debug.WriteLine($"Inner Exception: {dbEx.InnerException.Message}");
                }
                throw;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating delivery: {ex.Message}");
                Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                throw;
            }
        }

        public async Task<Delivery> UpdateDeliveryAsync(Delivery delivery)
        {
            Debug.WriteLine($"Updating delivery {delivery.Id}: Driver={delivery.Driver}, TruckId={delivery.TruckId}, Distance={delivery.Distance}");
            Debug.WriteLine($"New details: Driver={delivery.Driver}, TruckId={delivery.TruckId}, Distance={delivery.Distance}");
            Debug.WriteLine($"DepartureTime={delivery.DepartureTime}, ArrivalTime={delivery.ArrivalTime}");
            
            // Ensure all DateTime values are in UTC
            delivery.UpdatedAt = DateTime.UtcNow;
            delivery.DepartureTime = delivery.DepartureTime.ToUniversalTime();
            delivery.ArrivalTime = delivery.ArrivalTime.ToUniversalTime();
            
            try
            {
                Debug.WriteLine("Attempting to update delivery in context...");
                _context.Delivery.Update(delivery);
                Debug.WriteLine("Delivery updated in context, attempting to save changes...");
                await _context.SaveChangesAsync();
                Debug.WriteLine($"Successfully updated delivery {delivery.Id}");
                return delivery;
            }
            catch (DbUpdateException dbEx)
            {
                Debug.WriteLine("Database Update Exception:");
                Debug.WriteLine($"Message: {dbEx.Message}");
                Debug.WriteLine($"Entity Entries:");
                foreach (var entry in dbEx.Entries)
                {
                    Debug.WriteLine($"Entity: {entry.Entity.GetType().Name}");
                    Debug.WriteLine($"State: {entry.State}");
                    foreach (var prop in entry.Properties)
                    {
                        Debug.WriteLine($"Property: {prop.Metadata.Name}, Value: {prop.CurrentValue}");
                    }
                }
                if (dbEx.InnerException != null)
                {
                    Debug.WriteLine($"Inner Exception: {dbEx.InnerException.Message}");
                }
                throw;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating delivery: {ex.Message}");
                Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                throw;
            }
        }

        public async Task DeleteDeliveryAsync(int deliveryId)
        {
            var delivery = await _context.Delivery.FindAsync(deliveryId);
            if (delivery != null)
            {
                _context.Delivery.Remove(delivery);
                await _context.SaveChangesAsync();
            }
        }
    }
} 