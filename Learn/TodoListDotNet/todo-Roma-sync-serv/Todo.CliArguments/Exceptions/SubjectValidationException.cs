using System;

namespace Todo.CliArguments.Exceptions
{
    public class SubjectValidationException : Exception
    {
        public string ActualSubject { get; }

        public SubjectValidationException(string actualSubject)
        {
            ActualSubject = actualSubject;
        }
    }
}