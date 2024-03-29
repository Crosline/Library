﻿using UnityEngine;

namespace Crosline.UnityTools.Attributes {

    [System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true)]
    public class SeparatorAttribute : PropertyAttribute {

        public readonly float Spacing;

        public SeparatorAttribute(float spacing = 10f) {
            Spacing = spacing;
        }
    }
}
