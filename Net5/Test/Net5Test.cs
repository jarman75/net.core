using System.Reflection;
using NUnit.Framework;

namespace Test
{
    public class Net5Test
    {
        public record Person(string FirstName, string LastName);    
        public record Animal(string Name, int Age);
        public record Gato(string Name, int Age) : Animal(Name, Age);
        
        public class AnimalService
        {
            public virtual Animal Create(string nombre, int edad)
            {
                return new(nombre, edad);
            }
        }

        public class GatoService : AnimalService
        {
            public override Gato Create(string nombre, int edad)
            {
                return new(nombre, edad);
            }
        }
        
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_records()
        {   
            
            Person yo = new("Armando", "Martí");                
            var tu = new Person("Armando", "Martí");
            var otro = yo with {LastName = "Sánchez"};
                        
            Assert.AreEqual(yo,tu);
            Assert.AreNotEqual(yo,otro);
        }

        [Test]
        public void Test_covariant_returns()
        {
            AnimalService animalService = new();
            var gatoService = new GatoService();

            var resultAnimal = animalService.Create("Perro", 5);
            var resultGato = gatoService.Create("Gato", 6);

            Assert.IsTrue(resultAnimal is Animal);
            Assert.IsTrue(resultGato is Gato);
        }
    }
}