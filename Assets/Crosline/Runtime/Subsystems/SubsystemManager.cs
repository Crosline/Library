using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Subsystems.Core
{
    public sealed class SubsystemManager
    {
        private static SubsystemManagerMonoHelper _monoHelper;

        private static SubsystemManagerSettings _settings;

        private static CancellationTokenSource _cancellationTokenSource;

        private static List<Type> _subsystemTypes;
        private static List<GameSubsystem> _subsystems;
        internal static List<GameSubsystem> Subsystems => _subsystems;
        internal static SubsystemManager Instance { get; private set; }


        public static bool TryGetInstanceWithoutError<TSubsystem>(out TSubsystem subsystem) where TSubsystem : GameSubsystem
        {
            try
            {
                subsystem = TryGetInstance<TSubsystem>();
                return subsystem != null;
            }
            catch 
            {
                subsystem = null;
                return false;
            }
        }

        public static TSubsystem TryGetInstance<TSubsystem>() where TSubsystem : GameSubsystem
        {
            var typeIndex = _subsystemTypes.IndexOf(typeof(TSubsystem));
            if (typeIndex == -1)
                throw new ArgumentException($"Subsystem of type {typeof(TSubsystem)} not found");

            return _subsystems[typeIndex] as TSubsystem;
        }

        public static bool TryRegister<TSubsystem>(TSubsystem gameSubsystem) where TSubsystem : GameSubsystem
        {
            return TryRegister(typeof(TSubsystem), gameSubsystem);
        }

        private static bool TryRegister(Type type, GameSubsystem gameSubsystem)
        {
            if (_subsystemTypes.Contains(type)) return false;

            _subsystems.Add(gameSubsystem);
            _subsystemTypes.Add(type);
            return true;
        }

        public static bool TryUnregister<TSubsystem>(TSubsystem gameSubsystem) where TSubsystem : GameSubsystem
        {
            if (!_subsystemTypes.Contains(typeof(TSubsystem))) return false;

            _subsystems.Remove(gameSubsystem);
            _subsystemTypes.Remove(typeof(TSubsystem));
            return true;
        }


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnBeforeSceneLoad()
        {
            if (Instance != null) return;

            Application.quitting += OnApplicationQuit;
            Initialize();
        }

        private static void OnApplicationQuit()
        {
            Application.quitting -= OnApplicationQuit;
            __OnApplicationQuit();
            Shutdown();
        }

        private static void Initialize()
        {
            Instance = new SubsystemManager();
            _cancellationTokenSource = new CancellationTokenSource();
            _settings = SubsystemManagerSettings.GetOrCreate();

            _subsystems = new List<GameSubsystem>();
            _subsystemTypes = new List<Type>();

            foreach (var gameSubsystem in _settings.Subsystems)
            {
                var typeInfo = gameSubsystem.TypeInfo;
                if (typeInfo == null) continue;

                if (Activator.CreateInstance(typeInfo) is not GameSubsystem singletonObject)
                    continue;

                if (!TryRegister(typeInfo, singletonObject))
                    continue;

                singletonObject.Initialize();
            }

            _monoHelper = new GameObject(nameof(SubsystemManagerMonoHelper))
                .AddComponent<SubsystemManagerMonoHelper>();
        }

        private static void Shutdown()
        {
            Instance = null;

            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel(true);
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }

            foreach (var singleton in _subsystems) singleton.Shutdown();

            _subsystems.Clear();
            _subsystems = null;

            _subsystemTypes.Clear();
            _subsystemTypes = null;

            if (_monoHelper != null)
                Object.Destroy(_monoHelper.gameObject);
        }

        internal static void OnAwake()
        {
            foreach (var singleton in _subsystems)
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                singleton.OnAwake();
            }
        }

        internal static void OnDestroy()
        {
            foreach (var singleton in _subsystems)
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                singleton.OnDestroy();
            }
        }

        internal static void OnApplicationFocus(bool hasFocus)
        {
            foreach (var singleton in _subsystems)
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                singleton.OnApplicationFocus(hasFocus);
            }
        }

        internal static void OnApplicationPause(bool hasPaused)
        {
            foreach (var singleton in _subsystems)
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                singleton.OnApplicationPause(hasPaused);
            }
        }

        private static void __OnApplicationQuit()
        {
            foreach (var singleton in _subsystems)
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                singleton.OnApplicationQuit();
            }
        }


        #region Editor Callbacks

#if UNITY_EDITOR
        internal static void ForceInitialize_Editor()
        {
            if (Application.isPlaying) return;
            if (Instance != null) return;
            Initialize();

            UnityEditor.EditorApplication.delayCall += () => Object.Destroy(_monoHelper.gameObject);

            UnityEditor.EditorApplication.playModeStateChanged += _ => { OnApplicationQuit(); };
        }

        internal static void ForceShutdown_Editor()
        {
            if (Application.isPlaying) return;
            if (Instance == null) return;

            OnApplicationQuit();
        }
#endif

        #endregion
    }
}