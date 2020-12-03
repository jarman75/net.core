using Framework.Core.Interfaces;
using System;

namespace Framework.Core
{
    /// <summary>
    /// RuleResult. Rule Result.
    /// </summary>
    /// <seealso cref="Framework.Core.Interfaces.IRuleResult" />
    public class RuleResult : IRuleResult
    {
        private readonly Action ActionToBeExecuted;

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleResult"/> class.
        /// </summary>
        public RuleResult()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleResult"/> class.
        /// </summary>
        /// <param name="actionToBeExecuted">The action to be executed.</param>
        public RuleResult(Action actionToBeExecuted)
        {
            this.ActionToBeExecuted = actionToBeExecuted;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is sucess.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is sucess; otherwise, <c>false</c>.
        /// </value>
        public bool IsSucess { get; set; }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public void Execute()
        {
            if (IsSucess)
            {
                ActionToBeExecuted.Invoke();
            }
        }
    }
}
