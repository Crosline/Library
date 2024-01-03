using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Crosline.UnityTools;
using UnityEngine;

namespace Crosline.Game.Lifecycle {
    public class LifecycleManager : PersistentSingleton<LifecycleManager> {

        [SerializeField]
        private LifecycleSettings _settings;

        private IReadOnlyDictionary<Type, GameSystemBase> _lifecycleSettings;

        private IReadOnlyList<IUpdateListener> _updateListeners;

        protected override void OnAwake() {
            var tempSettingsDictionary = new Dictionary<Type, GameSystemBase>();
            var tempUpdateListenerList = new List<IUpdateListener>();
            var settings = LifecycleSettings.GetOrCreateDefault();

            foreach (var system in settings.GameSystems) {
#if !CROSLINE_DEBUG
                if (system.IsDevelopmentOnly)
                    continue;
#endif
                var systemTemp = Activator.CreateInstance(system.GetType()) as GameSystemBase;
                tempSettingsDictionary[system.GetType()] = systemTemp;

                if (systemTemp is IUpdateListener updateListener)
                    tempUpdateListenerList.Add(updateListener);
            }

            _lifecycleSettings = tempSettingsDictionary;
            _updateListeners = tempUpdateListenerList;

            _ = InitializeSystems();
        }

        private void OnDestroy() {
            DisposeSystems();
        }

        private async Task InitializeSystems() {
            foreach (var system in _lifecycleSettings.Values) {
                Debug.Log($"Starting {system.GetType()}");
                await system.Initialize();
                Debug.Log($"Started {system.GetType()}");
            }
        }

        private void DisposeSystems() {
            foreach (var system in _lifecycleSettings.Values) system.Dispose();
        }

        public T GetSystem<T>() where T : GameSystemBase {
            var gameSystem = _lifecycleSettings[typeof(T)];

            if (gameSystem is T system)
                return system;

            throw new Exception($"Something went wrong on getting system {typeof(T)}");
        }

    }
}