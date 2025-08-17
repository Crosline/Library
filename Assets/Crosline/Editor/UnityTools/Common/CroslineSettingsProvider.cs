using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UIElements;

namespace Crosline.UnityTools.Editor {
    internal class CroslineSettingsProvider<T> : SettingsProvider where T : ScriptableSingleton<T> {
        
        private SerializedObject _serializedSetting;
        private T _setting;
        
        public static bool IsSettingAvailable() => ScriptableSingleton<T>.instance != null;
        
        internal CroslineSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : base(path, scopes, keywords) {
        }

        public override void OnActivate(string searchContext, VisualElement rootElement) {
            _serializedSetting = new SerializedObject(ScriptableSingleton<T>.instance);
            _setting = (_serializedSetting.targetObject as T);
        }

        public override void OnGUI(string searchContext) {
            base.OnGUI(searchContext);
        }
    }
}