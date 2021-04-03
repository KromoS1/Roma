using System.Collections.Generic;

namespace Todo.CliArguments
{
    public class CliCommand
    {
        public string Name { get; }
        public Dictionary<string, string?> Arguments { get; }
        public string? Subject { get; }

        public CliCommand(string name, Dictionary<string, string?> arguments, string? subject)
        {
            Name = name;
            Arguments = arguments;
            Subject = subject;
        }

        public bool HasArgument(string argumentName)
        {
            return Arguments.ContainsKey(argumentName);
        }

        public string? GetArgument(string name)
        {
            return Arguments.TryGetValue(name, out var result) ? result : null;
        }
    }
}