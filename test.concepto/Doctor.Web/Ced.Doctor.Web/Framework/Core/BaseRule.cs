using Framework.Core.Interfaces;
using System;

namespace Framework.Core
{
    /// <summary>
    /// BaseRuel. Evaluable rule.
    /// </summary>
    /// <seealso cref="Framework.Core.Interfaces.IRule" />
    public abstract class BaseRule : IRule
    {
        private readonly Action actionToBeExecuted;
        protected readonly IRuleResult ruleResult;

        protected BaseRule(Action actionToBeExecuted)
        {
            this.actionToBeExecuted = actionToBeExecuted;
            this.ruleResult = new RuleResult(this.actionToBeExecuted);
        }

        protected BaseRule()
        {
            this.ruleResult = new RuleResult();
        }

        public abstract IRuleResult Eval();
    }
}
