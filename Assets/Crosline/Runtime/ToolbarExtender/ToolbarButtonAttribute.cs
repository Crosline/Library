using Crosline.UnityTools;

namespace Crosline.ToolbarExtender {
    public class ToolbarButtonAttribute : ToolbarAttribute {
        public ToolbarButtonAttribute(string label = null, ToolbarIcon toolbarIcon = ToolbarIcon.Default, int order = 0,
            ToolbarZone toolbarZone = ToolbarZone.MiddleRightAlign, string toolTip = null, params object[] parameters)
            : base(label, toolbarIcon, order, toolbarZone, toolTip, parameters) {
            
        }
    }
}
