﻿namespace NRules.Rete
{
    internal interface ITerminalNode
    {
        void Attach(IRuleNode ruleNode);
    }

    internal class TerminalNode : ITerminalNode, ITupleSink
    {
        private readonly FactIndexMap _factIndexMap;
        private IRuleNode _ruleNode;

        public FactIndexMap FactIndexMap
        {
            get { return _factIndexMap; }
        }

        public IRuleNode RuleNode
        {
            get { return _ruleNode; }
        }

        public TerminalNode(ITupleSource source, FactIndexMap factIndexMap)
        {
            _factIndexMap = factIndexMap;
            source.Attach(this);
        }

        public void PropagateAssert(IExecutionContext context, Tuple tuple)
        {
            RuleNode.Activate(context, tuple, _factIndexMap);
        }

        public void PropagateUpdate(IExecutionContext context, Tuple tuple)
        {
            //Do nothing
        }

        public void PropagateRetract(IExecutionContext context, Tuple tuple)
        {
            RuleNode.Deactivate(context, tuple, _factIndexMap);
        }

        public void Attach(IRuleNode ruleNode)
        {
            _ruleNode = ruleNode;
        }

        public void Accept<TContext>(TContext context, ReteNodeVisitor<TContext> visitor)
        {
            visitor.VisitTerminalNode(context, this);
        }
    }
}