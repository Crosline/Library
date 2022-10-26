using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Crosline.UnityTools.Editor {
    public static class AttributeFinder {
        private static string[] _excludedAssemblies = {
            "System.",
            "UnityEngine.",
            "UnityEditor."
        };

        public static HashSet<MethodInfo> TryFindMethodInfos<T>() where T : Attribute {
            var methodInfos = new HashSet<MethodInfo>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => !x.FullName.Contains(_excludedAssemblies[0]) || !x.FullName.Contains(_excludedAssemblies[1]) ||
                            !x.FullName.Contains(_excludedAssemblies[2])).ToArray();

            foreach (Assembly ass in assemblies) {
                var types = ass.GetTypes();

                foreach (Type type in types) {
                    var methods = type.GetMethods();

                    foreach (MethodInfo methodInfo in methods)
                        if (methodInfo.GetCustomAttribute<T>() != null) {
                            methodInfos.Add(methodInfo);
                        }
                }
            }

            return methodInfos;
        }
    }
}