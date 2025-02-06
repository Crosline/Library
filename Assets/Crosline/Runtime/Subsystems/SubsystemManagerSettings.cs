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
#if UNITY_EDITOR
                UnityEditor.AssetDatabase.CreateAsset(settings,
                    $"Assets/Settings/Resources/{nameof(SubsystemManagerSettings)}.asset");
#endif
            }

            return settings;
        }
    }
}