using System;
using System.Collections.Generic;
using Crosline.DebugTools;

namespace Crosline.BuildTools.Editor {
    public static class CommandLineBuffer {

        private const string ARGS = "-args";
        private const char ARGS_SEPARATOR = ';';
        private const char ARGS_EQUAL = '=';

        public static Dictionary<string, string> Arguments {
            get {
                _commandLineArguments ??= new Dictionary<string, string>();

                if (_commandLineArguments.Count == 0) {
                    var commandLineArgs = Environment.GetCommandLineArgs();

                    for (var i = 0; i < commandLineArgs.Length; i += 1)
                        if (commandLineArgs[i].Equals(ARGS)) {
                            var customArgs = commandLineArgs[i + 1].Split(ARGS_SEPARATOR);

                            foreach (var customArg in customArgs) {
                                var separatedArgs = customArg.Split(ARGS_EQUAL);
                                _commandLineArguments.Add(separatedArgs[0], separatedArgs[1]);
                            }

                            break;
                        }
                }

                if (_commandLineArguments.Count == 0)
                    CroslineDebug.LogError("Could not find any custom command line arguments.");

                return _commandLineArguments;
            }
        }

        private static Dictionary<string, string> _commandLineArguments;
    }
}
