using Api.Store.Domain;
using Api.Store.Domain.Factory;
using Api.Store.Domain.Strategies;
using NUnit.Framework;

namespace Test.Store.Domain.Factory
{
    public class CostPriceStrategyFactoryTests
    {
        private ICostPriceStrategyFactory factory = new CostPriceStrategyFactory();
        
        [Test]
        public void NormalStrategy_instance()
        {
            //Arrange
            var item = new Item { Category = Category.Normal };

            //Act
            var strategy = factory.Create(item);

            //Assert
            Assert.IsInstanceOf(typeof(NormalCostPriceStrategy), strategy);
        }

        [Test]
        public void AgedStrategy_instance()
        {
            //Arrange
            var item = new Item { Category = Category.Aged };

            //Act
            var strategy = factory.Create(item);

            //Assert
            Assert.IsInstanceOf(typeof(AgedCostPriceStrategy), strategy);
        }

        [Test]
        public void PerishedStrategy_instance()
        {
            //Arrange
            var item = new Item { Category = Category.Perishable };

            //Act
            var strategy = factory.Create(item);

            //Assert
            Assert.IsInstanceOf(typeof(PerishableCostPriceStrategy), strategy);
        }
    }
}
