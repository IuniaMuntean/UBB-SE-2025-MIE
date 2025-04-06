
namespace App1.Calina
{
    public class Route
    {
        public float Distance { get; set; }
        public float Cost { get; set; }
        public float Revenue { get; set; }

        public float GetDistance() => Distance;
        public float GetCost() => Cost;
        public float GetRevenue() => Revenue;
    }
}
