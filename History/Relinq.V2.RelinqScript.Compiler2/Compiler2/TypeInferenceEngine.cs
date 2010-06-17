using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Relinq.V2.Helpers;
using Relinq.V2.Helpers.Debug;
using Relinq.V2.RelinqScript.Compiler2.Expressions;
using Relinq.V2.RelinqScript.Compiler2.TypeInference;
using Relinq.V2.RelinqScript.Compiler2.TypeInference.TypeLinks;
using System.Linq;

namespace Relinq.V2.RelinqScript.Compiler2
{
    public class TypeInferenceEngine : IDisposable
    {
        private TypeInferenceUnit _ast = null;
        private List<TypeLink> _links = new List<TypeLink>();

        private bool _syncsAllowed = true;
        private Queue<Sync> _queue = new Queue<Sync>();

        private class Sync
        {
            public TypeLink Link { get; private set; }
            public FuzzyMetadata Sender { get; private set; }

            public Sync(TypeLink link, FuzzyMetadata sender)
            {
                Link = link;
                Sender = sender;
            }
        }

        public TypeInferenceEngine()
        {
            var slot = Thread.GetNamedDataSlot("TypeInferenceEngine");
            Thread.SetData(slot, this);
        }

        public TypeInferenceEngine(TypeInferenceUnit ast)
            :this()
        {
            BindToAst(ast);
        }

        private void BindToAst(TypeInferenceUnit ast)
        {
            try
            {
                _ast = ast;
                _ast.InitializeMainTypePoint();
                _ast.InitializeTypeLinks();
            }
            catch (Exception)
            {
#if DEBUG
                Dump();
#endif
                Dispose();
                throw;
            }
        }

        public void Dispose()
        {
            Thread.FreeNamedDataSlot("TypeInferenceEngine");
        }

        public static TypeInferenceEngine Current
        {
            get
            {
                var slot = Thread.GetNamedDataSlot("TypeInferenceEngine");
                return (TypeInferenceEngine)Thread.GetData(slot);
            }
        }

        public void RegisterLink(TypeLink link)
        {
            link.ContradictionReported += (o, e) => ProcessContradiction(o, e.WhatHappened);
            _links.Add(link);
        }

        public void UnregisterLink(TypeLink link)
        {
            _links.Remove(link);
            link.DisposeDebuggableObject();
        }

        public void Dispose(FuzzyMetadata fuzzy)
        {
            // bug: rewrite this ffs!
#if DEBUG
            foreach (var fuzzyChild in fuzzy.DebuggableChildren.OfType<FuzzyMetadata>())
            {
                Dispose(fuzzyChild);
            }
#else
            cba to rewrite this now, so you'll have to do it when you try to compile
            this craptastic stuff in release mode. I'm sorry tbh, but I have no time
#endif

            foreach (var link in _links.ToArray())
            {
                if (link.First == fuzzy || link.Second == fuzzy)
                {
                    UnregisterLink(link);
                }
            }

            fuzzy.DisposeDebuggableObject();
        }

        public bool Run()
        {
            // After the type inference AST has been initialized we need to introduce initial motion 
            // be forcibly syncing all type links existing.
            //
            // Right after creation AST nodes already possess some information about types.
            // Keywords have fully inferred type upon initialization, and lambdas at least partially 
            // get an idea about their type since they know the number of arguments.
            //
            // Syncing partially/fully inferred types with totally unknown ones will start 
            // a chain reaction of updates and subsequent syncs that will hopefully lead to 
            // the entire AST to be fully inferred.

            EngageTheEngine();

            while (!RunQueuedSyncs())
            {
                // If engaging type inference engine didn't help to fully infer all types, 
                // we need to bruteforce method alternatives.
                //
                // Of course it might appear that we even don't have alternatives list because
                // even target's basis type ain't inferred yet... Wow... that's a real failboat then.

                // fuck this for now
//                throw new NotImplementedException();

                return false;
            }

            return true;
        }

        public void EngageTheEngine()
        {
            _links.ForEach(link => link.Sync());
        }

        private bool RunQueuedSyncs()
        {
            while (!_queue.IsEmpty())
            {
                var nextSync = _queue.Dequeue();
                if (_links.Contains(nextSync.Link))
                {
                    nextSync.Link.Sync(nextSync.Sender);
                }
            }

            return _ast.IsFullyInferred;
        }

        public void ScheduleSync(TypeLink link, FuzzyMetadata sender)
        {
            if (_syncsAllowed)
            {
                _queue.Enqueue(new Sync(link, sender));
            }
        }

        private void ProcessContradiction(object sender, String message)
        {
            // this behavior will change once i get a grasp on idea of iterating through alternatives
            throw new ArgumentException(String.Format(
                "'{0}' reports: '{1}'",
                sender,
                message));
        }

#if DEBUG

        public void Dump()
        {
            DebugHelper.DumpAll();
            if (_ast != null) DumpAst(_ast);
            if (_ast != null) DumpEngine();
        }

        private void DumpAst(TypeInferenceUnit root)
        {
            if (Directory.Exists(@"d:\dumps\"))
            {
                using (var sw = new StreamWriter(@"d:\dumps\ast-" + Guid.NewGuid() + ".dump"))
                {
                    DumpAst(root, sw, String.Empty);
                }
            }
        }

        private void DumpAst(TypeInferenceUnit root, StreamWriter sw, String tab)
        {
            sw.Write(tab);
            root.Dump(sw);

            foreach (var child in root.Children)
            {
                DumpAst(child, sw, tab + "\t");
            }
        }

        private void DumpEngine()
        {
            if (Directory.Exists(@"d:\dumps\"))
            {
                using (var sw = new StreamWriter(@"d:\dumps\engine-" + Guid.NewGuid() + ".dump"))
                {
                    var flattenedAst = new List<TypeInferenceUnit>();
                    FlattenAst(flattenedAst, _ast);

                    foreach (var astNode in flattenedAst)
                    {
                        DumpDebuggableChildren(astNode, sw, String.Empty);
                        sw.WriteLine();
                    }
                }
            } 
        }

        private void FlattenAst(List<TypeInferenceUnit> list, TypeInferenceUnit root)
        {
            list.Add(root);
            root.Children.ForEach(child => FlattenAst(list, child));
        }

        private void DumpDebuggableChildren(IDebuggableObject @do, StreamWriter sw, String tab)
        {
            sw.WriteLine(tab + @do);

            foreach (var child in @do.DebuggableChildren)
            {
                if (@do is TypeInferenceUnit)
                {
                    if (child is FuzzyMetadata)
                    {
                        if (!child.DebuggableParents.Any(parent => parent is FuzzyMetadata))
                        {
                            DumpDebuggableChildren(child, sw, tab + "\t");
                        }
                    }
                }

                if (@do is FuzzyMetadata)
                {
                    DumpDebuggableChildren(child, sw, tab + "\t");
                }
            }

            sw.WriteLine();
        }

#endif

    }
}
