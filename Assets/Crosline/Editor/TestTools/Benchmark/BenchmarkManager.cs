using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Crosline.DebugTools;
using Crosline.UnityTools.Editor;

namespace Crosline.TestTools.Editor {
    public static class BenchmarkManager {
        public static Dictionary<MethodInfo, long> MethodInfos {
            get {
                if (_methodInfos == null)
                    _methodInfos = new Dictionary<MethodInfo, long>();

                if (_methodInfos.Count == 0)
                    FillMethodInfo();

                return _methodInfos;
            }
        }

        private static Dictionary<MethodInfo, long> _methodInfos = new Dictionary<MethodInfo, long>();

        public static void FillMethodInfo() {
            _methodInfos.Clear();
            var foundedMethodInfos = AttributeFinder.TryFindMethods<BenchmarkAttribute>();

            foreach (var methodInfo in foundedMethodInfos) {
                _methodInfos.Add(methodInfo, -1);
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

        public static void TestCachedMethods(int iteration = 1) {
            var methods = _methodInfos.Keys.ToArray();

            foreach (var method in methods) {
                TestMethod(method, iteration);
            }
        }

        public static void TestMethod(MethodInfo method, int iterationCount = 1) {
            Stopwatch stopWatch = new Stopwatch();

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            BenchmarkAttribute attribute = method.GetCustomAttribute<BenchmarkAttribute>();

            try {
                object obj = Activator.CreateInstance(method.DeclaringType);
                stopWatch.Start();

                for (int i = 0; i < iterationCount; i++) {
                    method.Invoke(obj, attribute.Parameters);
                }

                stopWatch.Stop();

                CroslineDebug.LogWarning(
                    $"[{method.GetType().Name}:{method.Name}] executed in {stopWatch.ElapsedMilliseconds / iterationCount}ms");

                _methodInfos[method] = stopWatch.ElapsedMilliseconds / iterationCount;
            }
            catch (Exception e) {
                CroslineDebug.LogError($"[{method.DeclaringType?.Name}:{method.Name}] could not be executed.\n{e}");

                _methodInfos[method] = -2;
            }
            finally {
                if (stopWatch.IsRunning)
                    stopWatch.Stop();
            }
        }
    }
}
