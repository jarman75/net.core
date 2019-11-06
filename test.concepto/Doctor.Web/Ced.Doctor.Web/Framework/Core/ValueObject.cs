using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Framework.Core
{
    /// <summary>
    /// ValueObject. 
    /// Value objects are immutable. Don't use public setters and passing data through the constructors.
    /// Value objects are copied by value (cloned): hence the expression valueObject1 = valueObject2 makes a copy of the values of the attributes of valueObject2 to those of valueObject1.
    /// </summary>
    /// <seealso cref="System.IEquatable{MinddenComponent.Core.Common.ValueObject}" />
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        private List<PropertyInfo> properties;
        private List<FieldInfo> fields;

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="obj1">The obj1.</param>
        /// <param name="obj2">The obj2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(ValueObject obj1, ValueObject obj2)
        {
            if (object.Equals(obj1, null))
            {
                return object.Equals(obj2, null);
            }
            return obj1.Equals(obj2);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="obj1">The obj1.</param>
        /// <param name="obj2">The obj2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(ValueObject obj1, ValueObject obj2)
        {
            return !(obj1 == obj2);
        }

        /// <summary>
        /// Equalses the specified object.
        /// </summary>
        /// <param name="other">The object.</param>
        /// <returns></returns>
        public bool Equals(ValueObject other)
        {
            return Equals(other as object);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;

            return GetProperties().All(p => PropertiesAreEqual(obj, p))
                && GetFields().All(f => FieldsAreEqual(obj, f));
        }

        private bool PropertiesAreEqual(object obj, PropertyInfo p)
        {
            return object.Equals(p.GetValue(this, null), p.GetValue(obj, null));
        }

        private bool FieldsAreEqual(object obj, FieldInfo f)
        {
            return object.Equals(f.GetValue(this), f.GetValue(obj));
        }

        private IEnumerable<PropertyInfo> GetProperties()
        {
            return this.properties ?? (this.properties = GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(p => p.GetCustomAttribute(typeof(IgnoreMemberAttribute)) == null)
                    .ToList());
        }

        private IEnumerable<FieldInfo> GetFields()
        {
            return this.fields ?? (this.fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
                    .Where(p => p.GetCustomAttribute(typeof(IgnoreMemberAttribute)) == null)
                    .ToList());
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            unchecked   //allow overflow
            {
                int hash = 17;
                foreach (var prop in GetProperties())
                {
                    var value = prop.GetValue(this, null);
                    hash = HashValue(hash, value);
                }

                foreach (var field in GetFields())
                {
                    var value = field.GetValue(this);
                    hash = HashValue(hash, value);
                }

                return hash;
            }
        }

        private int HashValue(int seed, object value)
        {
            var currentHash = value?.GetHashCode() ?? 0;

            return (seed * 23) + currentHash;
        }
    }
}
