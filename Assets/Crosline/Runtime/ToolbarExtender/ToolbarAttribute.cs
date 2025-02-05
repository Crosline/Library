using System;
using Crosline.UnityTools;
using Crosline.UnityTools.Attributes;

namespace Crosline.ToolbarExtender {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class ToolbarAttribute : MethodAttribute {
        public ToolbarIcon ToolbarIcon { get; private set; }
        public string Label { get; private set; }
        public int Order { get; private set; }
        public ToolbarZone ToolbarZone { get; private set; }
        public string ToolTip { get; private set; }

        protected ToolbarAttribute(string label = null, ToolbarIcon toolbarIcon = ToolbarIcon.Default,
            int order = 0, ToolbarZone toolbarZone = ToolbarZone.MiddleRightAlign, string toolTip = null, params object[] parameters) : base(parameters) {

            Label = label;
            ToolbarIcon = toolbarIcon;
            Order = order;
            ToolbarZone = toolbarZone;
            ToolTip = toolTip;
        }

    }
}