using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Crosline.DebugTools;

namespace Crosline.TestTools.Editor.Benchmark {
    public static class BenchmarkManager {
        public static Dictionary<MethodInfo, long> MethodInfos
        {
            get
            {
                if (_methodInfos == null)
                    _methodInfos = new Dictionary<MethodInfo, long>();
                
                if (_methodInfos.Count == 0)
                    FillMethodInfo();

                return _methodInfos;
            }
        }

        private static Dictionary<MethodInfo, long> _methodInfos = new Dictionary<MethodInfo, long>();

        private static string[] _excludedAssemblies = {
            "System.",
            "UnityEngine.",
            "UnityEditor."
        };

        public static void FillMethodInfo() {
            _methodInfos.Clear();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => !x.FullName.Contains("System.") || !x.FullName.Contains("UnityEngine.") ||
                            !x.FullName.Contains("UnityEditor.")).ToArray();

            foreach (Assembly ass in assemblies) {
                var types = ass.GetTypes();
                foreach (Type type in types) {
                    var methods = type.GetMethods();
                    foreach (MethodInfo methodInfo in methods)
                        if (methodInfo.GetCustomAttribute<BenchmarkAttribute>() != null) {
                            _methodInfos.Add(methodInfo, -1);
                        }
                }
            }
            
            CroslineDebug.Log($"MethodCount is {_methodInfos.Count}");
        }

        public static void ResetBenchmark(MethodInfo method) {
            _methodInfos[method] = -1;
        }
        
        public static void ResetAllBenchmark() {
            foreach (var method in _methodInfos.Keys.ToArray()) {
                _methodInfos[method] = -1;
            }
        }

        public static void TestCachedMethods() {
            var methods = _methodInfos.Keys.ToArray();
            foreach (var method in methods) {
                TestMethod(method);
            }
        }

        public static void TestMethod(MethodInfo method) {
            Stopwatch stopWatch = new Stopwatch();
            
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
                _methodInfos[method] = stopWatch.ElapsedMilliseconds / attribute.IterationCount;
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