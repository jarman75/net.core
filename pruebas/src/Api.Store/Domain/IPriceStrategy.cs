using System;

namespace Api.Store.Domain
{
    public interface IPriceStrategy
    {
        double CalculateCostPrice(ItemStock stock, DateTime? date = null);
    }
}
