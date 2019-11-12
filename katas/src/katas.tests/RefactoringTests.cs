using System;
using System.Reflection;
using NUnit.Framework;

namespace katas.tests
{

    public abstract class Status
    {
        public abstract string GetStatusDescription();            
        
    }
    public class NotSetStatus : Status
    {
        public override string GetStatusDescription()
        {
            return "I have never been set";
        }
    }
    public class NewStatus : Status
    {
        public override string GetStatusDescription()
        {
            return "I am new!";
        }
    }
    public class ActiveStatus : Status
    {
        public override string GetStatusDescription()
        {
            return "I am active";
        }
    }
    public class DeactivatedStatus : Status
    {
        public override string GetStatusDescription()
        {
            return "I have been deactivated";
        }
    }
    public class Kata {
        private Status _status;

        public Kata (Status status) {
            _status = status;
        }
        public Kata () {
            _status = new NotSetStatus();
        }
        public string GetStatusDescription() {            
            return _status.GetStatusDescription();
        }
    }





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