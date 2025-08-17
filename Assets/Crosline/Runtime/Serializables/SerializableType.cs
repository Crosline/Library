using System;
using System.Reflection;
using UnityEngine;

namespace Serializables
{
    [Serializable]
    public class SerializableType<T> : ISerializationCallbackReceiver
    {
        [SerializeField]
        private string assemblyQualifiedName = string.Empty;

        public TypeInfo TypeInfo;

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (TypeInfo == null)
            {
                TypeInfo = typeof(T).GetTypeInfo();
            }
            
            assemblyQualifiedName = TypeInfo.AssemblyQualifiedName;
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (string.IsNullOrWhiteSpace(assemblyQualifiedName)) return;

            var type = Type.GetType(assemblyQualifiedName);
            
            if (type == null) return;

            TypeInfo = type.GetTypeInfo();
        }
    }
}