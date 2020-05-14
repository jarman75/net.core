using NUnit.Framework;
using CommercialRegistration;
using ConsumerVehicleRegistration;
using LiveryRegistration;
using System;

namespace toll_calculator {
    public class TestCalculator {
        
        [Test]
        public void Test(){
            
            var taxi = new Taxi();

            var calculador = new TollCalculator();
            var res = calculador.CalculateToll(taxi);

            Assert.AreEqual(3.50m,res);

            
        }

        [Test]
        public void CalculateTest(){
            var tollCalc = new TollCalculatorPassenger();//new TollCalculator();

            var car = new Car{Passengers=3};
            var taxi = new Taxi{Fares=2};
            var bus = new Bus{Capacity=50, Riders = 49};
            var truck = new DeliveryTruck();

            Console.WriteLine($"The toll for a car is {tollCalc.CalculateToll(car)}");
            Console.WriteLine($"The toll for a taxi is {tollCalc.CalculateToll(taxi)}");
            Console.WriteLine($"The toll for a bus is {tollCalc.CalculateToll(bus)}");
            Console.WriteLine($"The toll for a truck is {tollCalc.CalculateToll(truck)}");

            try
            {
                tollCalc.CalculateToll("this will fail");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Caught an argument exception when using the wrong type");
            }
            try
            {
                tollCalc.CalculateToll(null);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Caught an argument exception when using null");
            }
        }
    }
}