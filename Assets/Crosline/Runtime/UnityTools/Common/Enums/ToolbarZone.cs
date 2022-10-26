namespace Crosline.UnityTools {
    public enum ToolbarZone {
        ToolbarZoneLeftAlign = 1 << 0,
        ToolbarZoneMiddleLeftAlign = 1 << 1,
        ToolbarZoneMiddleRightAlign = 1 << 2,
        ToolbarZoneRightAlign = 1 << 3,
        
        
        Middle = ToolbarZoneMiddleLeftAlign | ToolbarZoneMiddleRightAlign,
        Left = ToolbarZoneLeftAlign | ToolbarZoneMiddleLeftAlign,
        Right = ToolbarZoneRightAlign | ToolbarZoneMiddleRightAlign
    }
}