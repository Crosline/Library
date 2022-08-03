using Crosline.DebugTools.Gizmos.Editor;
using UnityEngine;

namespace Crosline
{
    public class MonoTester : MonoBehaviour {
        private Vector3[] testPath = {
            Vector3.zero,
            Vector3.one,
            Vector3.one * 2f,
            Vector3.one * 3f
        };
        
        private void OnDrawGizmosSelected() {
            //CroslineGizmos.DrawCircle(transform.position, transform.up)
            Gizmos.color = Color.blue;
            CroslineGizmos.DrawPath(testPath, true);
        }
    }
}
