using Api.Store.Domain.Strategies;

namespace Api.Store.Domain.Factory
{
    public class CostPriceStrategyFactory : ICostPriceStrategyFactory
    {
        public ICostPriceStrategy Create(Item item)
        {

            ICostPriceStrategy result = null; 
            
            switch (item.Category)
            {
                case Category.Normal:
                    result = new NormalCostPriceStrategy();
                    break;
                case Category.Aged:
                    result = new AgedCostPriceStrategy();
                    break;
                case Category.Perishable:
                    result = new PerishableCostPriceStrategy();
                    break;             
                default:
                    result = new NormalCostPriceStrategy();
                    break;
            }

            return result;
        }
    }
}
