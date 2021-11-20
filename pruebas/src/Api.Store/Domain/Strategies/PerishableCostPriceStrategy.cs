﻿using System;

namespace Api.Store.Domain.Strategies
{
    public class PerishableCostPriceStrategy : ICostPriceStrategy
    {
        public double CalculateCostPrice(ItemStock stock, DateTime? date = null)
        {
            double result = stock.CostPrice;

            if (stock.CostPrice > 0 && stock.ExpirationDate.HasValue)
            {

                var expirationDays = stock.ExpirationDate.Value.Date.Subtract(date.GetValueOrDefault(DateTime.Now).Date).Days;

                if (expirationDays < 1)
                    result = 0;

                if (expirationDays == 2)
                    result /= 2;

                if (expirationDays > 2 && expirationDays < 5)
                    result /= 4;
            }

            return result;

        }
    }
}
