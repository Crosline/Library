using UnityEngine;

namespace Crosline.DebugTools.Gizmos.Editor {
    public static class GizmosUtils {
        public static (Vector3 left, Vector3 up) GetComponentsFromNormal(Vector3 normal) {
            Vector3 left = Vector3.Cross(normal, Vector3.up).normalized;
            Vector3 up = Vector3.Cross(left, normal).normalized;

            if (Mathf.Approximately(left.sqrMagnitude, 0f)) {
                left = Vector3.left;
                up = Vector3.forward;
            }

            return (left, up);
        }
    }
}
