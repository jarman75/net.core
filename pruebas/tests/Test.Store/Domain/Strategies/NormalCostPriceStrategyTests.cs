using Api.Store.Domain;
using Api.Store.Domain.Strategies;
using NUnit.Framework;

namespace Test.Store.Domain.Strategies
{
    public class NormalCostPriceStrategyTests
    {
        private ICostPriceStrategy strategy;

        [SetUp]
        public void Setup()
        {
            strategy = new NormalCostPriceStrategy();
        }

        [Test]
        public void Same_price()
        {
            //Arrange
            var stock = new ItemStock { CostPrice = 100 };

            //Act
            var newCost = strategy.CalculateCostPrice(stock);

            //Assert
            Assert.AreEqual(100, newCost, 0.02d);
        }



    }
}
