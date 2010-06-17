using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using Relinq.Script.Semantics.Literals;
using Relinq.Helpers.Reflection;

namespace Relinq.Playground.Interactive
{
    public class EntryPoint
    {
        public static void Main(String[] args)
        {
            var interactive = new RelinqInteractive();
            Console.WriteLine("Welcome to Relinq interactive!");

            while(true)
            {
                Console.WriteLine();
                Console.Write("> ");
                var cmd = Console.ReadLine();
                Console.WriteLine();

                if (cmd == "exit")
                {
                    break;
                }
                else
                {
                    var matchReg = Regex.Match(cmd, @"register (?<keyword>\S*) as (?<class>\S*) " +
                        "from \"(?<assembly>.*)\"");
                    if (matchReg.Success)
                    {
                        try
                        {
                            var keyword = matchReg.Result("${keyword}");
                            var className = matchReg.Result("${class}");
                            var assemblyName = matchReg.Result("${assembly}");

                            Assembly assembly = null;
                            try{assembly = Assembly.Load(new AssemblyName(assemblyName));}catch{}
                            try{assembly = Assembly.LoadFrom(assemblyName);}catch{}

                            if (assembly == null)
                            {
                                Console.WriteLine(String.Format(
                                    "Could not load assembly \"{0}\"", assemblyName));
                            }
                            else
                            {
                                var @class = assembly.GetType(className);
                                var instance = Activator.CreateInstance(@class);

                                interactive.RegisterKeyword(keyword, instance);
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }

                        continue;
                    }

                    var matchUnreg = Regex.Match(cmd, @"unregister (?<keyword>\S*)");
                    if (matchUnreg.Success)
                    {
                        try
                        {
                            var keyword = matchReg.Result("${keyword}");
                            interactive.UnregisterKeyword(keyword);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }

                        continue;
                    }

                    try
                    {
                        var result = interactive.Run(cmd);
                        if (result == null)
                        {
                            Console.WriteLine("null");
                        }
                        else
                        {
                            try
                            {
                                Console.WriteLine(JsonSerializer.Serialize(result, result.GetType()));
                            }
                            catch (Exception)
                            {
                                var eltype = result.GetType().GetEnumerableElement();
                                if (eltype != null)
                                {
                                    Console.WriteLine(JsonSerializer.Serialize(result,
                                        typeof(IEnumerable<>).XMakeGenericType(eltype)));
                                }
                                else
                                {
                                    throw;
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }
    }
}