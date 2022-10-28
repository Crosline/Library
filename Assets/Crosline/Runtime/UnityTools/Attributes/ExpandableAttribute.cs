using UnityEngine;

namespace Crosline.UnityTools.Attributes {
    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ExpandableAttribute : PropertyAttribute {
    }
}
