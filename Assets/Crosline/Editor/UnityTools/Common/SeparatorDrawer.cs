using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Crosline.UnityTools.Editor {

    [CustomPropertyDrawer(typeof(SeparatorAttribute))]
    public class SeparatorDrawer : DecoratorDrawer {

        private SeparatorAttribute _separatorAttr;

        private const float Thickness = 1f;

        private readonly Color _separatorColor = Color.white * 0.6f;

        public override void OnGUI(Rect position) {
            _separatorAttr ??= attribute as SeparatorAttribute;

            Rect separatorRect = new Rect(position.xMin, position.yMin + _separatorAttr.Spacing, position.width, Thickness);

            EditorGUI.DrawRect(separatorRect, Color.white * 0.8f);
        }

        public override float GetHeight() {
            _separatorAttr ??= attribute as SeparatorAttribute;

            return _separatorAttr.Spacing * 2f + Thickness;
        }
    }
}
