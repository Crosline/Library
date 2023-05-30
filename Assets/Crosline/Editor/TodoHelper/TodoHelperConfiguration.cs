using System;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;
using FilePathAttribute = UnityEditor.FilePathAttribute;

namespace UnityTools.Editor {
    [FilePath("Assets/Crosline/Editor/TodoHelper/Config/TodoHelperConfiguration.asset", FilePathAttribute.Location.ProjectFolder)]
    public class TodoHelperConfiguration : ScriptableSingleton<TodoHelperConfiguration> {
        [NonSerialized] [ShowInInspector] [TitleGroup("Local Assignee")]
        public string LocalAssignee;

        [Title("Configuration")] public string[] AssigneeNames;

        public string[] FoldersToSearchTodo;

        public string[] PossibleIgnoreCaseTodoTypings;

        [TitleGroup("Local Assignee")]
        [Button]
        private void SaveLocalAssigneeName() {
            EditorPrefs.SetString("LocalAssignee", LocalAssignee);
            EditorApplication.update.Invoke();
        }

        [OnInspectorInit]
        private void GetSavedLocalAssignee() {
            LocalAssignee = EditorPrefs.GetString("LocalAssignee");

            if (LocalAssignee.IsNullOrWhitespace()) {
                Debug.LogError("Assign your local assignee name from todo helper configuration", this);
            }
        }

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

            return provider;
        }
    }
}