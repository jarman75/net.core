using System;

namespace Api.Store.Domain.Strategies
{
    public class PerishablePriceStrategy : IPriceStrategy
    {
        public double CalculatePrice(ItemStock stock, DateTime? date = null)
        {
            double result = stock.Price;

            if (stock.Price > 0 && stock.ExpirationDate.HasValue)
            {

                var expirationDays = stock.ExpirationDate.Value.Date.Subtract(date.GetValueOrDefault(DateTime.Now).Date).Days;

                if (expirationDays <= 1)
                    result = 0;

                if (expirationDays >= 2 && expirationDays <=3)
                    result /= 2;

                if (expirationDays >= 4 && expirationDays <= 5)
                    result /= 4;
            }

            return result;

        }
    }
}
