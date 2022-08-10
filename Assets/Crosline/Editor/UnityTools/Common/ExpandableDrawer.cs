//ref: https://forum.unity.com/threads/editor-tool-better-scriptableobject-inspector-editing.484393/
using System;
using System.Collections.Generic;
using Crosline.DebugTools;
using UnityEngine;
using UnityEditor;

namespace Crosline.UnityTools.Editor {
    [CustomPropertyDrawer(typeof(ExpandableAttribute), true)]
    public class ExpandableAttributeDrawer : PropertyDrawer {
        // Use the following area to change the style of the expandable ScriptableObject drawers;
        #region Style Setup
        private enum BackgroundStyles {
            None,
            HelpBox,
            Darken,
            Lighten
        }

        /// <summary>
        /// Whether the default editor Script field should be shown.
        /// </summary>
        private static bool SHOW_SCRIPT_FIELD = false;

        /// <summary>
        /// The spacing on the inside of the background rect.
        /// </summary>
        private static float INNER_SPACING = 6.0f;

        /// <summary>
        /// The spacing on the outside of the background rect.
        /// </summary>
        private static float OUTER_SPACING = 4.0f;

        /// <summary>
        /// The style the background uses.
        /// </summary>
        private static BackgroundStyles BACKGROUND_STYLE = BackgroundStyles.HelpBox;

        /// <summary>
        /// The colour that is used to darken the background.
        /// </summary>
        private static Color DARKEN_COLOUR = new(0.0f, 0.0f, 0.0f, 0.2f);

        /// <summary>
        /// The colour that is used to lighten the background.
        /// </summary>
        private static Color LIGHTEN_COLOUR = new(1.0f, 1.0f, 1.0f, 0.2f);
        #endregion

        /// <summary>
        /// Cached editor reference.
        /// </summary>
        private UnityEditor.Editor _editor = null;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            var totalHeight = 0.0f;

            totalHeight += EditorGUIUtility.singleLineHeight;

            if (property.objectReferenceValue == null)
                return totalHeight;

            if (!property.isExpanded)
                return totalHeight;

            if (_editor == null)
                UnityEditor.Editor.CreateCachedEditor(property.objectReferenceValue, null, ref _editor);

            if (_editor == null)
                return totalHeight;

            var field = _editor.serializedObject.GetIterator();

            field.NextVisible(true);

            if (SHOW_SCRIPT_FIELD)
                totalHeight += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            while (field.NextVisible(true))
                totalHeight += EditorGUI.GetPropertyHeight(field, true) + EditorGUIUtility.standardVerticalSpacing;

            totalHeight += INNER_SPACING * 2;
            totalHeight += OUTER_SPACING * 2;

            return totalHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var fieldRect = new Rect(position);
            fieldRect.height = EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(fieldRect, property, label, true);

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
            var marchingRect = new Rect(fieldRect);

            var bodyRect = new Rect(fieldRect);
            bodyRect.xMin += EditorGUI.indentLevel * 18;
            bodyRect.yMin += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing
                                                               + OUTER_SPACING;

            var field = _editor.serializedObject.GetIterator();
            field.NextVisible(true);

            marchingRect.y += INNER_SPACING + OUTER_SPACING;

            if (SHOW_SCRIPT_FIELD) {
                propertyRects.Add(marchingRect);
                marchingRect.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }

            while (field.NextVisible(true)) {
                marchingRect.y += marchingRect.height + EditorGUIUtility.standardVerticalSpacing;
                marchingRect.height = EditorGUI.GetPropertyHeight(field, true);
                propertyRects.Add(marchingRect);
            }

            marchingRect.y += INNER_SPACING;

            bodyRect.yMax = marchingRect.yMax;
        #endregion

            DrawBackground(bodyRect);

        #region Draw Fields
            EditorGUI.indentLevel++;

            var index = 0;
            field = _editor.serializedObject.GetIterator();
            field.NextVisible(true);

            if (SHOW_SCRIPT_FIELD) {
                //Show the disabled script field
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.PropertyField(propertyRects[index], field, true);
                EditorGUI.EndDisabledGroup();
                index++;
            }

            //Replacement for "editor.OnInspectorGUI ();" so we have more control on how we draw the editor
            while (field.NextVisible(true)) {
                try {
                    EditorGUI.PropertyField(propertyRects[index], field, true);
                }
                catch (StackOverflowException) {
                    field.objectReferenceValue = null;
                    CroslineDebug.LogError("StackOverflowException detected. Avoid using the same object inside a nested structure.");
                }

                index++;
            }

            EditorGUI.indentLevel--;
        #endregion


            if (_editor.serializedObject.hasModifiedProperties) {
                _editor.serializedObject.ApplyModifiedProperties();
            }
        }

        /// <summary>
        /// Draws the Background
        /// </summary>
        /// <param name="rect">The Rect where the background is drawn.</param>
        private void DrawBackground(Rect rect) {
            switch (BACKGROUND_STYLE) {

                case BackgroundStyles.HelpBox:
                    EditorGUI.HelpBox(rect, "", MessageType.None);

                    break;

                case BackgroundStyles.Darken:
                    EditorGUI.DrawRect(rect, DARKEN_COLOUR);

                    break;

                case BackgroundStyles.Lighten:
                    EditorGUI.DrawRect(rect, LIGHTEN_COLOUR);

                    break;
            }
        }
    }
}
