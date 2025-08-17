using Unity.Collections;
using UnityEditor;
using UnityEngine;

namespace Crosline.CroslineLibrary.Editor {
    [FilePath("ProjectSettings/CroslineLibrarySettings.asset", FilePathAttribute.Location.ProjectFolder)]
    internal class CroslineLibrarySettings : ScriptableSingleton<CroslineLibrarySettings> {
        [field: SerializeField, ReadOnly] public string Version = "0.0.0";

        internal void Save() {
            Save(true);
        }


        #region Settings Provider

        [SettingsProvider]
        public static SettingsProvider CreateCustomToolbarSettingProvider() {
            var provider = new SettingsProvider("Crosline/Library Settings", SettingsScope.Project)
            {
                keywords = new string[]
                {
                    "Library",
                    "Crosline"
                }
            };
            
            provider.guiHandler += GuiHandler;

            return provider;
        }
        
        private static SerializedObject GetSerializedSettings() {
            return new SerializedObject(instance);
        }

        private static void GuiHandler(string searchContext) {
            var settings = GetSerializedSettings();
            
            EditorGUILayout.PropertyField(settings.FindProperty("Version"), new GUIContent("Version"));
            
            settings.ApplyModifiedPropertiesWithoutUndo();
        }

        #endregion
    }
}