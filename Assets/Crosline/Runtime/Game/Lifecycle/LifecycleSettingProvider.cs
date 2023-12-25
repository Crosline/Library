using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crosline.Game.Lifecycle {
    public class LifecycleSettingProvider : SettingsProvider {

        private SerializedObject _lifecycleSettingsSO;

        private LifecycleSettings _lifecycleSettings;

        private Vector2 _scrollPos;

        public LifecycleSettingProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : base(
            path,
            scopes,
            keywords) {
        }

        public override void OnActivate(string searchContext, VisualElement rootElement) {
            _lifecycleSettings = AssetDatabase.LoadAssetAtPath<LifecycleSettings>(LifecycleSettings.SettingsPath);
            _lifecycleSettingsSO = new SerializedObject(_lifecycleSettings);
        }

        public override void OnGUI(string searchContext) {
            base.OnGUI(searchContext);
            
            EditorGUILayout.LabelField("ANNEn");
            return;

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

            SerializedProperty iterator = _lifecycleSettingsSO.GetIterator();
            bool enterChildren = true;
            while (iterator.NextVisible(enterChildren))
            {
                enterChildren = false;
                EditorGUILayout.PropertyField(iterator, true);
            }

            _lifecycleSettingsSO.ApplyModifiedProperties();

            if (!GUI.changed) return;


            EditorUtility.SetDirty(_lifecycleSettingsSO.targetObject);
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
                }
            };

            return provider;
        }
    }
}