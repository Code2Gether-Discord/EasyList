using System;
using System.Collections.Generic;

namespace EasyList
{
	public class Repository : ITaskRepository //ensure only one such instance is present
	{
		public static Dictionary<int, TaskItem> TaskList = new Dictionary<int, TaskItem>();//unit        
		public void Add(string taskLabel)
        {
            var newTask = new TaskItem(taskLabel);
            TaskList.Add(newTask.Id, newTask);
        }

        public void Delete(int Id)
        {
            if(!TaskList.ContainsKey(Id))
            {
                Console.WriteLine($"No Task with ID: {Id} Found");
                return;
            }
            TaskList.Remove(Id);
        }

        public void Read(int Id)
        {
            TaskList[Id].Display(Id);//Do better
        }

        public void Done(int Id)
        {
            if (!TaskList.ContainsKey(Id))
            {
                Console.WriteLine($"No Task eith ID: {Id} Found");
                return;
            }
            TaskList[Id].Status = TaskItem.TaskItemStatus.DONE; 
        }

        public void ListAllTask()
        {
            foreach (var task in TaskList)
            {
                TaskList[task.Key].Display(task.Key);//Do better
            }
        }

        
    }
}

