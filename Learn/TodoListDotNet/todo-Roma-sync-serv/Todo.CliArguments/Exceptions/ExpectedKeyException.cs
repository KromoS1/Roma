using System;

namespace Todo.CliArguments.Exceptions
{
    public class ExpectedKeyException : Exception
    {
        public string FoundToken { get; }

        public ExpectedKeyException(string foundToken)
        {
            FoundToken = foundToken;
        }
    }
}