using System.ComponentModel;

namespace Crosline.UnityTools {
    public enum ToolbarZone {
        
        [Description("ToolbarZoneLeftAlign")]
        LeftAlign = 1 << 0,
        [Description("ToolbarZoneLeftAlign")]
        MiddleLeftAlign = 1 << 1,
        
        [Description("ToolbarZoneRightAlign")]
        MiddleRightAlign = 1 << 2,
        [Description("ToolbarZoneRightAlign")]
        RightAlign = 1 << 3,
        
        
        Middle = MiddleLeftAlign | MiddleRightAlign,
        Left = LeftAlign | MiddleLeftAlign,
        Right = RightAlign | MiddleRightAlign
    }
}