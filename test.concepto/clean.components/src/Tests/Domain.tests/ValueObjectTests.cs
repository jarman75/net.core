using Domain.ValueObjects;
using NUnit.Framework;
using System;



namespace Domain.tests
{
    public class ValueObjectTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_shortName()
        {

            var ex = Assert.Throws<ShortNameValidationException>(() => new ShortName(""));
            Assert.That(ex.Message == "The 'Name' is required.", ex.Message);

            ex = Assert.Throws<ShortNameValidationException>(() => new ShortName(new String('X', 16)));
            Assert.That(ex.Message == "The 'Name' supports a maximum of 15 characters.", ex.Message);


            //correct names comparer
            var name1 = new ShortName("Manuel");
            var name2 = new ShortName("Manuel");

            Assert.IsTrue(name1.ToString() == "Manuel");
            Assert.AreEqual(name1, name2);
        }

    }



}