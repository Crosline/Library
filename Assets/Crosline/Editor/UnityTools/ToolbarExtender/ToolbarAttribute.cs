using System;

namespace Crosline.UnityTools.Editor.ToolbarExtender {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ToolbarAttribute : Attribute {
        internal string toolName;
        internal string iconName;
        
        internal int order;

        internal Direction direction;

        public ToolbarAttribute(string toolName = "", string iconName = "", int order = 0, Direction direction = Direction.Left) {
            this.toolName = string.IsNullOrEmpty(toolName) ? "X" : toolName;
            this.iconName = string.IsNullOrEmpty(iconName) ? "d_Invalid" : iconName;
            this.order = order;
            this.direction = direction;
        }

    }
}