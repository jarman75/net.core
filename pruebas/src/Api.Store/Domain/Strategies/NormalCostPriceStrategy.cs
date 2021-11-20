using System;

namespace Api.Store.Domain.Strategies
{
    public class NormalCostPriceStrategy : ICostPriceStrategy
    {
        public double CalculateCostPrice(ItemStock stock, DateTime? date = null)
        {
            return stock.CostPrice;
        }
    }
}
