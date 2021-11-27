using Api.Store.Domain.Strategies;

namespace Api.Store.Domain.Factory
{
    public class PriceStrategyFactory : IPriceStrategyFactory
    {
        public IPriceStrategy Create(Item item)
        {
            return item.Category switch
            {
                Category.Normal => new NormalPriceStrategy(),
                Category.Aged => new AgedPriceStrategy(),
                Category.Perishable => new PerishablePriceStrategy(),
                _ => new NormalPriceStrategy(),
            };
        }
    }
}
