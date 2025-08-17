using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Subsystems.Core
{
    public static class SubsystemManager
    {
        private static SubsystemManagerMonoHelper _monoHelper;

        private static SubsystemManagerSettings _settings;

        private static CancellationTokenSource _cancellationTokenSource;

        private static List<Type> _subsystemTypes;
        internal static List<GameSubsystem> Subsystems { get; private set; }
        
        public static bool IsInitialized { get; private set; }


        public static bool TryGet<TSubsystem>(out TSubsystem subsystem) where TSubsystem : GameSubsystem
        {
            try
            {
                subsystem = Get<TSubsystem>();
                return subsystem != null;
            }
            catch 
            {
                subsystem = null;
                return false;
            }
        }

        public static TSubsystem Get<TSubsystem>() where TSubsystem : GameSubsystem
        {
            var typeIndex = _subsystemTypes.IndexOf(typeof(TSubsystem));
            if (typeIndex == -1)
                throw new ArgumentException($"Subsystem of type {typeof(TSubsystem)} not found");

            return Subsystems[typeIndex] as TSubsystem;
        }

        private static bool TryRegister(Type type, GameSubsystem gameSubsystem)
        {
            if (_subsystemTypes.Contains(type)) return false;

            Subsystems.Add(gameSubsystem);
            _subsystemTypes.Add(type);
            return true;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnBeforeSceneLoad()
        {
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
            _cancellationTokenSource = new CancellationTokenSource();
            _settings = SubsystemManagerSettings.GetOrCreate();

            Subsystems = new List<GameSubsystem>();
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

            IsInitialized = true;
        }

        private static void Shutdown()
        {
            IsInitialized = false;

            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel(true);
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }

            foreach (var singleton in Subsystems) singleton.Shutdown();

            Subsystems.Clear();
            Subsystems = null;

            _subsystemTypes.Clear();
            _subsystemTypes = null;

            if (_monoHelper != null)
                Object.Destroy(_monoHelper.gameObject);
        }

        internal static void OnAwake()
        {
            foreach (var singleton in Subsystems)
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                singleton.OnAwake();
            }
        }

        internal static void OnDestroy()
        {
            foreach (var singleton in Subsystems)
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                singleton.OnDestroy();
            }
        }

        internal static void OnApplicationFocus(bool hasFocus)
        {
            foreach (var singleton in Subsystems)
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                singleton.OnApplicationFocus(hasFocus);
            }
        }

        internal static void OnApplicationPause(bool hasPaused)
        {
            foreach (var singleton in Subsystems)
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                singleton.OnApplicationPause(hasPaused);
            }
        }

        private static void __OnApplicationQuit()
        {
            foreach (var singleton in Subsystems)
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                singleton.OnApplicationQuit();
            }
        }


        #region Editor Callbacks

        [Conditional("UNITY_EDITOR")]
        internal static void ForceInitialize_Editor()
        {
            if (Application.isPlaying) return;
            Initialize();

            UnityEditor.EditorApplication.delayCall += () => Object.DestroyImmediate(_monoHelper.gameObject);

            UnityEditor.EditorApplication.playModeStateChanged += _ => { OnApplicationQuit(); };
        }

        [Conditional("UNITY_EDITOR")]
        internal static void ForceShutdown_Editor()
        {
            if (Application.isPlaying) return;

            OnApplicationQuit();
        }

        #endregion
    }
}