using System;
using System.Collections.Generic;
using System.Linq;
using Crosline.DebugTools;

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
                    
                    foreach (var customArg in customArgs) {
                        var separatedArgs = customArg.Split(ARGS_EQUAL);
                        _commandLineArguments.Add(separatedArgs[0], separatedArgs[1]);
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
