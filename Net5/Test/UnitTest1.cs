using System.Reflection;
using NUnit.Framework;

namespace Test
{
    public class Tests
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
            
            var tu = new Person("Luis", "Sanchez");

            var other = yo with {LastName = "Martí"};
            
            Assert.IsTrue("Armando" == other.FirstName);
        }
    }
}