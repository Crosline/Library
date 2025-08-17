using System;
using Serializables;

namespace Subsystems
{
    public abstract class GameSubsystem<T> : GameSubsystem where T : GameSubsystem<T>
    { }

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