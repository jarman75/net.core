using NUnit.Framework;

namespace katas.tests
{
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