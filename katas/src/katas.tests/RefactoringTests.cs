using System;
using NUnit.Framework;

namespace katas.tests
{
    /*
    
    Refactorizar clase Kata para quitar el switch. 
    Cambiar enum 'Status' por:
    
    public abstract class Status
    {
        public abstract string GetStatusDescription();
    }

    El código de los test no se puede modificar

    */

     public class Kata
    {
        public enum Status
        {
            Default = 0,
            New = 1,
            Active = 2,
            Deactivated = 3
        }
    
        private readonly Status _status;

        public Kata()
        {
        }

        public Kata(Status status)
        {
            _status = status;
        }

        public string GetStatusDescription()
        {
            switch (_status)
            {
                case Status.Default:
                    return "I have never been set";

                case Status.New:
                    return "I am new!";

                case Status.Active:
                    return "I am active";

                case Status.Deactivated:
                    return "I have been deactivated";

                default:
                    throw new InvalidOperationException("Invalid status encountered");
            }
        }
    }

    /***************************************************************
                TESTS
    ***************************************************************/
    public class RefacotingTests
    {
        [Test]
        public void StatusCorrect()
        {
            Assert.AreEqual("I have never been set", new Kata().GetStatusDescription());
            Assert.AreEqual("I am new!", new Kata(new NewStatus()).GetStatusDescription());
            Assert.AreEqual("I am active", new Kata(new ActiveStatus()).GetStatusDescription());
            Assert.AreEqual("I have been deactivated", new Kata(new DeactivatedStatus()).GetStatusDescription());
        }

        [Test]
        public void HasStatusField()
        {
            Type type = typeof(Kata);
            FieldInfo field = type.GetField("_status", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(field);
            Type fieldType = field.FieldType;
            Assert.AreEqual(fieldType, typeof(Status));    
        }
    }
}