namespace Crosline.BuildTools {
    public class BuildOptions {
        
        public enum BuildPlatform {
            Windows = 1 << 0,
            Linux = 1 << 1,
            MacOS = 1 << 2,
            Android = 1 << 3,
            IOS = 1 << 4,
            Mobile = Android | IOS,
            Standalone = Windows | Linux | MacOS,
            Generic = Standalone | Mobile
        }
        
        public enum ApiCompability {
            NetStandard21 = 1 << 0,
            NetFramework = 1 << 1
        }
        
        public enum Compression {
            None = 1 << 0,
            CompressWithLz4 = 1 << 1,
            CompressWithLz4HC = 1 << 2
        }

        public enum BuildMode {
            DEBUG = 1 << 0,
            RELEASE = 1 << 1
        }
    }
}
