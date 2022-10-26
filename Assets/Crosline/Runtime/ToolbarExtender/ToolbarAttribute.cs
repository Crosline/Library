using System;
using Crosline.UnityTools;
using Crosline.UnityTools.Attributes;

namespace Crosline.ToolbarExtender {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ToolbarAttribute : MethodAttribute {
        public string toolTip;
        public string iconName;
        
        public int order;

        public ToolbarZone toolbarZone;
        
        public ToolbarAttribute(params object[] parameters) : base(parameters) {
            
            this.toolTip = "";
            this.iconName = string.IsNullOrEmpty(iconName) ? "d_BuildSettings.Broadcom" : iconName;
            this.order = 0;
            this.toolbarZone = ToolbarZone.ToolbarZoneRightAlign;
        }

        public ToolbarAttribute(string iconName = "", params object[] parameters) : base(parameters) {
            
            this.toolTip = "";
            this.iconName = string.IsNullOrEmpty(iconName) ? "d_BuildSettings.Broadcom" : iconName;
            this.order = 0;
            this.toolbarZone = ToolbarZone.ToolbarZoneRightAlign;
        }
        
        public ToolbarAttribute(ToolbarZone toolbarZone = ToolbarZone.ToolbarZoneMiddleLeftAlign,
            string toolTip = "", string iconName = "", int order = 0, params object[] parameters) : base(parameters) {
            
            this.toolTip = toolTip;
            this.iconName = string.IsNullOrEmpty(iconName) ? "d_BuildSettings.Broadcom" : iconName;
            this.order = order;
            this.toolbarZone = toolbarZone;
        }

    }
}