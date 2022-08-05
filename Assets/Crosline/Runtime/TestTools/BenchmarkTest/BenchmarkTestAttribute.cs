using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Crosline.DebugTools;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Crosline.TestTools {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class BenchmarkTestAttribute : Attribute {

        public object[] Parameters;
        
        public BenchmarkTestAttribute() {
            Parameters = null;
        }

        public BenchmarkTestAttribute(params object[] parameters)
        {
            Parameters = parameters;
        }
        
        [BenchmarkTest]
        private void BenchmarkTestAttributeTest() {
            byte[] b = new byte[100000000];
            for (int i = 0; i < 100000000; i++) {
                b[i] = 0x00000001;
            }
        }

        public static void SecondTest() {
            Debug.Log(1);
            var methods = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsClass)
                .SelectMany(x => x.GetMethods())
                .Where(x => x.GetCustomAttributes(typeof(BenchmarkTestAttribute), false).FirstOrDefault() != null);

            Debug.Log(2);
            foreach (var method in methods) {
                Debug.Log(3);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                BenchmarkTestAttribute attribute = method.GetCustomAttribute<BenchmarkTestAttribute>();

                Stopwatch stopWatch = new Stopwatch();

                try {
                    Debug.Log(4);
                    stopWatch.Start();
                    method.Invoke(null, attribute.Parameters);
                    stopWatch.Stop();

                    CroslineDebug.LogWarning($"[{method.Name}] executed in {stopWatch.ElapsedMilliseconds}ms");
                }
                catch (Exception) {
                    Debug.Log(5);
                    CroslineDebug.LogError($"[{method.Name}] could not be executed.");
                }
                finally {
                    Debug.Log(6);
                    if (stopWatch.IsRunning)
                        stopWatch.Stop();

                    stopWatch.Reset();
                }
            }
        }
        
        public static void Test() {
            Debug.Log(1);
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();

            Debug.Log(2);
            foreach (var type in types)
            {
                Debug.Log(3);
                MethodInfo[] methods = type.GetMethods();
                methods = methods.Where(x => x.GetCustomAttribute<BenchmarkTestAttribute>() != null).ToArray();

                foreach (var method in methods)
                {
                    Debug.Log(4);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();

                    BenchmarkTestAttribute attribute = method.GetCustomAttribute<BenchmarkTestAttribute>();

                    Stopwatch stopWatch = new Stopwatch();

                    try {
                        Debug.Log(5);
                        stopWatch.Start();
                        method.Invoke(null, attribute.Parameters);
                        stopWatch.Stop();
                        
                        CroslineDebug.LogWarning($"[{type.Name}:{method.Name}] executed in {stopWatch.ElapsedMilliseconds}ms");
                    }
                    catch (Exception) {
                        CroslineDebug.LogError($"[{type.Name}:{method.Name}] could not be executed.");
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
