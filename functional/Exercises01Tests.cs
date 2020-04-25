using NUnit.Framework;
using System;

namespace functional
{
    public class Exercises01Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {            

            Func<int,int,bool> esMayor = (arg1,arg2) => (arg1>arg2); 
            Assert.IsTrue(esMayor(10,1));
            
            var negacion = esMayor.Negate();
            Assert.IsFalse(negacion(10,1));
        }    

        
    }

    static class funciones { 
        public static Func<T1,T2,bool> Negate<T1,T2>(this Func<T1,T2,bool> f) 
        => (t1,t2) => !f(t1,t2); 
    }
}