using System;
using System.IO;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using Relinq.DSL;

namespace JavaScript
{
    class Program
    {
        public static void Main(string[] args)
        {
            var lex = new RelinqScriptLexer(new ANTLRFileStream("Sample.js"));
            var tokens = new CommonTokenStream(lex);

            var parser = new RelinqScriptParser(tokens);
            var expression = parser.expression();
            DumpTree((BaseTree)expression.Tree);
        }

        private static void DumpTree(BaseTree tree)
        {
            if (!Directory.Exists(@"d:\ast-dumps\"))
                return;

            using(var sw = new StreamWriter(@"d:\ast-dumps\"+ Guid.NewGuid() + ".dump"))
            {
                DumpSubTree(tree, 0, sw);
            }
        }

        private static void DumpSubTree(BaseTree tree, int level, StreamWriter sw)
        {
            var text = tree.Text ?? "nil";
            sw.WriteLine(new String(' ', level * 2) + text);

            foreach(BaseTree subTree in tree.Children ?? new BaseTree[0])
            {
                DumpSubTree(subTree, level + 1, sw);
            }
        }
    }
}
