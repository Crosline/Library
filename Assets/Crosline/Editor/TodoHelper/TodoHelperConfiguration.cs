using System;
// using Sirenix.OdinInspector;
// using Sirenix.Utilities;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;
using FilePathAttribute = UnityEditor.FilePathAttribute;

namespace UnityTools.Editor {
    [FilePath("Assets/Crosline/Editor/TodoHelper/Config/TodoHelperConfiguration.asset", FilePathAttribute.Location.ProjectFolder)]
    public class TodoHelperConfiguration : ScriptableSingleton<TodoHelperConfiguration> {
        public string LocalAssignee;

        public string[] AssigneeNames;

        public string[] FoldersToSearchTodo;

        public string[] PossibleIgnoreCaseTodoTypings;

        private void SaveLocalAssigneeName() {
            EditorPrefs.SetString("Crosline_LocalAssignee", LocalAssignee);
            EditorApplication.update.Invoke();
        }

        private void GetSavedLocalAssigneeName() {
            LocalAssignee = EditorPrefs.GetString("Crosline_LocalAssignee");
        }

        #region Settings Provider

        [SettingsProvider]
        public static SettingsProvider CreateCustomToolbarSettingProvider() {
            SettingsProvider provider = new SettingsProvider("Crosline/Todo Helper", SettingsScope.Project)
            {
                keywords = new string[]
                {
                    "TODO",
                    "assignee",
                    "urgent",
                }
            };

            instance.GetSavedLocalAssigneeName();

            provider.guiHandler += GuiHandler;

            return provider;
        }

        private static SerializedObject GetSerializedSettings() {
            return new SerializedObject(instance);
        }

        private static void GuiHandler(string searchContext) {
            var settings = GetSerializedSettings();
            
            EditorGUILayout.PropertyField(settings.FindProperty("LocalAssignee"), new GUIContent("Local Assignee"));
            EditorGUILayout.PropertyField(settings.FindProperty("AssigneeNames"), new GUIContent("Assignee Names"));
            EditorGUILayout.PropertyField(settings.FindProperty("FoldersToSearchTodo"), new GUIContent("Folders To Search Todo"));
            EditorGUILayout.PropertyField(settings.FindProperty("PossibleIgnoreCaseTodoTypings"), new GUIContent("Possible IgnoreCase Todo Typings"));

            if (settings.hasModifiedProperties) {
                settings.ApplyModifiedPropertiesWithoutUndo();
                instance.SaveLocalAssigneeName();
            }
        }
        
        #endregion
    }
}