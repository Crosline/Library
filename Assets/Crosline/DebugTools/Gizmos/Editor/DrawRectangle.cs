using UnityEngine;

namespace Crosline.DebugTools.Gizmos.Editor {
    public static partial class CroslineGizmos {
        
        /// <param name="normalSize">
        /// Specifies the normal with magnitude being the size of the sides.
        /// </param>
        public static void DrawSquare(Vector3 origin, Vector3 normalSize) {
            float size = normalSize.magnitude;
            DrawRectangle(origin, normalSize, size, size);
        }
        
        public static void DrawSquare(Vector3 origin, Vector3 normal, float size = 1f) {
            DrawRectangle(origin, normal, size, size);
        }

        public static void DrawRectangle(Vector3 origin, Vector3 normal,
            float width, float height) {
            (Vector3 left, Vector3 up) = GizmosUtils.GetComponentsFromNormal(normal);

            UnityEngine.Gizmos.DrawLine(origin - up * height - left * width, origin - up * height + left * width);
            UnityEngine.Gizmos.DrawLine(origin - up * height + left * width, origin + up * height + left * width);
            UnityEngine.Gizmos.DrawLine(origin + up * height + left * width, origin + up * height - left * width);
            UnityEngine.Gizmos.DrawLine(origin + up * height - left * width, origin - up * height - left * width);
        }
    }
}
