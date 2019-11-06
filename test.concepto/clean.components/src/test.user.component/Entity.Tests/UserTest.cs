using System.Reflection;
using System.ComponentModel;
using NUnit.Framework;
using System;
using user.component.Entities;


namespace test.user.component.Entity.Tests
{
    public class UserTest
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test_name_required()
        {            
            User user = new User();           
           
            // the exception we expect thrown from the User.Save() method
            var ex = Assert.Throws<ArgumentException>(() => user.Save());

            // now we can test the exception itself
            Assert.That(ex.Message == "Required value. (Parameter 'Name')", ex.Message);
        }
        [Test]
        public void Test_email_required()
        {            
            User user = new User();  
            user.Name = "testname";         
           
            // the exception we expect thrown from the User.Save() method
            var ex = Assert.Throws<ArgumentException>(() => user.Save());

            // now we can test the exception itself
            Assert.That(ex.Message == "Required value. (Parameter 'Email')", ex.Message);
        }
        [Test]
        public void Test_rol_required()
        {            
            User user = new User();    
            user.Name = "testname";
            user.Email = "testmail@mail.com";
           
            // the exception we expect thrown from the User.Save() method
            var ex = Assert.Throws<ArgumentNullException>(() => user.Save());

            // now we can test the exception itself
            Assert.That(ex.Message == "Value cannot be null. (Parameter 'Role')", ex.Message);
        }

         [Test]
        public void Test_rol_required_name()
        {            
            User user = new User();    
            user.Name = "testname";
            user.Email = "testmail@mail.com";     
            user.Role = new Role();  
                       
            // the exception we expect thrown from the User.Save() method
            var ex = Assert.Throws<ArgumentException>(() => user.Save());

            // now we can test the exception itself
            Assert.That(ex.Message == "Required value. (Parameter 'Role')", ex.Message);
        }

         [Test]
        public void Test_user_save_ok()
        {            
            User user = new User();    
            user.Name = "testname";
            user.Email = "testmail@mail.com";     
            user.Role = new Role();  
            user.Role.Name = "testRole";            
            Assert.Pass();
        }

        [Test]
        public void Test_validate_password_min_lenght()
        {
            User user = new User();
            
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => user.SetPassword("1A"));
            Assert.That(ex.Message == "Specified argument was out of the range of valid values. (Parameter 'Password must include at least 8 chars.')", ex.Message);
            

        }
    }

    
    
}