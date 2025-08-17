using System;
using System.Diagnostics;
using System.IO;
using Serializables;
using UnityEngine;

namespace Subsystems.Core
{
    [CreateAssetMenu(fileName = nameof(SubsystemManagerSettings), menuName = "AAA/SubsystemManagerSettings")]
    public class SubsystemManagerSettings : ScriptableObject
    {
        [field: SerializeField] public SerializableType<GameSubsystem>[] Subsystems { get; private set; }

        public static SubsystemManagerSettings GetOrCreate()
        {
            var settings = Resources.Load<SubsystemManagerSettings>(nameof(SubsystemManagerSettings));

            if (settings == null)
            {
                settings = CreateInstance<SubsystemManagerSettings>();

                settings.Subsystems = Array.Empty<SerializableType<GameSubsystem>>();
                
                CreateSettingsAsset(settings);
            }

            return settings;
        }

        [Conditional("UNITY_EDITOR")]
        private static void CreateSettingsAsset(SubsystemManagerSettings settings) {
            if (!Directory.Exists("Assets/Settings/Resources"))
                Directory.CreateDirectory("Assets/Settings/Resources");

            UnityEditor.AssetDatabase.CreateAsset(settings,
                $"Assets/Settings/Resources/{nameof(SubsystemManagerSettings)}.asset");
        }
    }
}