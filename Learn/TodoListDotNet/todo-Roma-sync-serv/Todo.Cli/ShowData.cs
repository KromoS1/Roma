using System;
using System.Collections.Generic;
using Todo.Entities;

namespace Todo.Cli
{
    public class ShowData : IShowData
    {
        public void ShowTasks(IEnumerable<Tasks> tasks)
        {
            foreach (var task in tasks)
            {
                if (task.Due != null)
                {
                    Console.WriteLine($"ID: {task.ID}, Name: {task.Name}, Priority: {task.Priority}" +
                                      $", End time : {task.Due:g}, Status: {task.Status}.");
                }
                else
                {
                    Console.WriteLine($"ID: {task.ID}, Name: {task.Name}, Priority: {task.Priority}, Status: {task.Status}.");
                }
            }
        }

        public void ShowTasks(Tasks task)
        {
            ShowTasks(new []{task});
        }
    }
}
