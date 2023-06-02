using System;
using System.IO;
using System.Linq;
using Crosline.DebugTools;

namespace Crosline.SystemTools {
    public static class DirectoryTools {
        public static bool IsDirectoryEmpty(string path) {
            return Directory.EnumerateDirectories(path)?.Any() != true;
        }

        public static bool? IsDirectoryEmpty(this DirectoryInfo directory) {
            return directory?.EnumerateFileSystemInfos().Any() != true;
        }

        public static bool CreateDirectory(string directoryPath) {
            if (string.IsNullOrEmpty(directoryPath) || Directory.Exists(directoryPath))
                return false;

            Directory.CreateDirectory(directoryPath);
            return true;
        }

        public static bool CreateDirectoryFromFile(string filePath) => CreateDirectory(Path.GetDirectoryName(filePath));


        private static bool DeleteNonRecursive(string path) {

            try {
                Directory.Delete(path, false);
            }
            catch (Exception e) when (e is IOException or UnauthorizedAccessException) {
                CroslineDebug.LogError($"Could not delete the directory: {path}\nException: {e}");
                return false;
            }

            return true;
        }

        public static bool DeleteIfEmpty(string path) {
            if (!IsDirectoryEmpty(path)) {
                return false;
            }

            return DeleteNonRecursive(path);
        }

        public static void DeleteEmptySubdirectories(string directoryPath) {

            foreach (var subdirectory in Directory.GetDirectories(directoryPath)) {
                DeleteEmptySubdirectories(subdirectory);
            }

            DeleteIfEmpty(directoryPath);
        }
    }
}