using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Relinq.V2.Helpers.Debug;
using Relinq.V2.RelinqScript.Compiler2.TypeInference;

namespace Relinq.V2.RelinqScript.Compiler2.Expressions
{
    public abstract class TypeInferenceUnit : DebuggableObject
    {
        public ExpressionType NodeType { get; private set; }
        public FuzzyType Type { get; private set; }

        protected virtual FuzzyType OnInitializeMainTypePoint() { return FuzzyType.Unknown(); }
        protected virtual void OnInitializeTypeLinks() { }

        public int ChildIndex { get; private set; }
        public TypeInferenceUnit Parent { get; private set; }
        public IEnumerable<TypeInferenceUnit> Children { get; private set; }

        protected TypeInferenceUnit(ExpressionType nodeType, IEnumerable<TypeInferenceUnit> children)
        {
            NodeType = nodeType;
            Children = new List<TypeInferenceUnit>(children).AsReadOnly();

            var childIndex = 0;
            foreach (var child in Children)
            {
                child.Parent = this;
                child.RegDebuggableParent(this);
                child.ChildIndex = childIndex++;
            }
        }

        #region Initialization Stage 1: Creation of the public type point and arbitrary number of internal ones

        protected bool TypePointsSetInStone = false;

        public void InitializeMainTypePoint()
        {
            if (!TypePointsSetInStone)
            {
                foreach (var child in Children.Cast<TypeInferenceUnit>())
                {
                    child.InitializeMainTypePoint();
                }

                try
                {
                    var slot = Thread.GetNamedDataSlot("TypeInferenceUnit");
                    Thread.SetData(slot, this);

                    Type = OnInitializeMainTypePoint();
                    TypePointsSetInStone = true;
                }
                finally
                {
                    Thread.FreeNamedDataSlot("TypeInferenceUnit");
                }
            }
            else
            {
                throw new ArgumentException("Cannot initialize type points twice");
            }
        }

        public static TypeInferenceUnit Current
        {
            get
            {
                var slot = Thread.GetNamedDataSlot("TypeInferenceUnit");
                return (TypeInferenceUnit)Thread.GetData(slot);
            }
        }

        #endregion

        #region Initialization Stage 2: Creation of type links: internal and external

        protected bool TypeLinksSetInStone = false;

        public void InitializeTypeLinks()
        {
            if (!TypeLinksSetInStone)
            {
                foreach (var child in Children.Cast<TypeInferenceUnit>())
                {
                    child.InitializeTypeLinks();
                }

                OnInitializeTypeLinks();
                TypeLinksSetInStone = true;
            }
            else
            {
                throw new ArgumentException("Cannot initialize type links twice");
            }
        }

        #endregion

        public virtual bool IsFullyInferred
        {
            get
            {
                return
                    Type.IsFullyInferred &&
                    Children.All(child => child.IsFullyInferred);
            }
        }
        
        public virtual Dictionary<String, LambdaExpression> Closure
        {
            get
            {
                return new Dictionary<String, LambdaExpression>(
                    Parent == null ? new Dictionary<string, LambdaExpression>() : Parent.Closure);
            }
        }

        protected abstract String ContentToString();

        public override string ToString()
        {
            var stack = new Stack<String>();
            for (var current = this; current != null; current = current.Parent)
            {
                var nodeStr = current.ContentToString();
                if (current.Parent is ConditionalExpression)
                {
                    switch (current.ChildIndex)
                    {
                        case 0:
                            nodeStr = "?:" + nodeStr;
                            break;
                        case 1:
                            nodeStr = "0:" + nodeStr;
                            break;
                        case 2:
                            nodeStr = "1:" + nodeStr;
                            break;
                        default:
                            throw new NotSupportedException(current.ChildIndex.ToString());
                    }
                }

                stack.Push("/" + nodeStr);
            }

            var sb = new StringBuilder();
            while (stack.Count != 0) sb.Append(stack.Pop());
            return sb.ToString();
        }
    }
}