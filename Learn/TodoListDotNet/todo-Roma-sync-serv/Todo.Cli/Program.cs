using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Todo.CliArguments;
using Todo.CliArguments.Exceptions;

namespace Todo.Cli
{
    class Program
    {
        public static async Task Main (string[] args)
        {
            var serviceProvider = DependencyConfiguration.BuildServiceProvider();
            
            if (args.Length == 0)
            {
                serviceProvider.GetRequiredService<IHelpService>().PrintHelp();
                return;
            }
            
            PrintArgs(args);
            
            try
            {
                var cliCommandParser = serviceProvider.GetRequiredService<ICliCommandParser>();
                var command = cliCommandParser.Parse(args);

                PrintCommand(command);
                
                var commandExecutor = serviceProvider.GetRequiredService<ICliCommandExecutor>();
                await commandExecutor.Execute(command);
            }
            catch (ArgumentNotFoundException e)
            {
                var argName = e.IsShort ? "-" + e.Name : "--" + e.Name;
                Console.WriteLine($"Key {argName} not found");
            }
            catch (ArgumentValidationException e)
            {
                Console.WriteLine($"Invalid key value --{e.ArgumentName} = {e.ActualValue}");
            }
            catch (CommandNotFoundException e)
            {
                Console.WriteLine($"Command {e.ExpectedCommandName} not found");
            }
            catch (ExpectedKeyException e)
            {
                Console.WriteLine($"Expected key, but found {e.FoundToken}");
            }
            catch (ExpectedSubjectException)
            {
                Console.WriteLine("Expected subject");
            }
            catch (ExpectedValueException e)
            {
                Console.WriteLine($"Expected value for key --{e.ArgumentName}");
            }
            catch (NotExpectedSubjectException e)
            {
                Console.WriteLine($"Not expected subject, but found {e.FoundToken}");
            }
            catch (SubjectValidationException e)
            {
                Console.WriteLine($"Invalid subject: {e.ActualSubject}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unable to parse command: {e.Message}");
            }
        }

        private static void PrintArgs(string[] args)
        {
            Console.WriteLine($"Input arguments: [{string.Join(", ", args)}]");
        }

        private static void PrintCommand(CliCommand command)
        {
            Console.WriteLine($"Command Name: {command.Name}");

            if (command.Arguments.Any())
            {
                Console.WriteLine($"Arguments: {string.Join(", ", command.Arguments.Select(x => x.Value != null ? $"({x.Key}, {x.Value})" : $"({x.Key})"))}");
            }

            if (command.Subject != null)
            {
                Console.WriteLine($"Subject: {command.Subject}");
            }
        }
    }
}