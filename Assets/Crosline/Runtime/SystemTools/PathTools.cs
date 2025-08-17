using System;
using System.IO;
using System.Runtime.CompilerServices;
using Crosline.DebugTools;

namespace Crosline.SystemTools {
    public static class PathTools {
        public static char DirectorySeparatorChar
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Path.DirectorySeparatorChar;
        }

        public static char AltDirectorySeparatorChar
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Path.AltDirectorySeparatorChar;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsRelativePath(this string path) {
            return !Path.IsPathRooted(path);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFullPath(this string path) {
            return Path.IsPathRooted(path);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string FixDirectorySeparatorChars(this string path, bool isReverse = false) {
            if (path == null)
                return null;

            if (isReverse) {
                return path.Replace(DirectorySeparatorChar, AltDirectorySeparatorChar);
            }

            return path.Replace(AltDirectorySeparatorChar, DirectorySeparatorChar);
        }


        public static string MakeRelativePath(this string basePath, string filePath, bool ensureSucceeded = false) {
            CroslineDebug.AssertException(!string.IsNullOrEmpty(basePath), new ArgumentNullException(nameof(basePath)));
            CroslineDebug.AssertException(!string.IsNullOrEmpty(filePath), new ArgumentNullException(nameof(filePath)));

            var fromUri = new Uri(basePath!);
            var toUri = new Uri(filePath!);

            var relativeUri = fromUri.MakeRelativeUri(toUri);
            var relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            relativePath = relativePath.FixDirectorySeparatorChars();

            if (ensureSucceeded)
                if (!relativePath.IsRelativePath())
                    throw new Exception(
                        $"Relative path conversion failed. Input: '{filePath}'. Base: '{basePath}'. Failed result: '{relativePath}'.");

            return relativePath;
        }
    }
}