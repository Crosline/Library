using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

namespace Crosline.UnityTools.Editor {
    public static class AttributeFinder {
        public static IEnumerable<MethodInfo> TryFindMethods<T>() where T : Attribute {
            return TypeCache.GetMethodsWithAttribute<T>();
        }
    }
}