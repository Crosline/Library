using UnityEngine;

namespace Subsystems.Core
{
    public class SubsystemManagerMonoHelper : MonoBehaviour
    {
        private void Awake()
        {
            if (!SubsystemManager.IsInitialized)
                return;

            SubsystemManager.OnAwake();
        }

        private void OnDestroy()
        {
            if (!SubsystemManager.IsInitialized)
                return;

            SubsystemManager.OnDestroy();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!SubsystemManager.IsInitialized)
                return;

            SubsystemManager.OnApplicationFocus(hasFocus);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (!SubsystemManager.IsInitialized)
                return;

            SubsystemManager.OnApplicationPause(pauseStatus);
        }
    }
}