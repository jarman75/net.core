using Api.Store.Domain;
using Api.Store.Domain.Strategies;
using NUnit.Framework;
using System;

namespace Test.Store.Domain.Strategies
{
    public class AgedPriceStrategyTests
    {
        private IPriceStrategy strategy;

        [SetUp]
        public void Setup()
        {
            strategy = new AgedPriceStrategy();
        }

        [Test]
        public void ComputeYears_between_2_4()
        {
            //Arrange
            var stock = new ItemStock { Price = 100, ManufacturingDate = DateTime.Now.AddYears(-4), Entrydate = DateTime.Now.AddYears(-1) };

            //Act
            var newPrice = strategy.CalculateCostPrice(stock);

            //Assert
            Assert.AreEqual(105d, newPrice, 0.02d);
        }

        [Test]
        public void ComputeYears_between_5_9()
        {
            //Arrange
            var stock = new ItemStock { Price = 100, ManufacturingDate = DateTime.Now.AddYears(-9), Entrydate = DateTime.Now.AddYears(-1) };

            //Act
            var newPrice = strategy.CalculateCostPrice(stock);

            //Assert
            Assert.AreEqual(110d, newPrice, 0.02d);
        }

        [Test]
        public void ComputeYears_greaterEqual10()
        {
            //Arrange
            var stock = new ItemStock { Price = 100, ManufacturingDate = DateTime.Now.AddYears(-12), Entrydate = DateTime.Now.AddYears(-1) };

            //Act
            var newPrice = strategy.CalculateCostPrice(stock);

            //Assert
            Assert.AreEqual(111d, newPrice, 0.02d);
        }

        [Test]
        public void No_requiredDate()
        {
            //Arrange
            var stock = new ItemStock { Price = 100, ManufacturingDate = null, Entrydate = DateTime.Now.AddYears(-1) };

            //Act
            var newPrice = strategy.CalculateCostPrice(stock);

            //Assert
            Assert.AreEqual(100d, newPrice, 0.02d);
        }

        [Test]
        public void No_required_ComputeYears()
        {
            //Arrange
            var stock = new ItemStock { Price = 100, ManufacturingDate = DateTime.Now.AddYears(-2), Entrydate = DateTime.Now.AddYears(-1) };

            //Act
            var newPrice = strategy.CalculateCostPrice(stock);

            //Assert
            Assert.AreEqual(100d, newPrice, 0.02d);
        }



    }
}
