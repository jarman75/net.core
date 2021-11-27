using Api.Store.Domain;
using Api.Store.Domain.Factory;
using Api.Store.Domain.Strategies;
using NUnit.Framework;

namespace Test.Store.Domain.Factory
{
    public class PriceStrategyFactoryTests
    {
        private IPriceStrategyFactory factory = new PriceStrategyFactory();
        
        [Test]
        public void NormalStrategy_instance()
        {
            //Arrange
            var item = new Item { Category = Category.Normal };

            //Act
            var strategy = factory.Create(item);

            //Assert
            Assert.IsInstanceOf(typeof(NormalPriceStrategy), strategy);
        }

        [Test]
        public void AgedStrategy_instance()
        {
            //Arrange
            var item = new Item { Category = Category.Aged };

            //Act
            var strategy = factory.Create(item);

            //Assert
            Assert.IsInstanceOf(typeof(AgedPriceStrategy), strategy);
        }

        [Test]
        public void PerishedStrategy_instance()
        {
            //Arrange
            var item = new Item { Category = Category.Perishable };

            //Act
            var strategy = factory.Create(item);

            //Assert
            Assert.IsInstanceOf(typeof(PerishablePriceStrategy), strategy);
        }
    }
}
