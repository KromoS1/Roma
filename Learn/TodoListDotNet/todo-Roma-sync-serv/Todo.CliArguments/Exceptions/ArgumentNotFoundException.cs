using System;

namespace Todo.CliArguments.Exceptions
{
    public class ArgumentNotFoundException : Exception
    {
        public string Name { get; }
        public bool IsShort { get; }

        public ArgumentNotFoundException(string name, bool isShort)
        {
            Name = name;
            IsShort = isShort;
        }
    }
}