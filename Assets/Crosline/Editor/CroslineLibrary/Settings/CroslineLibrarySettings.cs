using Unity.Collections;
using UnityEditor;
using UnityEngine;

namespace Crosline.CroslineLibrary.Editor {
    [FilePath("ProjectSettings/CroslineLibrarySettings.asset", FilePathAttribute.Location.ProjectFolder)]
    
    internal class CroslineLibrarySettings : ScriptableSingleton<CroslineLibrarySettings> {
        [field: SerializeField, ReadOnly] public string Version = "0.0.0";
        
        internal static SerializedObject GetSerializedSetting() {
            return new SerializedObject(instance);
        }

        internal void Save() {
            Save(true);
        }
    }
}