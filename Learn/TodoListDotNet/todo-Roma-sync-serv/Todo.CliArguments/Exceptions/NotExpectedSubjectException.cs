using System;

namespace Todo.CliArguments.Exceptions
{
    public class NotExpectedSubjectException : Exception
    {
        public string FoundToken { get; }

        public NotExpectedSubjectException(string foundToken)
        {
            FoundToken = foundToken;
        }
    }
}