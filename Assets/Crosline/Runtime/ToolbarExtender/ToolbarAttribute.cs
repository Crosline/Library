using System;
using Crosline.UnityTools;
using Crosline.UnityTools.Attributes;
using UnityEngine.UIElements;

namespace Crosline.ToolbarExtender {
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class ToolbarAttribute : MethodAttribute {
        public ToolbarIcon ToolbarIcon { get; private set; }

        public string Label { get; private set; }

        public int Order { get; private set; }

        public ToolbarZone ToolbarZone { get; private set; }

        public string ToolTip { get; private set; }

        protected ToolbarAttribute(ToolbarIcon toolbarIcon = ToolbarIcon.Default,
            string label = null,
            int order = 0,
            ToolbarZone toolbarZone = ToolbarZone.Left,
            string toolTip = null,
            object[] parameters = null) : base(parameters) {

            ToolbarIcon = toolbarIcon;
            Label = label;
            Order = order;
            ToolbarZone = toolbarZone;
            ToolTip = toolTip;
        }


        internal abstract VisualElement CreateVisualElement();

    }
}
