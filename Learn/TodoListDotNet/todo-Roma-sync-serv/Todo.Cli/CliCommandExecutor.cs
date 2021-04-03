using System;
using System.Threading.Tasks;
using Todo.CliArguments;
using Todo.Core;
using Todo.Entities;

namespace Todo.Cli
{
    public class CliCommandExecutor : ICliCommandExecutor
    {
        private readonly IHelpService _helpService;
        private readonly ITaskManager _taskManager;
        private readonly IShowData _showData;

        public CliCommandExecutor(IHelpService helpService, IShowData showData, ITaskManager taskManager)
        {
            _helpService = helpService;
            _showData = showData;
            _taskManager = taskManager;
        }
        
        public async Task Execute(CliCommand command)
        {
            switch (command.Name)
            {
                case "help":
                {
                    _helpService.PrintHelp();
                    break;
                }

                case "get":
                    var idForGet = StringParseHelper.GetInt(command.GetArgument("id"));
                    if (idForGet != null)
                        _showData.ShowTasks(_taskManager.GetTask(idForGet).Result);
                    else
                        _showData.ShowTasks(_taskManager.GetAllTasks().Result);

                    break;

                case "create":
                {
                    var name = command.Subject;
                    var priority = StringParseHelper.GetInt(command.GetArgument("priority"));
                    var start = StringParseHelper.GetDate(command.GetArgument("start"));
                    var due = StringParseHelper.GetDate(command.GetArgument("due"));
                    var status = StringParseHelper.GetString(command.GetArgument("status"));

                    var taskAdded = await _taskManager.AddTask(name, priority, start, due, status);
                    Console.WriteLine(taskAdded);
                    break;
                }

                case "update":
                    var idUpdate = StringParseHelper.GetInt(command.GetArgument("id"));
                    var priorityUpDate = StringParseHelper.GetInt(command.GetArgument("priority"));
                    var dueUpdate = StringParseHelper.GetDate(command.GetArgument("due"));
                    var statusUpDate = StringParseHelper.GetString(command.GetArgument("status"));
                    var taskUpdate = new Tasks
                    {
                        Priority = priorityUpDate, Due = dueUpdate, Status = statusUpDate
                    };
                    var resultUpdate = await _taskManager.UpdateTask(idUpdate, taskUpdate);
                    Console.WriteLine(resultUpdate);
                    break;

                case "delete":
                    var id = StringParseHelper.GetInt(command.GetArgument("id"));
                    var deleted= await _taskManager.DeleteTask(id);
                    Console.WriteLine(deleted);
                    break;
                default:
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}