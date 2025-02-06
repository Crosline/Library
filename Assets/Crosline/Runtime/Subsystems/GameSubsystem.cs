using System;
using Serializables;

namespace Subsystems
{
    public abstract class GameSubsystem<T> : GameSubsystem where T : GameSubsystem<T>
    {
        static T Instance { get; set; }

        public override void Initialize()
        {
            Instance ??= this as T;
        }

        public override void Shutdown()
        {
            if (Instance != this) return;

            Instance = null;
        }


        public override void OnAwake()
        {
        }

        public override void OnDestroy()
        {
        }

        public override void OnApplicationQuit()
        {
        }

        public override void OnApplicationFocus(bool hasFocus)
        {
        }

        public override void OnApplicationPause(bool hasPaused)
        {
        }
    }

    [Serializable]
    public abstract class GameSubsystem
    {
        public abstract void Initialize();

        public abstract void Shutdown();

        public abstract void OnAwake();

        public abstract void OnDestroy();

        public abstract void OnApplicationQuit();

        public abstract void OnApplicationFocus(bool hasFocus);

        public abstract void OnApplicationPause(bool hasPaused);
    }
}