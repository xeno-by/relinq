using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using System.Linq;
using Relinq.Helpers.Reflection;
using Relinq.Helpers.Collections;

namespace Relinq.Playground
{
    [TestFixture, Ignore]
    public class AppDomainScanTests
    {
        #region AppDomain Scan Implementation

        private static bool ready;
        private static List<Type> ts;
        private static List<MethodInfo> ms;
        private static List<MemberInfo> fs;

        public void LoadPrgAssembliesAndScanAppDomain()
        {
            var swLoadAsm = Stopwatch.StartNew();
            swLoadAsm.Start();

            Trace.WriteLine("Loading Pereregister assemblies...");

            const string prgRoot = @"D:\Helmes\Sources\Current\Client\ShellApplication\bin\";
            const string localRoot = @".\";

            if (!Directory.Exists(@".\en"))
                Directory.CreateDirectory(@".\en");

            foreach (var fname in Directory.GetFiles(
                prgRoot, 
                "*.dll",
                SearchOption.AllDirectories))
            {
                var localName = fname.Replace(prgRoot, localRoot);
                File.Copy(fname, localName, true);

                if (!localName.EndsWith("EIDCard.dll") &&
                    !localName.Contains(".resources."))
                {
                    Assembly.LoadFrom(fname);
                }
            }

            swLoadAsm.Stop();
            Trace.WriteLine(String.Format("Elapsed time: {0}", swLoadAsm.Elapsed));

            var swTotal = Stopwatch.StartNew();
            swTotal.Start();

            var asses = AppDomain.CurrentDomain.GetAssemblies();

            Trace.WriteLine(String.Empty);
            Trace.WriteLine("Scanning AppDomain assemblies...");
            Trace.WriteLine(String.Format("{0} assemblies found", asses.Length));

            ts = new List<Type>();
            ms = new List<MethodInfo>();
            fs = new List<MemberInfo>();

            foreach (var ass in asses)
            {
//                Trace.WriteLine(String.Empty);
//                Trace.WriteLine(String.Format("Scanning assembly '{0}'", ass));

                var oldts = ts.Count;
                var oldms = ms.Count;
                var oldfs = fs.Count;

                foreach (var t in ass.GetTypes())
                {
                    ts.Add(t);

                    t.GetMethods(BF.All).ForEach(m => ms.Add(m));
                    t.GetFields(BF.All).ForEach(f => fs.Add(f));
                    t.GetProperties(BF.All).ForEach(p => fs.Add(p));
                }

//                Trace.WriteLine(String.Format(
//                    "Scanned {0} types, {1} methods, {2} fields",
//                    ts.Count - oldts,
//                    ms.Count - oldms,
//                    fs.Count - oldfs));
            }

//            Trace.WriteLine(String.Empty);
//            Trace.WriteLine("AppDomain scan has been completed");

            Trace.WriteLine(String.Format(
                "Found {0} types, {1} methods, {2} fields",
                ts.Count,
                ms.Count,
                fs.Count));

            swTotal.Stop();
            Trace.WriteLine(String.Format("Elapsed time: {0}", swTotal.Elapsed));
        }

        #endregion

        [Test]
        public void TestAppDomainScanNormalLoad()
        {
            if (!ready) LoadPrgAssembliesAndScanAppDomain();

            var swd = Stopwatch.StartNew();
            Trace.WriteLine(String.Empty);
            Trace.WriteLine("Calculating distincts");
            Trace.WriteLine(String.Format(
                "Found {0} distinctly named methods, {1} distinctly named fields",
                ms.Select(mi => mi.Name).Distinct().Count(),
                fs.Select(fi => fi.Name).Distinct().Count()));
            swd.Stop();
            Trace.WriteLine(String.Format("Elapsed time: {0}", swd.Elapsed));
            ready = true;

            Trace.WriteLine(String.Empty);
            Trace.WriteLine("Starting 100 searches within found methods");
            var swPerf = Stopwatch.StartNew();

            for (var i = 0; i < 100; ++i)
                ms.Where(mi => mi.Name == "get_Name").ToArray();

            swPerf.Stop();
            Trace.WriteLine(String.Format("Elapsed time: {0}", swPerf.Elapsed));
        }

        [Test]
        public void TestHostOfFieldInference()
        {
            if (!ready) LoadPrgAssembliesAndScanAppDomain();

            var swPerf = Stopwatch.StartNew();
            Trace.WriteLine(String.Empty);
            Trace.WriteLine("Reverse engineering host of IsPublic (100 times)");

            IEnumerable<Type> hosts = Enumerable.Empty<Type>();
            for (var i = 0; i < 100; ++i)
                hosts = fs.Where(f => f.Name == "IsPublic").Select(f => f.DeclaringType).ToArray();

            Trace.WriteLine(String.Format("Found {0} alternatives", hosts.Count()));
            swPerf.Stop();
            Trace.WriteLine(String.Format("Elapsed time: {0}", swPerf.Elapsed));
        }

        [Test]
        public void TestFindAllDeleteMethods()
        {
            if (!ready) LoadPrgAssembliesAndScanAppDomain();

            var swPerf = Stopwatch.StartNew();
            Trace.WriteLine(String.Empty);
            Trace.WriteLine("Reverse engineering host of Delete (100 times)");

            var hosts = Enumerable.Empty<Type>();
            for (var i = 0; i < 100; ++i)
                hosts =
                    from m in ms
                    where m.Name == "Delete"
                    select m.DeclaringType;

            Trace.WriteLine(String.Format("Found {0} alternatives", hosts.Count()));
            swPerf.Stop();
            Trace.WriteLine(String.Format("Elapsed time: {0}", swPerf.Elapsed));
        }

        [Test]
        public void TestFindDeleteStringMethods()
        {
            if (!ready) LoadPrgAssembliesAndScanAppDomain();

            var swPerf = Stopwatch.StartNew();
            Trace.WriteLine(String.Empty);
            Trace.WriteLine("Reverse engineering host of Delete(String...) (100 times)");

            var hosts = Enumerable.Empty<Type>();
            for (var i = 0; i < 100; ++i)
                hosts = 
                    from m in ms
                    where m.Name == "Delete"
                    let ps = m.GetParameters()
                    where ps.Length != 0 && ps[0].ParameterType == typeof(String)
                    select m.DeclaringType;

            Trace.WriteLine(String.Format("Found {0} alternatives", hosts.Count()));
            swPerf.Stop();
            Trace.WriteLine(String.Format("Elapsed time: {0}", swPerf.Elapsed));
        }
    }
}