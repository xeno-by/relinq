using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Relinq.Script.Semantics.Lifting
{
    // Source code shamelessly copy/pasted from http://www.codeplex.com/interlinq
    // todo: rewrite this stuff by my own hands
    public class DynamicAssemblyHolder
    {
        private AssemblyBuilder _assembly;
        private static DynamicAssemblyHolder _current;
        private ModuleBuilder _module;

        private DynamicAssemblyHolder()
        {
        }

        private void Initialize()
        {
            var currentDomain = AppDomain.CurrentDomain;
            var name = new AssemblyName{Name = "Relinq.Signatures"};
#if DEBUG
            _assembly = currentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndSave);
            _module = _assembly.GetDynamicModule("Relinq.Signatures.Module") ??
                _assembly.DefineDynamicModule("Relinq.Signatures.Module", "signatures.dll");
#else
            _assembly = currentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run);
            _module = _assembly.GetDynamicModule("Relinq.Signatures.Module") ??
                _assembly.DefineDynamicModule("Relinq.Signatures.Module");
#endif
        }

        public static DynamicAssemblyHolder Current
        {
            get
            {
                if (_current == null)
                {
                    lock (typeof(DynamicAssemblyHolder))
                    {
                        if (_current == null)
                        {
                            _current = new DynamicAssemblyHolder();
                            _current.Initialize();
                        }
                    }
                }

                return _current;
            }
        }

        public ModuleBuilder ModuleBuilder
        {
            get
            {
                return _module;
            }
        }
    }
}
