using System;

namespace Framework.Core
{
    /// <summary>
    /// IgnoreMemberAttribute
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreMemberAttribute : Attribute
    {
    }
}
