using System;
using System.Collections.Generic;
using System.Reflection;

namespace Crosline.UnityTools.Editor {
    public static class AttributeFinder {
        public static IEnumerable<MethodInfo> TryFindMethods<T>() where T : Attribute {
            return UnityEditor.TypeCache.GetMethodsWithAttribute<T>();
        }
    }
}