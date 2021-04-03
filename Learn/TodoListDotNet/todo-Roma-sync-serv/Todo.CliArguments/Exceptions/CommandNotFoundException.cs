using System;

namespace Todo.CliArguments.Exceptions
{
    public class CommandNotFoundException : Exception
    {
        public string ExpectedCommandName { get; }
        
        public CommandNotFoundException(string expectedCommandName)
        {
            ExpectedCommandName = expectedCommandName;
        }
    }
}