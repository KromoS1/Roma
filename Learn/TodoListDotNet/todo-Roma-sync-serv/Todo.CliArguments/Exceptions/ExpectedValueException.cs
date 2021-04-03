using System;

namespace Todo.CliArguments.Exceptions
{
    public class ExpectedValueException : Exception
    {
        public string ArgumentName { get; }

        public ExpectedValueException(string argumentName)
        {
            ArgumentName = argumentName;
        }
    }
}