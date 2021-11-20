namespace Api.Store.Domain
{
    public interface ICostPriceStrategyFactory
    {
        ICostPriceStrategy Create(Item item);
    }
}
