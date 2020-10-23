using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;

namespace uspEF.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            
            Assert.Pass();
        }
    }

    public class TestContext : DbContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer("Data Source=office.mindden.com,33406;Initial Catalog=Enerlogy;Persist Security Info=True;User ID=sa;Password=Mindden2017");
            
        }
    }
}