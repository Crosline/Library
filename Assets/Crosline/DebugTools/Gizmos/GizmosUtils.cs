using UnityEngine;

namespace Crosline.DebugTools.Gizmos {
    public static class GizmosUtils {
        public static (Vector3 left, Vector3 up) GetComponentsFromNormal(Vector3 normal, Transform t = null) {
            Vector3 left = GetRightFromNormal(normal, t) * -1f;
            Vector3 up = GetUpFromNormal(normal, t);

            return (left, up);
        }

        public static Vector3 GetRightFromNormal(Vector3 normal, Transform t = null) {
            Vector3 localUp = t != null ? t.up : Vector3.up;
            Vector3 localRight = t != null ? t.right : Vector3.right;

            Vector3 right = Vector3.Cross(normal, localUp * -1f).normalized;

            if (Mathf.Approximately(right.sqrMagnitude, 0f)) {
                right = localRight;
            }

            return right;
        }

        public static Vector3 GetUpFromNormal(Vector3 normal, Transform t = null) {
            Vector3 left = GetRightFromNormal(normal, t) * -1f;
            Vector3 up = Vector3.Cross(left, normal).normalized;

            Vector3 localForward = t != null ? t.forward : Vector3.forward;

            if (Mathf.Approximately(up.sqrMagnitude, 0f)) {
                up = localForward;
            }

            return up;
        }
    }
}
