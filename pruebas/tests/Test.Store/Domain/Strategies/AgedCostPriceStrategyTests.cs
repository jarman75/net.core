using Api.Store.Domain;
using Api.Store.Domain.Strategies;
using NUnit.Framework;
using System;

namespace Test.Store.Domain.Strategies
{
    public class AgedCostPriceStrategyTests
    {
        private ICostPriceStrategy strategy;

        [SetUp]
        public void Setup()
        {
            strategy = new AgedCostPriceStrategy();
        }

        [Test]
        public void ComputeYears_between_2_4()
        {
            //Arrange
            var stock = new ItemStock { CostPrice = 100, ManufacturingDate = DateTime.Now.AddYears(-4), Entrydate = DateTime.Now.AddYears(-1) };

            //Act
            var newCost = strategy.CalculateCostPrice(stock);

            //Assert
            Assert.AreEqual(105d, newCost, 0.02d);
        }

        [Test]
        public void ComputeYears_between_5_9()
        {
            //Arrange
            var stock = new ItemStock { CostPrice = 100, ManufacturingDate = DateTime.Now.AddYears(-9), Entrydate = DateTime.Now.AddYears(-1) };

            //Act
            var newCost = strategy.CalculateCostPrice(stock);

            //Assert
            Assert.AreEqual(110d, newCost, 0.02d);
        }

        [Test]
        public void ComputeYears_greaterEqual10()
        {
            //Arrange
            var stock = new ItemStock { CostPrice = 100, ManufacturingDate = DateTime.Now.AddYears(-12), Entrydate = DateTime.Now.AddYears(-1) };

            //Act
            var newCost = strategy.CalculateCostPrice(stock);

            //Assert
            Assert.AreEqual(111d, newCost, 0.02d);
        }

        [Test]
        public void No_requiredDate()
        {
            //Arrange
            var stock = new ItemStock { CostPrice = 100, ManufacturingDate = null, Entrydate = DateTime.Now.AddYears(-1) };

            //Act
            var newCost = strategy.CalculateCostPrice(stock);

            //Assert
            Assert.AreEqual(100d, newCost, 0.02d);
        }

        [Test]
        public void No_required_ComputeYears()
        {
            //Arrange
            var stock = new ItemStock { CostPrice = 100, ManufacturingDate = DateTime.Now.AddYears(-2), Entrydate = DateTime.Now.AddYears(-1) };

            //Act
            var newCost = strategy.CalculateCostPrice(stock);

            //Assert
            Assert.AreEqual(100d, newCost, 0.02d);
        }



    }
}
