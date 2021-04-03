using System;

namespace Todo.CliArguments
{
    public class ArgumentDefinitionBuilder
    {
        private readonly string _name;
        private string? _shortName;
        private string? _helpText;
        private bool _isValueExpected;
        private Func<string, bool>? _validation;

        public ArgumentDefinitionBuilder(string name)
        {
            _name = name;
        }
        
        public static ArgumentDefinitionBuilder NewArgument(string name)
        {
            return new ArgumentDefinitionBuilder(name);
        }

        public ArgumentDefinitionBuilder WithShortName(string shortName)
        {
            _shortName = shortName;

            return this;
        }

        public ArgumentDefinitionBuilder WithHelpText(string helpText)
        {
            _helpText = helpText;

            return this;
        }

        public ArgumentDefinitionBuilder WithValueExpected(Func<string, bool> validation = null)
        {
            _isValueExpected = true;
            _validation = validation;

            return this;
        }

        public ArgumentDefinition Build()
        {
            return new ArgumentDefinition(_name, _shortName, _helpText, _isValueExpected, _validation);
        }
    }
}