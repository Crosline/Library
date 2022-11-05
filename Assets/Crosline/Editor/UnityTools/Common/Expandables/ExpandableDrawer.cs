//ref: https://forum.unity.com/threads/editor-tool-better-scriptableobject-inspector-editing.484393/
using System;
using System.Collections.Generic;
using Crosline.DebugTools;
using Crosline.UnityTools.Attributes;
using UnityEngine;
using UnityEditor;

namespace Crosline.UnityTools.Editor {
    [CustomPropertyDrawer(typeof(ExpandableAttribute), true)]
    public class ExpandableAttributeDrawer : PropertyDrawer {
        #region Style Setup

        private static bool ENTER_CHILDREN = true;

        /// <summary>
        /// The spacing on the inside of the background rect.
        /// </summary>
        private static float INNER_SPACING = 6.0f;

        /// <summary>
        /// The spacing on the outside of the background rect.
        /// </summary>
        private static float OUTER_SPACING = 4.0f;

        #endregion

        /// <summary>
        /// Cached editor reference.
        /// </summary>
        private UnityEditor.Editor _editor = null;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            float totalHeight = 0.0f;

            totalHeight += EditorGUIUtility.singleLineHeight;

            if (property.objectReferenceValue == null)
                return totalHeight;

            if (!property.isExpanded)
                return totalHeight;

            if (_editor == null)
                UnityEditor.Editor.CreateCachedEditor(property.objectReferenceValue, null, ref _editor);

            if (_editor == null)
                return totalHeight;

            SerializedProperty field = _editor.serializedObject.GetIterator();

            field.NextVisible(true);

            while (field.NextVisible(ENTER_CHILDREN)) {
                if (field.depth > 0)
                    continue;
                totalHeight += EditorGUI.GetPropertyHeight(field, true) + EditorGUIUtility.standardVerticalSpacing;
            }
            totalHeight += INNER_SPACING * 2;
            totalHeight += OUTER_SPACING * 2;

            return totalHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            Rect fieldRect = new Rect(position);
            fieldRect.height = EditorGUIUtility.singleLineHeight;
            fieldRect.xMax -= OUTER_SPACING;

            EditorGUI.PropertyField(fieldRect, property, label, false);

            if (property.objectReferenceValue == null)
                return;

            property.isExpanded = EditorGUI.Foldout(fieldRect, property.isExpanded, GUIContent.none, true);

            if (!property.isExpanded)
                return;

            if (_editor == null)
                UnityEditor.Editor.CreateCachedEditor(property.objectReferenceValue, null, ref _editor);

            if (_editor == null) {
                CroslineDebug.Log("Editor could not be fetched");

                return;
            }


            #region Format Field Rects

            var propertyRects = new List<Rect>();
            Rect marchingRect = new Rect(fieldRect);
            marchingRect.xMax -= INNER_SPACING;

            Rect bodyRect = new Rect(fieldRect);
            bodyRect.xMin += EditorGUI.indentLevel * 14;

            bodyRect.yMin += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing
                                                               + OUTER_SPACING;

            SerializedProperty field = _editor.serializedObject.GetIterator();
            field.NextVisible(true);

            marchingRect.y += INNER_SPACING + OUTER_SPACING;

            while (field.NextVisible(ENTER_CHILDREN)) {
                if (field.depth > 0)
                    continue;
                marchingRect.y += marchingRect.height + EditorGUIUtility.standardVerticalSpacing;
                marchingRect.height = EditorGUI.GetPropertyHeight(field, true);
                propertyRects.Add(marchingRect);
            }

            marchingRect.y += INNER_SPACING;

            bodyRect.yMax = marchingRect.yMax;

            #endregion

            EditorGUI.HelpBox(bodyRect, "", MessageType.None);

            #region Draw Fields

            EditorGUI.indentLevel++;

            field = _editor.serializedObject.GetIterator();
            field.NextVisible(true);

            //Replacement for "editor.OnInspectorGUI ();" so we have more control on how we draw the editor

            for (int index = 0; field.NextVisible(ENTER_CHILDREN); index++) {
                try {
                    if (field.depth > 0)
                        continue;
                    EditorGUI.PropertyField(propertyRects[index], field, true);
                }
                catch (StackOverflowException) {
                    field.objectReferenceValue = null;
                    CroslineDebug.LogError(
                        "StackOverflowException detected. Avoid using the same object inside a nested structure.");
                }
            }

            EditorGUI.indentLevel--;

            #endregion


            if (_editor.serializedObject.hasModifiedProperties)
                _editor.serializedObject.ApplyModifiedProperties();
        }
    }
}