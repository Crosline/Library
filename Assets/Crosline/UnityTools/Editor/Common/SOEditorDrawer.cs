using UnityEngine;
using UnityEditor;

namespace Crosline.UnityTools.Editor {
    [CustomPropertyDrawer(typeof(SOEditorAttribute))]
    public class SOEditorDrawer : PropertyDrawer {

        private UnityEditor.Editor _editor = null;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

            EditorGUI.PropertyField(position, property, label, true);

            if (property.objectReferenceValue != null) {
                property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none);
            }

            // Draw foldout properties
            if (property.isExpanded) {
                // Make child fields be indented
                EditorGUI.indentLevel++;

                // Draw object properties
                if (!_editor)
                    UnityEditor.Editor.CreateCachedEditor(property.objectReferenceValue, null, ref _editor);

                _editor.OnInspectorGUI();

                // Set indent back to what it was
                EditorGUI.indentLevel--;
            }
        }
    }
}
