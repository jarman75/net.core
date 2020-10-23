using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Exercises.Chapter1 {
    public static class Examples 
    {

        class Cache<T> where T: class {
            public T Get(int id) => null;
            public T Get(int id, Func<T> onMiss) => Get(id) ?? onMiss();
        }
        class bus {
            public int id {get; set;}
            public string name {get; set;}
        }

        [Test]
        public static void TestCache(){
            var cache = new Cache<bus>();
           
            var bus = new bus{id=0,name="no hay"};
            Func<bus> onMiss = () => bus;
            
                     
            
            var data = cache.Get(28, onMiss); 
            Assert.AreEqual(bus,data);
            
        }

    }

    

}