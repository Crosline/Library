using UnityEngine;

namespace Crosline.DebugTools.Gizmos.Editor {
    public static partial class CroslineGizmos {
        public static void DrawCross(Vector3 position, float size) {
            float d = size / 2.0f;
            Vector3 p = position;
            UnityEngine.Gizmos.DrawLine(p + Vector3.left * d, p + Vector3.right * d);
            UnityEngine.Gizmos.DrawLine(p + Vector3.up * d, p + Vector3.down * d);
            UnityEngine.Gizmos.DrawLine(p + Vector3.back * d, p + Vector3.forward * d);
        }
        
        public static void DrawArrow(Vector3 position, Vector3 direction, float size = 0.05f) {
            float d = size / 2.0f;
            direction = direction.normalized;
            
            UnityEngine.Gizmos.DrawLine(position + direction * d, position + Vector3.right * d);
            UnityEngine.Gizmos.DrawLine(position + Vector3.up * d, position + Vector3.down * d);
            UnityEngine.Gizmos.DrawLine(position + Vector3.back * d, position + Vector3.forward * d);
        }

        public static void DrawTriangle(Vector3 position, Vector3 direction, float size = 0.05f) {
            float k = 1.175f * 0.333f;
            Vector3 right = GizmosUtils.GetRightFromNormal(direction);
            
            direction = direction.normalized * size;
            right = right.normalized * size;

            UnityEngine.Gizmos.DrawLine(position - direction * k - right, position + direction * k * 2);
            UnityEngine.Gizmos.DrawLine(position + direction * k * 2, position - direction * k + right);
            UnityEngine.Gizmos.DrawLine(position - direction * k + right, position - direction * k - right);
        }
        
        public static void DrawTriangle(Vector3 position1, Vector3 position2, Vector3 position3) {
            
        }
    }
}
