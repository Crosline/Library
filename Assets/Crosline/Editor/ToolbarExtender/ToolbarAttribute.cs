using System;
using Crosline.UnityTools;

namespace Crosline.ToolbarExtender.Editor {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ToolbarAttribute : Attribute {
        internal string toolTip;
        internal string iconName;
        
        internal int order;

        internal ToolbarZone toolbarZone;

        public ToolbarAttribute(ToolbarZone toolbarZone = ToolbarZone.ToolbarZoneMiddleLeftAlign, string toolTip = "", string iconName = "", int order = 0) {
            this.toolTip = string.IsNullOrEmpty(toolTip) ? "X" : toolTip;
            this.iconName = string.IsNullOrEmpty(iconName) ? "d_Invalid" : iconName;
            this.order = order;
            this.toolbarZone = toolbarZone;
        }

    }
}