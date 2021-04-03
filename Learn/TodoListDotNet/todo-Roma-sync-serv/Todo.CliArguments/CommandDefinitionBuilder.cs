using System;
using System.Collections.Generic;

namespace Todo.CliArguments
{
    public class CommandDefinitionBuilder
    {
        private readonly string _name;
        private string? _helpText;
        private readonly List<ArgumentDefinition> _definedArguments = new List<ArgumentDefinition>();
        private bool _isSubjectExpected;
        private Func<string, bool>? _validation;
        
        public CommandDefinitionBuilder(string name)
        {
            _name = name;
        }
        
        public static CommandDefinitionBuilder NewCommand(string name)
        {
            return new CommandDefinitionBuilder(name);
        }

        public CommandDefinitionBuilder WithHelpText(string helpText)
        {
            _helpText = helpText;

            return this;
        }

        public CommandDefinitionBuilder WithArgument(ArgumentDefinition argument)
        {
            _definedArguments.Add(argument);

            return this;
        }

        public CommandDefinitionBuilder WithExpectedSubject(Func<string, bool> validation = null)
        {
            _isSubjectExpected = true;
            _validation = validation;

            return this;
        }
        
        public CommandDefinition Build()
        {
            return new CommandDefinition(_name, _helpText, _isSubjectExpected, _validation, _definedArguments.ToArray());
        }
    }
}