using System;

namespace Serializables.Editor
{
    public static class TypeExtensions
    {
        public static bool InheritsOrImplements(this Type type, Type baseType)
        {
            type = ResolveGenericType(type);
            baseType = ResolveGenericType(baseType);

            while (type != typeof(object))
            {
                if (baseType == type || HasAnyInterfaces(type, baseType)) return true;

                type = ResolveGenericType(type.BaseType);
                if (type == null) return false;
            }

            return false;
        }

        internal static Type ResolveGenericType(Type type)
        {
            if (type is not { IsGenericType: true }) return type;

            var genericType = type.GetGenericTypeDefinition();
            return genericType != type ? genericType : type;
        }

        internal static bool HasAnyInterfaces(Type type, Type interfaceType)
        {
            foreach (var i in type.GetInterfaces())
                if (ResolveGenericType(i) == interfaceType)
                    return true;

            return false;
        }
    }
}