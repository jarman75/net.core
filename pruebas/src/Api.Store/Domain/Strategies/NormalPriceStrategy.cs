using System;

namespace Api.Store.Domain.Strategies
{
    public class NormalPriceStrategy : IPriceStrategy
    {
        public double CalculateCostPrice(ItemStock stock, DateTime? date = null)
        {
            return stock.Price;
        }
    }
}
