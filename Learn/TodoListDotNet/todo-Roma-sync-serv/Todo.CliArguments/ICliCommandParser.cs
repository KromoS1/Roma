using System.Collections.Generic;

namespace Todo.CliArguments
{
    public interface ICliCommandParser
    {
        CliCommand Parse(params string[] cliArguments);
        void AddCommandDefinition(CommandDefinition commandDefinition);
        IEnumerable<CommandDefinition> GetDefinedCommands();
    }
}