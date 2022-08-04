using System;
using System.Diagnostics;
using Crosline.DebugTools;
using UnityEngine;

namespace Crosline.TestTools {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class BenchmarkTestAttribute : Attribute, IDisposable {


        private readonly Stopwatch _stopwatch;

        public BenchmarkTestAttribute() {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public static void Test() {

        }

        public void Dispose() {
            _stopwatch.Stop();
            CroslineDebug.Log($"Time elapsed: {_stopwatch.ElapsedMilliseconds}");
        }
    }
}
