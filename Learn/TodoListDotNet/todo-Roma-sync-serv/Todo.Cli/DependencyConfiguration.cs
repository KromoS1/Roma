using System;
using Microsoft.Extensions.DependencyInjection;
using MoreLinq;
using Todo.CliArguments;
using Todo.Core;
using static Todo.CliArguments.CommandDefinitionBuilder;
using static Todo.CliArguments.ArgumentDefinitionBuilder;
using Todo.RestTaskRepository;

namespace Todo.Cli
{
    public static class DependencyConfiguration
    {
        public static IServiceProvider BuildServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            return serviceCollection.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ICliCommandParser>(_ =>
            {
                var parser = new CliCommandParser();
                Configure(parser);
                return parser;
            });
            serviceCollection.AddSingleton<ICliCommandExecutor, CliCommandExecutor>();
            serviceCollection.AddSingleton<IHelpService, HelpService>();
            serviceCollection.AddSingleton<ITaskManager, TaskManager>();
            serviceCollection.AddSingleton<ITaskRepository, TaskRepository>();
            serviceCollection.AddSingleton<IShowData, ShowData>();
        }

        private static void Configure(CliCommandParser cliCommandParser)
        {
            var commands = new[]
            {
                NewCommand("create")
                   .WithHelpText("Adds task to list.")
                   .WithArgument(
                        NewArgument("priority")
                           .WithShortName("p")
                           .WithHelpText("Specifies priority of task.")
                           .WithValueExpected(x => int.TryParse(x, out _))
                           .Build())
                   .WithArgument(
                        NewArgument("due")
                           .WithShortName("d")
                           .WithHelpText("Specifies time when task should be done.")
                           .WithValueExpected()
                           .Build())
                   .WithArgument(
                        NewArgument("start")
                           .WithShortName("s")
                           .WithHelpText("Specifies time when task should start.")
                           .WithValueExpected()
                           .Build())
                   .WithArgument(
                        NewArgument("status")
                            .WithShortName("ss")
                            .WithHelpText("indicates the status of the task at the current time.")
                            .WithValueExpected()
                            .Build())
                   .WithExpectedSubject()
                   .Build(),

                NewCommand("update")
                    .WithArgument(
                        NewArgument("id")
                            .WithShortName("i")
                            .WithValueExpected(x=>int.TryParse(x, out _))
                            .Build())
                    .WithArgument(
                        NewArgument("status")
                            .WithShortName("ss")
                            .WithHelpText("indicates the status of the task at the current time")
                            .WithValueExpected()
                            .Build())
                    .WithArgument(
                        NewArgument("priority")
                            .WithShortName("p")
                            .WithValueExpected(x=>int.TryParse(x, out _))
                            .Build())
                    .WithArgument(
                        NewArgument("due")
                            .WithShortName("d")
                            .WithHelpText("Specifies time when task should be done.")
                            .WithValueExpected()
                            .Build())
                    .Build(),

                NewCommand("delete")
                    .WithArgument(
                        NewArgument("id")
                            .WithShortName("i")
                            .WithHelpText("Deletes an ID task")
                            .WithValueExpected(x=>int.TryParse(x, out _))
                            .Build())
                    .Build(),
                   
                NewCommand("get")
                   .WithHelpText("Shows list of tasks.")
                   .WithArgument(
                       NewArgument("id")
                           .WithShortName("i")
                           .WithValueExpected(x=>int.TryParse(x, out _))
                           .Build())
                   .Build(),
                
                NewCommand("help")
                   .Build()
            };

            commands.ForEach(cliCommandParser.AddCommandDefinition);
        }
    }
}