using System;

namespace Api.Store.Domain.Strategies
{
    public class AgedPriceStrategy : IPriceStrategy
    {
        public double CalculatePrice(ItemStock stock, DateTime? date)
        {
            double result = stock.Price;

            if (stock.Price > 0 && stock.ManufacturingDate.HasValue && stock.Entrydate.HasValue)
            {

                var totalDaysFromManufacturing = date.GetValueOrDefault(DateTime.Now).Date.Subtract(stock.ManufacturingDate.Value.Date).TotalDays;
                var age = Math.Truncate(totalDaysFromManufacturing / 365);

                var totalDaysFromEntry = date.GetValueOrDefault(DateTime.Now).Date.Subtract(stock.Entrydate.Value.Date).TotalDays;
                var storeYears = Math.Truncate(totalDaysFromEntry / 365);

                var computeYears = age - storeYears;

                if (computeYears >= 1 && computeYears <= 5)
                    result *= 1.05;

                if (computeYears >= 6 && computeYears <= 10)
                    result *= 1.10;

                if (computeYears > 10)
                    result *= 1 + (computeYears / 100);
            }

            return result;
        }

        
    }
}
