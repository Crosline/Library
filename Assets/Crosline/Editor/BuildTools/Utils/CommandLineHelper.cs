using System;
using System.Collections.Generic;
using System.Linq;
using Crosline.DebugTools;
using UnityEngine;

namespace Crosline.BuildTools.Editor {
    public static class CommandLineHelper {

        private const string ARGS = "-args";

        private const char ARGS_SEPARATOR = ';';

        private const char ARGS_EQUAL = '=';

        public static Dictionary<string, string> Arguments {
            get {
                _commandLineArguments ??= new Dictionary<string, string>();

                if (_commandLineArguments.Count == 0) {
                    var commandLineArgs = Environment.GetCommandLineArgs();

                    var customArgs = commandLineArgs.SkipWhile(x => !x.Equals(ARGS)).Skip(1).FirstOrDefault()?.Split(ARGS_SEPARATOR);

                    string argsDebug = "";
                    foreach (var customArg in customArgs) {
                        var separatedArgs = customArg.Split(ARGS_EQUAL);
                        _commandLineArguments.Add(separatedArgs[0], separatedArgs[1]);
                        argsDebug += $"k: {separatedArgs[0]}, v: {separatedArgs[1]}\n";
                    }
                    CroslineDebug.Log($"[Builder] Debug: Adding command line arguments:\n{argsDebug}");

                }

                if (_commandLineArguments.Count == 0)
                    CroslineDebug.LogError("[Builder] Error: Could not find any custom command line arguments.");

                return _commandLineArguments;
            }
        }

        public static string Argument(string arg) {
            if (!Arguments.ContainsKey(arg)) {
                CroslineDebug.LogError($"[Builder][CommandLineHelper] Warning: Command line argument: {arg} could not found.");
                return "";
            }
            
            return Arguments[arg];
        }

        public static bool ArgumentTrue(string arg) {
            if (!Arguments.ContainsKey(arg)) {
                CroslineDebug.LogError($"[Builder][CommandLineHelper] Warning: Command line argument: {arg} could not found.");
                return false;
            }

            return Arguments[arg].Equals("true") || Arguments[arg].Equals("1");
        }

        private static Dictionary<string, string> _commandLineArguments;
    }
}
