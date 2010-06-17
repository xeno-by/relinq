using System;
using Microsoft.JScript;
using Microsoft.JScript.Vsa;
using Convert=Microsoft.JScript.Convert;

namespace Playground.JSEvaluator
{
    [Serializable]
    public class Evaluator : INeedEngine
    {
        // Fields
        [NonSerialized]
        private VsaEngine vsaEngine;

        [JSFunction(JSFunctionAttributeEnum.HasStackFrame)]
        public virtual string Eval(string expr, bool allowUnsafe)
        {
            string str = null;
            JSLocalField[] fields = new JSLocalField[] { new JSLocalField("expr", typeof(string).TypeHandle, 0), new JSLocalField("return value", typeof(string).TypeHandle, 1) };
            StackFrame.PushStackFrameForMethod(this, fields, ((INeedEngine) this).GetEngine());
            try
            {
                ((StackFrame) ((INeedEngine) this).GetEngine().ScriptObjectStackTop()).localVars[0] = expr;
                ((StackFrame) ((INeedEngine) this).GetEngine().ScriptObjectStackTop()).localVars[1] = str;
                var localVars = ((StackFrame) ((INeedEngine) this).GetEngine().ScriptObjectStackTop()).localVars;
                expr = Convert.ToString(((StackFrame) ((INeedEngine) this).GetEngine().ScriptObjectStackTop()).localVars[0], true);
                str = Convert.ToString(((StackFrame) ((INeedEngine) this).GetEngine().ScriptObjectStackTop()).localVars[1], true);
                var objArray2 = ((StackFrame) ((INeedEngine) this).GetEngine().ScriptObjectStackTop()).localVars;
                str = Convert.ToString(Microsoft.JScript.Eval.JScriptEvaluate(expr, allowUnsafe ? "unsafe" : null, ((INeedEngine) this).GetEngine()), true);
                ((StackFrame) ((INeedEngine) this).GetEngine().ScriptObjectStackTop()).localVars[0] = expr;
                ((StackFrame) ((INeedEngine) this).GetEngine().ScriptObjectStackTop()).localVars[1] = str;
                var objArray3 = ((StackFrame) ((INeedEngine) this).GetEngine().ScriptObjectStackTop()).localVars;
            }
            finally
            {
                ((INeedEngine) this).GetEngine().PopScriptObject();
            }
            return str;
        }

        VsaEngine INeedEngine.GetEngine()
        {
            if (this.vsaEngine == null)
            {
                this.vsaEngine = VsaEngine.CreateEngineWithType(typeof(Playground.JSEvaluator.Evaluator).TypeHandle);
            }
            return this.vsaEngine;
        }

        void INeedEngine.SetEngine(VsaEngine engine1)
        {
            this.vsaEngine = engine1;
        }
    }
}
