using System;

namespace Crosline.UnityTools.Attributes {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class MethodAttribute : Attribute {
        public readonly object[] Parameters;

        protected MethodAttribute() {
            Parameters = null;
        }

        protected MethodAttribute(params object[] parameters) {
            Parameters = parameters;
        }
    }
}