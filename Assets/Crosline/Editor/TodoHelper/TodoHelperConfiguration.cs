using System;
using System.IO;
using Crosline.UnityTools.Editor;
// using Sirenix.OdinInspector;
// using Sirenix.Utilities;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;
using FilePathAttribute = UnityEditor.FilePathAttribute;

namespace UnityTools.Editor {
    [FilePath("Assets/Settings/Resources/TodoHelper/TodoHelperConfiguration.asset", FilePathAttribute.Location.ProjectFolder)]
    public class TodoHelperConfiguration : CroslineScriptableSingleton<TodoHelperConfiguration> {
        public string LocalAssignee;

        public string[] AssigneeNames;

        public string[] FoldersToSearchTodo;

        public string[] PossibleIgnoreCaseTodoTypings;

        #region AssetCreation

        protected override string AssetDirectory => "Assets/Settings/Resources/TodoHelper";
        protected override void SetDefaults() {
            instance.PossibleIgnoreCaseTodoTypings = new[] {
                "//TODO",
                "// TODO",
                "//TO DO",
                "// TO DO",
            };
        }

        // internal static TodoHelperConfiguration GetOrCreate() {
        //     var instance = ScriptableSingleton<TodoHelperConfiguration>.instance;
        //
        //     const string directory = "Assets/Settings/Resources/TodoHelper";
        //
        //     if (instance != null)
        //         return instance;
        //
        //     if (!Directory.Exists(directory))
        //         Directory.CreateDirectory(directory);
        //         
        //     instance = CreateInstance<TodoHelperConfiguration>();
        //
        //     instance.PossibleIgnoreCaseTodoTypings = new[] {
        //         "//TODO",
        //         "// TODO",
        //         "//TO DO",
        //         "// TO DO",
        //     };
        //         
        //     AssetDatabase.CreateAsset(instance, $"directory/TodoHelperConfiguration.asset");
        //
        //     return instance;
        // }
        #endregion

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