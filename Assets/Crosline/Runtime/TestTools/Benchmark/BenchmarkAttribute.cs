using System;

namespace Crosline.TestTools {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class BenchmarkAttribute : Attribute {

        public readonly object[] Parameters;

        public readonly int IterationCount;

        public BenchmarkAttribute(int iteration = 1) {
            Parameters = null;
            IterationCount = iteration;
        }

        public BenchmarkAttribute(int iteration = 1, params object[] parameters) {
            Parameters = parameters;
            IterationCount = iteration;
        }
    }
}
