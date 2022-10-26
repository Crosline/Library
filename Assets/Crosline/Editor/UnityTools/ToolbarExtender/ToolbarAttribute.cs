using System;

namespace Crosline.UnityTools.Editor.ToolbarExtender {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ToolbarAttribute : Attribute {
        internal string toolTip;
        internal string iconName;
        
        internal int order;

        internal Direction direction;

        public ToolbarAttribute(Direction direction = Direction.Left, string toolTip = "", string iconName = "", int order = 0) {
            this.toolTip = string.IsNullOrEmpty(toolTip) ? "X" : toolTip;
            this.iconName = string.IsNullOrEmpty(iconName) ? "d_Invalid" : iconName;
            this.order = order;
            this.direction = direction;
        }

    }
}