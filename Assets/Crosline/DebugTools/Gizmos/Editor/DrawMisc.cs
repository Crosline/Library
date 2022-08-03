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
        
        public static void DrawArrow(Vector3 position, Vector3 direction, float size = 0.1f) {
            float k = 1.175f * 0.33f;
            Vector3 right = GizmosUtils.GetRightFromNormal(direction);
            
            direction = direction.normalized * size * k;
            right = right.normalized * size * k;

            UnityEngine.Gizmos.DrawLine(position - direction - right, position + direction * 2);
            UnityEngine.Gizmos.DrawLine(position + direction * 2, position - direction + right);
            UnityEngine.Gizmos.DrawLine(position - direction + right, position - direction * 0.33f);
            UnityEngine.Gizmos.DrawLine(position - direction * 0.33f, position - direction - right);
        }

        public static void DrawTriangle(Vector3 position, Vector3 direction, float size = 0.1f) {
            float k = 1.175f * 0.33f;
            Vector3 right = GizmosUtils.GetRightFromNormal(direction);
            
            direction = direction.normalized * size * k;
            right = right.normalized * size * k;

            UnityEngine.Gizmos.DrawLine(position - direction - right, position + direction * 2);
            UnityEngine.Gizmos.DrawLine(position + direction * 2, position - direction + right);
            UnityEngine.Gizmos.DrawLine(position - direction + right, position - direction - right);
        }
        
        public static void DrawTriangle(Vector3 position1, Vector3 position2, Vector3 position3) {
            
        }
    }
}
