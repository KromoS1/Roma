using System;
using Todo.CliArguments;

namespace Todo.Cli
{
    public class HelpService : IHelpService
    {
        private readonly ICliCommandParser _parser;

        public HelpService(ICliCommandParser parser)
        {
            _parser = parser;
        }
        
        public void PrintHelp()
        {
            var commands = _parser.GetDefinedCommands();
            
            Console.WriteLine("Supported commands:");
            foreach (var command in commands)
            {
                Console.Write($"\t{command.Name}");

                if (!string.IsNullOrWhiteSpace(command.HelpText))
                {
                    Console.WriteLine($" - {command.HelpText}");
                }
                
                Console.WriteLine();
                
                foreach (var argument in command.Arguments)
                {
                    Console.Write($"\t\t--{argument.Name}");

                    if (!string.IsNullOrWhiteSpace(argument.ShortName))
                    {
                        Console.Write($"\t-{argument.ShortName}");
                    }
                    
                    Console.WriteLine();

                    if (!string.IsNullOrWhiteSpace(command.HelpText))
                    {
                        Console.WriteLine($"\t\t\t{argument.HelpText}");
                    }
                }
            }
        }
    }
}