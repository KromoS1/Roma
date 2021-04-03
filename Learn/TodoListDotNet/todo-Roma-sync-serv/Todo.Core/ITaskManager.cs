using System;
using System.Threading.Tasks;
using Todo.Entities;

namespace Todo.Core
{
    public interface ITaskManager
    {
        Task<string> AddTask(string name, int? priority, DateTime? start, DateTime? due, string status);
        Task<Tasks[]> GetAllTasks();
        Task<Tasks> GetTask(int? id);
        Task<string> UpdateTask(int? id, Tasks task);
        Task<string> DeleteTask(int? id);
    }
}