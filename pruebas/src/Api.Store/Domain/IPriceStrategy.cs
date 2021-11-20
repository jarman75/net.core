using System;

namespace Api.Store.Domain
{
    public interface IPriceStrategy
    {
        double CalculatePrice(ItemStock stock, DateTime? date = null);
    }
}
