namespace Api.Store.Domain
{
    public interface IPriceStrategyFactory
    {
        IPriceStrategy Create(Item item);
    }
}
