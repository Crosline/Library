using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Serializables.Editor
{
    [CustomPropertyDrawer(typeof(SerializableType<>), true)]
    public class SerializableTypeDrawer : PropertyDrawer
    {
        private string[] _typeNames, _typeFullNames;

        private void Initialize()
        {
            if (_typeFullNames != null) return;

            var parentType = fieldInfo.FieldType;
            parentType = parentType.IsArray 
                ? parentType.GetElementType()!.GetGenericArguments()[0] 
                : parentType.GetGenericArguments()[0];

            var filteredTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => ParentFilter(t, parentType))
                .ToArray();


            _typeNames = filteredTypes.Select(t => t.ReflectedType == null ? t.Name : "t.ReflectedType.Name + t.Name")
                .ToArray();
            _typeFullNames = filteredTypes.Select(t => t.AssemblyQualifiedName).ToArray();
        }

        private static bool ParentFilter(Type type, Type parentType)
        {
            return !type.IsAbstract &&
                   !type.IsInterface &&
                   !type.IsGenericType &&
                   type != parentType &&
                   type.InheritsOrImplements(parentType);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Initialize();
            if (property.isArray)
            {
                SerializeList(position, property);
                return;
            }

            SerializeType(position, property);
        }

        private void SerializeList(Rect position, SerializedProperty property)
        {
            if (!property.isArray) return;

            for (int i = 0; i < property.arraySize; i++)
            {
                var element = property.GetArrayElementAtIndex(i);

                if (i > 0) position.y += EditorGUIUtility.singleLineHeight;

                SerializeType(position, element);
            }
        }

        private void SerializeType(Rect position, SerializedProperty property)
        {
            var assemblyQualifiedNameProperty = property.FindPropertyRelative("assemblyQualifiedName");

            var currentIndex = Array.IndexOf(_typeFullNames, assemblyQualifiedNameProperty?.stringValue ?? "");

            var selectedIndex = EditorGUI.Popup(position, "", currentIndex, _typeNames);

            if (assemblyQualifiedNameProperty == null) return;

            if (selectedIndex < 0 || selectedIndex == currentIndex) return;

            assemblyQualifiedNameProperty.stringValue = _typeFullNames[selectedIndex];
            property.serializedObject.ApplyModifiedProperties();
        }
    }
}