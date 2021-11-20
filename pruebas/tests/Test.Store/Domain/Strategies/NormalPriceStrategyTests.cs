using Api.Store.Domain;
using Api.Store.Domain.Strategies;
using NUnit.Framework;

namespace Test.Store.Domain.Strategies
{
    public class NormalPriceStrategyTests
    {
        private IPriceStrategy strategy;

        [SetUp]
        public void Setup()
        {
            strategy = new NormalPriceStrategy();
        }

        [Test]
        public void Same_price()
        {
            //Arrange
            var stock = new ItemStock { Price = 100 };

            //Act
            var newPrice = strategy.CalculateCostPrice(stock);

            //Assert
            Assert.AreEqual(100, newPrice, 0.02d);
        }



    }
}
