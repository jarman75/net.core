using Api.Store.Domain.Strategies;

namespace Api.Store.Domain.Factory
{
    public class PriceStrategyFactory : IPriceStrategyFactory
    {
        public IPriceStrategy Create(Item item)
        {

            IPriceStrategy result = null; 
            
            switch (item.Category)
            {
                case Category.Normal:
                    result = new NormalPriceStrategy();
                    break;
                case Category.Aged:
                    result = new AgedPriceStrategy();
                    break;
                case Category.Perishable:
                    result = new PerishablePriceStrategy();
                    break;             
                default:
                    result = new NormalPriceStrategy();
                    break;
            }

            return result;
        }
    }
}
