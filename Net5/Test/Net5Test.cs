using System.Reflection;
using NUnit.Framework;

namespace Test
{
    public partial class Net5Test
    {
        
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

        [Test]
        public void Test_clone() {
            var original = new Clonable(){
                Id=1, Name="Nombre01", 
                Pets = new System.Collections.Generic.List<Animal>{
                    new Animal("Rufo", 7),
                    new Animal("Marlene", 15)
                }
                };
            
            var clone = original.DeepClone();
            clone.Name = "Nombre02";
            clone.Id = 2;
            clone.Pets.RemoveAt(0);
            clone.Pets[0] = new Animal("Lucas",17);

            Assert.AreNotEqual(original.Id, clone.Id);
        
        }
    }
}