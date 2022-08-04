using Crosline.DebugTools;
using Crosline.UnityTools;
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

        [SerializeField]
        private float test1;
        [SerializeField]
        private float test2;
        [Space]
        [SerializeField]
        private float test3;
        [Separator()]
        [SerializeField]
        private float test4;
        [Separator(20)]
        [SerializeField]
        private float test5;
        [Separator(30)]
        [SerializeField]
        private float test6;

        private void Start() {
            CroslineDebug.LogError("hi");
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            CroslineGizmos.DrawPath(testPath, true, true);
        }
    }
}
