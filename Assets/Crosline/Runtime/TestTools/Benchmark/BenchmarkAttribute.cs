using System;
using Crosline.UnityTools.Attributes;

namespace Crosline.TestTools {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class BenchmarkAttribute : MethodAttribute {

        public BenchmarkAttribute() : base() { }

        public BenchmarkAttribute(params object[] parameters) : base(parameters) { }
    }
}
