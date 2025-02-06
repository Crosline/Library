using Crosline.UnityTools;

namespace Crosline.ToolbarExtender {
    public class ToolbarDropdownAttribute : ToolbarAttribute {

        public string[] Options { get; private set; }

        public ToolbarDropdownAttribute(string[] options, ToolbarIcon toolbarIcon = ToolbarIcon.Default,
            string label = null,
            int order = 0,
            ToolbarZone toolbarZone = ToolbarZone.Left,
            string toolTip = null,
            object[] parameters = null) : base(toolbarIcon,
            label,
            order,
            toolbarZone,
            toolTip,
            parameters) {

            Options = options;

        }
    }
}
