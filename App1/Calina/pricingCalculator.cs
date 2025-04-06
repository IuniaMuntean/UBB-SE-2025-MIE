namespace App1.Calina
{
    public class PricingCalculator
    {
        public float CalculatePrice(Route route)
        {
            return route.GetRevenue() - route.GetCost();
        }
    }
}
