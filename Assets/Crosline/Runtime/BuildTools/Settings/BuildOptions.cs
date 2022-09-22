﻿namespace Crosline.BuildTools {
    public class BuildOptions {
        
        public enum BuildPlatform {
            Windows = 0,
            Linux = 1,
            MacOS = 2,
            Android = 3,
            IOS = 4,
            Mobile = Android | IOS,
            Standalone = Windows | Linux | MacOS,
            Generic = Standalone | Mobile
        }
        
        public enum ApiCompability {
            NetStandard21 = 0,
            NetFramework = 1
        }
        
        public enum Compression {
            None = 0,
            CompressWithLz4 = 1,
            CompressWithLz4HC = 2
        }

        public enum BuildMode {
            DEBUG = 0,
            RELEASE = 1
        }
    }
}
