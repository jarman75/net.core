using System;

namespace Api.Store.Domain
{
    public interface ICostPriceStrategy
    {
        double CalculateCostPrice(ItemStock stock, DateTime? date = null);
    }
}
