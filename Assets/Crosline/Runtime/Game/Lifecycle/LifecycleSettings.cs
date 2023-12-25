using UnityEngine;

namespace Crosline.Game.Lifecycle {
    internal class LifecycleSettings : ScriptableObject {

        public const string SettingsPath = "Assets/Crosline/Runtime/Game/Resources/LifecycleSettings.asset";

        [field: SerializeReference]
        public GameSystemBase[] GameSystems { get; private set; }
    }
}