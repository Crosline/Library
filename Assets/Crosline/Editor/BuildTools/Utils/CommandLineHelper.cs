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
                    
                    Debug.Log("[Builder][CommandLineHelper] Debug: commandLineArgs");

                    foreach (var VARIABLE in commandLineArgs) {
                        Debug.Log(VARIABLE);
                    }

                    var customArgs = commandLineArgs.SkipWhile(x => !x.Equals(ARGS)).Skip(1).FirstOrDefault()?.Split(ARGS_SEPARATOR);

                    CroslineDebug.Log("[Builder] Debug: Adding command line arguments:.");
                    foreach (var customArg in customArgs) {
                        var separatedArgs = customArg.Split(ARGS_EQUAL);
                        _commandLineArguments.Add(separatedArgs[0], separatedArgs[1]);
                        CroslineDebug.Log($"k: {separatedArgs[0]}, v: {separatedArgs[1]}");
                    }

                }

                if (_commandLineArguments.Count == 0)
                    CroslineDebug.LogError("[Builder] Error: Could not find any custom command line arguments.");

                return _commandLineArguments;
            }
        }

        public static string Argument(string arg) {
            var dictionary = Arguments;

            if (!dictionary.ContainsKey(arg)) {
                CroslineDebug.LogWarning($"[Builder] Warning: Command line argument: {arg} could not found.");
                return "";
            }
            
            return dictionary[arg];
        }

        public static bool ArgumentTrue(string arg) {
            var dictionary = Arguments;

            if (!dictionary.ContainsKey(arg)) {
                CroslineDebug.LogWarning($"[Builder] Warning: Command line argument: {arg} could not found.");
                return false;
            }

            return dictionary[arg].Equals("true") || dictionary[arg].Equals("1");
        }

        private static Dictionary<string, string> _commandLineArguments;
    }
}
