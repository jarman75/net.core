using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace functional
{
    public class Exercises01Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestNegate()
        {            

            Func<int,int,bool> esMayor = (arg1,arg2) => (arg1>arg2); 
            Assert.IsTrue(esMayor(10,1));
            
            var negacion = esMayor.Negate();
            Assert.IsFalse(negacion(10,1));   
            
        }  

        [Test]
        public void TestQuickSort()
        {
            var list = new List<int> {-100, 63, 30, 45, 1, 1000, -23, -67, 1, 2, 56, 75, 975, 432, -600, 193, 85, 12};
            var expected = new List<int> {-600, -100, -67, -23, 1, 1, 2, 12, 30, 45, 56, 63, 75, 85, 193, 432, 975, 1000};
            var actual = list.QuickSort();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestShortName() {
            var name = new ShortName(new string('5',50));
            Assert.AreEqual(new string('5',5),name.ToString());            
        }
        
    }

    static class funciones { 
        public static Func<T1,T2,bool> Negate<T1,T2>(this Func<T1,T2,bool> f) 
        => (t1,t2) => !f(t1,t2);
        public static List<int> QuickSort(this List<int> lista) 
        { 
            if (!lista.Any()) return new List<int>();

            var pivot = lista[0];
            var rest = lista.Skip(1);
            var small = from item in rest where item <= pivot select item;
            var large = from item in rest where pivot < item select item;

            return small.ToList().QuickSort()
                .Append(pivot)
                .Concat(large.ToList().QuickSort())
                .ToList();
        }

        public static List<T> QuickSort<T>(this List<T> lista, Comparison<T> compare) 
        { 
            if (!lista.Any()) return new List<T>();

            var pivot = lista[0];
            var rest = lista.Skip(1);
            var small = from item in rest where compare(item,pivot) <= 0 select item;
            var large = from item in rest where 0 < compare(item,pivot) select item;

            return small.ToList().QuickSort(compare)
                .Append(pivot)
                .Concat(large.ToList().QuickSort(compare))
                .ToList();
        }

        public static R Using<TDisp, R>(Func<TDisp> createDisposable
         , Func<TDisp, R> func) where TDisp : IDisposable
        {
            using (var disp = createDisposable()) return func(disp);
        }
    }

    public struct ShortName : IEquatable<ShortName>
    {
        
        public ShortName(string value) {
            _value = value;
        }
        private string _value;        

        public bool Equals([AllowNull] ShortName other) 
        => other.ToString().Equals(this.ToString());

        public override int GetHashCode() 
        => HashCode.Combine(_value);

        public override string ToString()
        {
            return _value.Substring(0,5);
        }
    }
}