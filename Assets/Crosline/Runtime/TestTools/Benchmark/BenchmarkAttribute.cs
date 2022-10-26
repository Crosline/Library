using System;

namespace Crosline.TestTools {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class BenchmarkAttribute : Attribute {

        public readonly object[] Parameters;

        public BenchmarkAttribute() {
            Parameters = null;
        }

        public BenchmarkAttribute(int iteration = 1, params object[] parameters) {
            Parameters = parameters;
        }
    }
}
