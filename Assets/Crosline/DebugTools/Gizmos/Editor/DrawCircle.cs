using UnityEngine;

namespace Crosline.DebugTools.Gizmos.Editor {
    public static partial class Gizmos {
        /// <param name="normalRadius">
        /// Specifies the normal with magnitude being the radius.
        /// </param>
        public static void DrawCircle(Vector3 origin, Vector3 normalRadius, int segments = 32) {
            (var left, var up) = GizmosUtils.GetComponentsFromNormal(normalRadius);

            var radius = normalRadius.magnitude;

            for (var i = 0; i < segments; i++) {
                var theta0 = 2f * Mathf.PI * (float) i / segments;
                var theta1 = 2f * Mathf.PI * (float) (i + 1) / segments;

                var x0 = radius * Mathf.Cos(theta0);
                var y0 = radius * Mathf.Sin(theta0);
                var x1 = radius * Mathf.Cos(theta1);
                var y1 = radius * Mathf.Sin(theta1);

                UnityEngine.Gizmos.DrawLine(origin + left * x0 + up * y0, origin + left * x1 + up * y1);
            }
        }
        
        public static void DrawCircle(Vector3 origin, Vector3 normal, float size, int segments = 32) {
            DrawCircle(origin, normal * size, segments);
        }
        
        public static void WireCylinder(Vector3 origin, float radius, float height, int segments=16) {
            
            Vector3 top = origin + Vector3.up * height / 2f;
            Vector3 bot = origin - Vector3.up * height / 2f;

            for (int i = 0; i < segments; i++) {
                float theta0 = 2f * Mathf.PI * (float) i / segments;
                float theta1 = 2f * Mathf.PI * (float) (i+1) / segments;

                float x0 = radius * Mathf.Cos(theta0);
                float y0 = radius * Mathf.Sin(theta0);
                float x1 = radius * Mathf.Cos(theta1);
                float y1 = radius * Mathf.Sin(theta1);

                Vector3 left = Vector3.left;
                Vector3 fore = Vector3.forward;
                // Top circle
                UnityEngine.Gizmos.DrawLine(
                    top + left * x0 + fore * y0,
                    top + left * x1 + fore * y1);
                // Bottom circle
                UnityEngine.Gizmos.DrawLine(
                    bot + left * x0 + fore * y0,
                    bot + left * x1 + fore * y1);
                // Sides
                UnityEngine.Gizmos.DrawLine(
                    top + left * x0 + fore * y0,
                    bot + left * x0 + fore * y0);
            }
        }
    }
}
