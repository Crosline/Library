using System;
using System.Collections.Generic;
using Crosline.UnityTools;

namespace Crosline.Game.Lifecycle {
    public class LifecycleManager : PersistentSingleton<LifecycleManager> {

        private IReadOnlyDictionary<Type, GameSystemBase> _lifecycleSettings;

        protected override void OnAwake() {
            var tempDictionary = new Dictionary<Type, GameSystemBase>();
            var settings = LifecycleSettings.GetOrCreateDefault();

            foreach (var system in settings.GameSystems) {
                tempDictionary[system.GetType()] = system;
            }

            _lifecycleSettings = tempDictionary;
        }

        public T GetSystem<T>() where T : GameSystemBase {
            var gameSystem = _lifecycleSettings[typeof(T)];
            
            if (gameSystem is T system)
                return system;

            throw new Exception($"Something went wrong on getting system {typeof(T)}");
        }
        
    }
}