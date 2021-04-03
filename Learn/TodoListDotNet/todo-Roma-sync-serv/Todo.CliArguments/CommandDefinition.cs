using System;

namespace Todo.CliArguments
{
    public class CommandDefinition
    {
        private readonly Func<string, bool> _validation;
        
        public string Name { get; }
        public string? HelpText { get; }
        public bool IsSubjectExpected { get; }
        public ArgumentDefinition[] Arguments { get; }

        public CommandDefinition(
            string name,
            string? helpText,
            bool isSubjectExpected,
            Func<string, bool>? validation = null,
            params ArgumentDefinition[] arguments)
        {
            Name = name;
            HelpText = helpText;
            IsSubjectExpected = isSubjectExpected;
            Arguments = arguments;
            _validation = validation ?? (_ => true);
        }
        
        public bool Validate(string value) => _validation(value);
    }
}