using System;
using Crosline.DebugTools.Gizmos.Editor;

using UnityEngine;
namespace Crosline.Subsystems.Builder.Editor.Builders
{
    public class Test : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.DrawCross()
        }
    }
}
