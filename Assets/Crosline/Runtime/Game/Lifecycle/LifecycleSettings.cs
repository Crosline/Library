using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Crosline.Game.Lifecycle {
    internal class LifecycleSettings : ScriptableObject {
#if UNITY_EDITOR
        private const string SettingsPath = "Assets/Crosline/Runtime/Game/Resources/LifecycleSettings.asset";
#endif
        public GameSystemBase[] GameSystems { get => _gameSystems; }

        [SerializeReference]
        private GameSystemBase[] _gameSystems;

        internal static LifecycleSettings GetOrCreateDefault() {
            // ReSharper disable once JoinDeclarationAndInitializer
            LifecycleSettings settings;
#if UNITY_EDITOR
            settings = AssetDatabase.LoadAssetAtPath<LifecycleSettings>(SettingsPath);
#else
            settings = Resources.Load<LifecycleSettings>($"{nameof(LifecycleSettings)}.asset");
#endif

            if (settings != null) return settings;

#if UNITY_EDITOR
            settings = CreateInstance<LifecycleSettings>();
            if (!Directory.Exists(SettingsPath))
                Directory.CreateDirectory(Application.dataPath + SettingsPath.Remove(0, "Assets".Length));

            AssetDatabase.CreateAsset(settings, SettingsPath);
            AssetDatabase.SaveAssets();
            
            return settings;
#endif

            throw new Exception($"{nameof(LifecycleSettings)} could not be created.");
        }
    }
}