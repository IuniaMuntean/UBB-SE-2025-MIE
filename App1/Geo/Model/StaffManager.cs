using System;
using System.Collections.Generic;
using System.Linq;

namespace App1.Geo.Model
{
    public class StaffManager
    {
        private static readonly Dictionary<string, List<string>> ManagerDrivers = new()
        {
            { "John Smith", new List<string> { "Mike Johnson", "Sarah Williams", "David Brown", "Lisa Davis", "Tom Wilson" } },
            { "Emma Thompson", new List<string> { "James Anderson", "Emily Taylor", "Robert Martinez", "Patricia Garcia", "Michael Lee" } },
            { "Alex Rodriguez", new List<string> { "Jennifer White", "Christopher Clark", "Amanda Hall", "Daniel Turner", "Michelle King" } }
        };

        private static readonly Random Random = new();

        public static (string Manager, string Driver) AssignStaff()
        {
            var manager = ManagerDrivers.Keys.ElementAt(Random.Next(ManagerDrivers.Count));
            var drivers = ManagerDrivers[manager];
            var driver = drivers[Random.Next(drivers.Count)];
            return (manager, driver);
        }

        public static DateTime FindNextAvailableTimeSlot(List<Delivery> existingDeliveries, string driver)
        {
            var today = DateTime.Today;
            var startTime = today.AddHours(7); 
            var endTime = today.AddHours(22); 

            var driverDeliveries = existingDeliveries
                .Where(d => d.Driver == driver && 
                           DateTime.Parse(d.GoTime).Date == today)
                .OrderBy(d => DateTime.Parse(d.GoTime))
                .ToList();

            if (!driverDeliveries.Any())
                return startTime;

            var currentTime = startTime;
            foreach (var delivery in driverDeliveries)
            {
                var deliveryStart = DateTime.Parse(delivery.GoTime);
                var deliveryEnd = DateTime.Parse(delivery.ArrTime);

                if (deliveryStart - currentTime >= TimeSpan.FromHours(2))
                    return currentTime;

                currentTime = deliveryEnd;
            }

            if (endTime - currentTime >= TimeSpan.FromHours(2))
                return currentTime;

            return FindNextAvailableTimeSlot(existingDeliveries, driver);
        }
    }
} 