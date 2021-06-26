using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyList
{

	public class TodoRepository 
    {
		private static List<Todo> todoList = new List<Todo>();
        
		public static void Add(Todo todo)
        {
            todoList.Add(todo);
        }

        public static void Delete(Todo todo)
        {
                todoList.Remove(todo);
        }

        public static Todo Get(int Id)
        {
            return todoList[Id - 1]; 
            //handle exception
        }

        public static void MarkAsDone(Todo todo)
        {
            Edit(todo.Id, status:TodoStatus.Done);
        }

        public static void Edit(int Id, 
                        string? Label = null, 
                        string? Description = null, 
                        DateTimeOffset? DueDate = null, 
                        TodoPriority priority = TodoPriority.Low, 
                        TodoStatus status = TodoStatus.InProgress)
        {
                var todo = Get(Id);
                todo.Label = Label ?? todo.Label;
                todo.Description = Description;
                todo.DueDate = DueDate ?? todo.DueDate;
                todo.Priority = priority;
                todo.Status = status;
        }

        public static IEnumerable<Todo> GetAllTask(TodoOrder order = TodoOrder.CreateDate)
        {
            IEnumerable<Todo> orderedList;
            switch (order)
            {
                case TodoOrder.DueDate:
                    {
                        orderedList = todoList.Where(_todo => _todo.Status == TodoStatus.InProgress)
                                              .OrderByDescending(_todo => _todo.DueDate);
                        break;
                    }

                case TodoOrder.Priority:
                    {
                        orderedList = todoList.Where(_todo => _todo.Status == TodoStatus.InProgress)
                                              .OrderByDescending(_todo => _todo.Priority);
                        break;
                    }

                default:
                    {
                        orderedList = todoList.Where(_todo => _todo.Status == TodoStatus.InProgress)
                                              .OrderByDescending(_todo => _todo.CreatedDate);
                        break;
                    }
            }
            return orderedList;
        }        
    }
}

