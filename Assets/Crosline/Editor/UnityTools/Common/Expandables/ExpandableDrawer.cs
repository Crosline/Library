//ref: https://forum.unity.com/threads/editor-tool-better-scriptableobject-inspector-editing.484393/
using System;
using System.Collections.Generic;
using Crosline.UnityTools.Attributes;
using UnityEngine;
using UnityEditor;

namespace Crosline.UnityTools.Editor {
    [CustomPropertyDrawer(typeof(ExpandableAttribute), true)]
    public class ExpandableAttributeDrawer : PropertyDrawer {
        // Use the following area to change the style of the expandable ScriptableObject drawers;

        #region Style Setup

        /// <summary>
        /// The spacing on the inside of the background rect.
        /// </summary>
        private const float INNER_SPACING = 6.0f;

        /// <summary>
        /// The spacing on the outside of the background rect.
        /// </summary>
        private const float OUTER_SPACING = 4.0f;

        #endregion

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            var totalHeight = 0.0f;

            totalHeight += EditorGUIUtility.singleLineHeight;

            if (property.objectReferenceValue == null)
                return totalHeight;

            if (!property.isExpanded)
                return totalHeight;

            var targetObject = new SerializedObject(property.objectReferenceValue);

            var field = targetObject.GetIterator();

            field.NextVisible(true);

            while (field.NextVisible(false)) {
                totalHeight += EditorGUI.GetPropertyHeight(field, true) + EditorGUIUtility.standardVerticalSpacing;
            }

            totalHeight += INNER_SPACING * 2;
            totalHeight += OUTER_SPACING * 2;

            return totalHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var fieldRect = new Rect(position) {
                height = EditorGUIUtility.singleLineHeight
            };

            EditorGUI.PropertyField(fieldRect, property, label, true);

            if (property.objectReferenceValue == null)
                return;

            property.isExpanded = EditorGUI.Foldout(fieldRect, property.isExpanded, GUIContent.none, true);

            if (!property.isExpanded)
                return;

            var targetObject = new SerializedObject(property.objectReferenceValue);


            #region Format Field Rects
            var propertyRects = new List<Rect>();
            var marchingRect = new Rect(fieldRect);

            var bodyRect = new Rect(fieldRect);
            bodyRect.xMin += EditorGUI.indentLevel * 14;

            bodyRect.yMin += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing
                                                               + OUTER_SPACING;

            var field = targetObject.GetIterator();
            field.NextVisible(true);

            marchingRect.y += INNER_SPACING + OUTER_SPACING;

            while (field.NextVisible(false)) {
                marchingRect.y += marchingRect.height + EditorGUIUtility.standardVerticalSpacing;
                marchingRect.height = EditorGUI.GetPropertyHeight(field, true);
                propertyRects.Add(marchingRect);
            }

            marchingRect.y += INNER_SPACING;

            bodyRect.yMax = marchingRect.yMax;
            #endregion

            EditorGUI.HelpBox (bodyRect, "", MessageType.None);

            #region Draw Fields
            EditorGUI.indentLevel++;

            var index = 0;
            field = targetObject.GetIterator();
            field.NextVisible(true);

            //Replacement for "editor.OnInspectorGUI ();" so we have more control on how we draw the editor
            while (field.NextVisible(false)) {
                try {
                    EditorGUI.PropertyField(propertyRects[index], field, true);
                }
                catch (StackOverflowException) {
                    field.objectReferenceValue = null;

                    Debug.LogError("Detected self-nesting causing a StackOverflowException, avoid using the same " +
                                   "object inside a nested structure.");
                }

                index++;
            }

            if (targetObject.hasModifiedProperties)
                targetObject.ApplyModifiedProperties();

            EditorGUI.indentLevel--;
            #endregion
        }
    }
}
