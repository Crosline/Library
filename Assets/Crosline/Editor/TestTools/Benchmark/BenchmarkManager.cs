using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Crosline.DebugTools;

namespace Crosline.TestTools.Editor.Benchmark {
    public class BenchmarkManager {
        public List<MethodInfo> MethodInfos
        {
            get
            {
                if (_methodInfos == null)
                    _methodInfos = new List<MethodInfo>();
                
                if (_methodInfos.Count == 0)
                    FillMethodInfo();

                return _methodInfos;
            }
        }

        private static List<MethodInfo> _methodInfos = new List<MethodInfo>();

        private static string[] _excludedAssemblies = {
            "System.",
            "UnityEngine.",
            "UnityEditor."
        };

        private static void FillMethodInfo() {
            _methodInfos.Clear();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => !x.FullName.Contains("System.") || !x.FullName.Contains("UnityEngine.") ||
                            !x.FullName.Contains("UnityEditor.")).ToArray();

            foreach (Assembly ass in assemblies) {
                var types = ass.GetTypes();
                foreach (Type type in types) {
                    var methods = type.GetMethods();
                    foreach (MethodInfo methodInfo in methods) _methodInfos.Add(methodInfo);
                }
            }
        }

        public static void Test() {
            Stopwatch stopWatch = new Stopwatch();
            foreach (MethodInfo method in _methodInfos) {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                BenchmarkAttribute attribute = method.GetCustomAttribute<BenchmarkAttribute>();

                try {
                    object obj = Activator.CreateInstance(method.DeclaringType);
                    stopWatch.Start();

                    for (int i = 0; i < attribute.IterationCount; i++) {
                        method.Invoke(obj, attribute.Parameters);
                    }

                    stopWatch.Stop();
                    CroslineDebug.LogWarning(
                        $"[{method.GetType().Name}:{method.Name}] executed in {stopWatch.ElapsedMilliseconds / attribute.IterationCount}ms");
                }
                catch (Exception e) {
                    CroslineDebug.LogError($"[{method.GetType().Name}:{method.Name}] could not be executed.\n{e}");
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