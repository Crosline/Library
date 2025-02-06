using UnityEngine;

namespace Subsystems.Core
{
    public class SubsystemManagerMonoHelper : MonoBehaviour
    {
        private void Awake()
        {
            if (SubsystemManager.Instance == null)
                return;

            SubsystemManager.OnAwake();
        }

        private void OnDestroy()
        {
            if (SubsystemManager.Instance == null)
                return;

            SubsystemManager.OnDestroy();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (SubsystemManager.Instance == null)
                return;

            SubsystemManager.OnApplicationFocus(hasFocus);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (SubsystemManager.Instance == null)
                return;

            SubsystemManager.OnApplicationPause(pauseStatus);
        }
    }
}