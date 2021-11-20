using Api.Store.Domain;
using Api.Store.Domain.Strategies;
using NUnit.Framework;
using System;

namespace Test.Store.Domain.Strategies
{
    public class PerishableCostPriceStrategyTests
    {
        private ICostPriceStrategy strategy;

        [SetUp]
        public void Setup()
        {
            strategy = new PerishableCostPriceStrategy();
        }

        [Test]
        public void ExpirationDays_less_than_1()
        {
            //Arrange
            var stock = new ItemStock { CostPrice = 100, ExpirationDate = DateTime.Now };

            //Act
            var newCost = strategy.CalculateCostPrice(stock);

            //Assert
            Assert.AreEqual(0, newCost, 0.02d);
        }
        
        [Test]
        public void ExpirationDays_2()
        {
            //Arrange
            var stock = new ItemStock { CostPrice = 100, ExpirationDate = DateTime.Now.AddDays(2) };

            //Act
            var newCost = strategy.CalculateCostPrice(stock);

            //Assert
            Assert.AreEqual(50, newCost, 0.02d);
        }
        [Test]
        public void ExpirationDays_between_3_and_4()
        {
            //Arrange
            var stock = new ItemStock { CostPrice = 100, ExpirationDate = DateTime.Now.AddDays(4) };

            //Act
            var newCost = strategy.CalculateCostPrice(stock);

            //Assert
            Assert.AreEqual(25, newCost, 0.02d);
        }

        [Test]
        public void ExpirationDays_with_price0()
        {
            //Arrange
            var stock = new ItemStock { CostPrice = 0, ExpirationDate = DateTime.Now.AddDays(2) };

            //Act
            var newCost = strategy.CalculateCostPrice(stock);

            //Assert
            Assert.AreEqual(0, newCost, 0.02d);
        }

        [Test]
        public void Stock_with_no_expiration_date()
        {
            //Arrange
            var stock = new ItemStock { CostPrice = 100, ExpirationDate = null };

            //Act
            var newCost = strategy.CalculateCostPrice(stock);

            //Assert
            Assert.AreEqual(100, newCost, 0.02d);
        }




    }
}
