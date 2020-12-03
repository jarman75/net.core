using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.Core.Interfaces
{
    /// <summary>
    /// ISpecification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        /// Gets the criteria.
        /// </summary>
        /// <value>
        /// The criteria.
        /// </value>
        Expression<Func<T, bool>> Criteria { get; }

        /// <summary>
        /// Gets the includes.
        /// </summary>
        /// <value>
        /// The includes.
        /// </value>
        List<Expression<Func<T, object>>> Includes { get; }

        /// <summary>
        /// Gets the include string.
        /// </summary>
        /// <value>
        /// The include string.
        /// </value>
        List<string> IncludeStrings { get; }

    }
}
