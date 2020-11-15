using System.Reflection;
using NUnit.Framework;

namespace Test
{
    public class Net5Test
    {
        public record Person(string FirstName, string LastName);    
        
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
    }
}