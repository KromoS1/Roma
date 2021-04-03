using System;
using System.Threading.Tasks;
using Todo.Entities;
using Todo.RestTaskRepository;

namespace Todo.Core
{
    public class TaskManager : ITaskManager
    {
        private readonly ITaskRepository _taskRepository;

        public TaskManager(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Task<Tasks[]> GetAllTasks()
        {
            return _taskRepository.GetAllTasks();
        }

        public Task<Tasks> GetTask(int? id)
        {
            return _taskRepository.GetTask(id);
        }

        public Task<string> AddTask(string name, int? priority, DateTime? start, DateTime? due, string status)
        {
            var task = new Tasks
            {
                Name = name,
                Priority = priority,
                Start = start,
                Due = due,
                Status = status
            };
           return _taskRepository.CreateTask(task);
        }

        public Task<string> UpdateTask(int? id, Tasks task)
        {
            return _taskRepository.UpdateTask(id, task);
        }

        public Task<string> DeleteTask(int? id)
        {
           return _taskRepository.Delete(id);
        }

    }
}