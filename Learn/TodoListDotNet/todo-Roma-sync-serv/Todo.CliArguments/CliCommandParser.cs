using System;
using System.Collections.Generic;
using System.Linq;
using Todo.CliArguments.Exceptions;

namespace Todo.CliArguments
{
    /// <summary>
    /// Command layout: commandName [OPTIONS] subject
    /// Command Name: any string like "add"
    /// Options: zero, one or more keys with optional values. Keys can be used via long form prefixed with double dash (--)
    /// or optional short form with single dash (-)
    /// Subject is optional string that should be last in command layout
    /// </summary>
    /// <example>
    /// add --dry --priority 1 -x Reminder | command: add, keys: [(dry), (priority, 1), (x)], subject: Reminder
    /// show --formatted                   | command: show, keys: [(formatted)], subject: null
    /// </example>
    public class CliCommandParser : ICliCommandParser
    {
        private const string LongNameArgumentPrefix = "--";
        private const string ShortNameArgumentPrefix = "-";
        
        private readonly List<CommandDefinition> _definedCommands = new List<CommandDefinition>();
        
        public CliCommand Parse(params string[] cliArguments)
        {
            if (cliArguments.Length == 0) throw new ArgumentException(nameof(cliArguments));

            var command = _definedCommands.FirstOrDefault(x => x.Name == cliArguments[0]);
            
            if (command == null) throw new CommandNotFoundException(cliArguments[0]);

            var arguments = new Dictionary<string, string?>();
            
            ArgumentDefinition? currentArgument = null;
            string? subject = null;
            for (var index = 1; index < cliArguments.Length; index++)
            {
                if (currentArgument != null)
                {
                    // Ready for value
                    var (type, value) = ExtractToken(cliArguments[index]);
                    if (type != TokenType.Value) throw new ExpectedValueException(currentArgument.Name);
                    
                    FlushCurrentArgument(value);
                }
                else
                {
                    // Ready for key or subject
                    var (type, value) = ExtractToken(cliArguments[index]);

                    switch (type)
                    {
                        case TokenType.LongKey:
                        {
                            currentArgument = command.Arguments.FirstOrDefault(x => x.Name == value) 
                                              ?? throw new ArgumentNotFoundException(value, false);
                            if (!currentArgument.IsValueExpected)
                            {
                                FlushCurrentArgument(null);
                            }
                            break;
                        }
                        case TokenType.ShortKey:
                        {
                            currentArgument = command.Arguments.FirstOrDefault(x => x.ShortName == value)
                                              ?? throw new ArgumentNotFoundException(value, true);
                            if (!currentArgument.IsValueExpected)
                            {
                                FlushCurrentArgument(null);
                            }
                            break;
                        }
                        case TokenType.Value:
                        {
                            if (index != cliArguments.Length - 1)
                            {
                                throw new ExpectedKeyException(value);
                            }

                            if (!command.IsSubjectExpected)
                            {
                                throw new NotExpectedSubjectException(value);
                            }

                            subject = value;
                            break;
                        }
                    }
                }
            }

            if (currentArgument != null)
            {
                throw new ExpectedValueException(currentArgument.Name);
            }

            ValidateSubject(subject);
            
            return new CliCommand(command.Name, arguments, subject);

            void FlushCurrentArgument(string? v)
            {
                ValidateArgumentValue(v);
                
                arguments.Add(currentArgument.Name, v);
                currentArgument = null;
            }

            void ValidateArgumentValue(string? v)
            {
                if (currentArgument.IsValueExpected)
                {
                    if (v == null) throw new ExpectedValueException(currentArgument.Name);
                    if (!currentArgument.Validate(v)) throw new ArgumentValidationException(currentArgument.Name, v);
                }
            }

            void ValidateSubject(string? s)
            {
                if (command.IsSubjectExpected)
                {
                    if (s == null) throw new ExpectedSubjectException();
                    if (!command.Validate(s)) throw new SubjectValidationException(s);
                }
            }
        }
        
        public void AddCommandDefinition(CommandDefinition commandDefinition)
        {
            _definedCommands.Add(commandDefinition);
        }

        public IEnumerable<CommandDefinition> GetDefinedCommands() => _definedCommands;

        private (TokenType type, string value) ExtractToken(string text)
        {
            if (text.StartsWith(LongNameArgumentPrefix)) return (TokenType.LongKey, text[2..]);
            if (text.StartsWith(ShortNameArgumentPrefix)) return (TokenType.ShortKey, text[1..]);

            return (TokenType.Value, text);
        }
        
        private enum TokenType
        {
            LongKey, ShortKey, Value
        }
    }
}