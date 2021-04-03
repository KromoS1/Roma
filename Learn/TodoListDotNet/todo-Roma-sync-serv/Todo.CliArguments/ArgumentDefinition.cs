using System;

namespace Todo.CliArguments
{
    public class ArgumentDefinition
    {
        private readonly Func<string, bool> _validation;
        
        public string Name { get; }
        public string? ShortName { get; }
        public string? HelpText { get; }
        public bool IsValueExpected { get; }
        
        public ArgumentDefinition(
            string name,
            string? shortName,
            string? helpText,
            bool isValueExpected,
            Func<string, bool>? validation = null)
        {
            Name = name;
            ShortName = shortName;
            HelpText = helpText;
            IsValueExpected = isValueExpected;
            _validation = validation ?? (_ => true);
        }

        public bool Validate(string value) => _validation(value);
    }
}