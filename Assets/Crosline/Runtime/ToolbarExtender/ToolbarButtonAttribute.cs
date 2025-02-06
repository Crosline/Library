using Crosline.UnityTools;
using UnityEngine.UIElements;

namespace Crosline.ToolbarExtender {
    public class ToolbarButtonAttribute : ToolbarAttribute {
        public ToolbarButtonAttribute(ToolbarIcon toolbarIcon = ToolbarIcon.Default,
            string label = null,
            int order = 0,
            ToolbarZone toolbarZone = ToolbarZone.LeftAlign,
            string toolTip = null,
            object[] parameters = null) : base(toolbarIcon,
            label,
            order,
            toolbarZone,
            toolTip,
            parameters) { }

        override internal VisualElement CreateVisualElement() {
            var buttonVE = new Button(() => { }) {
                text = Label,
                tooltip = ToolTip
            };

            return buttonVE;
        }
    }
}
