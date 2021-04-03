using System;

namespace Todo.CliArguments.Exceptions
{
    public class ArgumentValidationException : Exception
    {
        public string ArgumentName { get; }
        public string ActualValue { get; }

        public ArgumentValidationException(
            string argumentName, string actualValue)
        {
            ArgumentName = argumentName;
            ActualValue = actualValue;
        }
    }
}