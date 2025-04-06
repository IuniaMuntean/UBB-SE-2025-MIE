namespace App1.Calina
{
    public class ProfitCalculator
    {
        public float CalculateProfit(Route route)
        {
            return route.GetRevenue() - route.GetCost(); // Same as price in this example
        }
    }
}
