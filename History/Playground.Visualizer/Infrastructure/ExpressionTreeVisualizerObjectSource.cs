using System;
using System.IO;
using System.Linq.Expressions;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace Playground.Visualizer.Infrastructure
{
    public class ExpressionTreeVisualizerObjectSource : VisualizerObjectSource
    {
        public override void GetData(object target, Stream outgoingData)
        {
            Expression expr = (Expression)target;
            ExpressionTreeNode browser = new ExpressionTreeNode(expr);
            ExpressionTreeContainer container = new ExpressionTreeContainer(browser, expr.ToString());

            Serialize(outgoingData, container);
        }
    }

    [Serializable]
    public class ExpressionTreeContainer
    {
        public ExpressionTreeContainer(ExpressionTreeNode tree, string expression)
        {
            this.tree = tree;
            this.expression = expression;

        }
        private ExpressionTreeNode tree;

        public ExpressionTreeNode Tree
        {
            get { return tree; }
            set { tree = value; }
        }
        private string expression;

        public string Expression
        {
            get { return expression; }
            set { expression = value; }
        }
    }
}