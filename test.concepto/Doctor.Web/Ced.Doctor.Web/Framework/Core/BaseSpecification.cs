using Framework.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.Core
{
    /// <summary>
    /// BaseSpecification. Specify evaluable criteria.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Framework.Core.Interfaces.ISpecification{T}" />
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSpecification{T}"/> class.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        protected BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        /// <summary>
        /// Gets the criteria.
        /// </summary>
        /// <value>
        /// The criteria.
        /// </value>
        public Expression<Func<T, bool>> Criteria { get; }

        /// <summary>
        /// Gets the includes.
        /// </summary>
        /// <value>
        /// The includes.
        /// </value>
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        /// <summary>
        /// Gets the include string.
        /// </summary>
        /// <value>
        /// The include string.
        /// </value>
        public List<string> IncludeStrings { get; } = new List<string>();

        /// <summary>
        /// Adds the include.
        /// </summary>
        /// <param name="includeExpression">The include expression.</param>
        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
    }
}
