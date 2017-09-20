using System;
using System.Collections.Generic;

namespace DotvvmAcademy.Lessons.Collections.ViewModels
{
    public class ToDoListViewModel
    {
        public ToDoListViewModel()
        {
            Tasks = new List<TaskData>();
        }

        public string AddedTaskTitle { get; set; }

        public List<TaskData> Tasks { get; set; }

        public void AddTask()
        {
            Tasks.Add(new TaskData() { Title = AddedTaskTitle });
            AddedTaskTitle = "";
        }

        public void CompleteTask(TaskData task)
        {
            task.IsCompleted = true;
        }
    }
}