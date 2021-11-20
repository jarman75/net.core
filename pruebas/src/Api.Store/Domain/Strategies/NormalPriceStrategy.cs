using System;

namespace Api.Store.Domain.Strategies
{
    public class NormalPriceStrategy : IPriceStrategy
    {
        public double CalculatePrice(ItemStock stock, DateTime? date = null)
        {
            return stock.Price;
        }
    }
}
