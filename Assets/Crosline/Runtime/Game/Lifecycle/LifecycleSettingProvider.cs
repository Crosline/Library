using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crosline.Game.Lifecycle {
    public class LifecycleSettingProvider {

        private static SerializedObject _lifecycleSettingsSO;

        private static Vector2 _scrollPos;

        private static void OnActivate(string searchContext, VisualElement rootElement) {
            _lifecycleSettingsSO = new SerializedObject(LifecycleSettings.GetOrCreateDefault());
        }

        private static void OnGUI(string searchContext) {
            _lifecycleSettingsSO.Update();

            EditorGUILayout.PropertyField(_lifecycleSettingsSO.FindProperty("_gameSystems"), new GUIContent("Game Systems"), true);

            _lifecycleSettingsSO.ApplyModifiedPropertiesWithoutUndo();
        }

        [SettingsProvider]
        public static SettingsProvider CreateLifecycleSettingProvider() {
            var provider = new SettingsProvider(
                "Crosline/Lifecycle Settings",
                SettingsScope.Project)
            {
                keywords = new string[]
                {
                    "Lifecycle",
                    "update",
                    "mono",
                    "singleton"
                },
                activateHandler = OnActivate,
                guiHandler = OnGUI
            };

            return provider;
        }
    }
}