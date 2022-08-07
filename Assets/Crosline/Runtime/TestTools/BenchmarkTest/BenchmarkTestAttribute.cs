using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Crosline.DebugTools;

namespace Crosline.TestTools {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class BenchmarkTestAttribute : Attribute {

        private readonly object[] Parameters;

        private readonly int IterationCount;

        public BenchmarkTestAttribute(int iteration = 1) {
            Parameters = null;
            IterationCount = iteration;
        }

        public BenchmarkTestAttribute(int iteration = 1, params object[] parameters) {
            Parameters = parameters;
            IterationCount = iteration;
        }

        private static string[] _excludedAssemblies =
        {
            "System.",
            "UnityEngine.",
            "UnityEditor."
        };
        
        public static void Test() {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => !x.FullName.).ToArray();

            var assemb = _excludedAssemblies.Any(x => AppDomain.CurrentDomain.GetAssemblies().Contains<string>(x));

            foreach (var ass in assemblies) {

                Type[] types = ass.GetTypes();


                foreach (var type in types) {
                    MethodInfo[] methods = type.GetMethods();
                    methods = methods.Where(x => x.GetCustomAttribute<BenchmarkTestAttribute>() != null).ToArray();

                    foreach (var method in methods) {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        GC.Collect();

                        BenchmarkTestAttribute attribute = method.GetCustomAttribute<BenchmarkTestAttribute>();

                        Stopwatch stopWatch = new Stopwatch();

                        try {
                            var obj = Activator.CreateInstance(method.DeclaringType);
                            stopWatch.Start();

                            for (int i = 0; i < attribute.IterationCount; i++) {
                                
                            }
                            method.Invoke(obj, attribute.Parameters);
                            stopWatch.Stop();

                            CroslineDebug.LogWarning($"[{type.Name}:{method.Name}] executed in {stopWatch.ElapsedMilliseconds}ms");
                        }
                        catch (Exception e) {
                            CroslineDebug.LogError($"[{type.Name}:{method.Name}] could not be executed.\n{e}");
                        }
                        finally {
                            if (stopWatch.IsRunning)
                                stopWatch.Stop();

                            stopWatch.Reset();
                        }
                    }
                }
            }
        }
    }
}
