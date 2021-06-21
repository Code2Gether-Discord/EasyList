using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyList
{

	public static class TodoRepository 
    {
        //could use and let input layer handle numbering
		private static List<Todo> TodoList = new List<Todo>();
        
		public static Todo Add(Todo newTodo)
        {
            TodoList.Add(newTodo);
            return newTodo;
        }

        public static string Delete(int Id)
        {
            if (Id > 0)
            {
                var todo = TodoList[Id - 1];
                TodoList.Remove(todo);
                return todo.Label;
            }
            return string.Empty;           
        }

        public static Todo Get(int Id)
        {
            return TodoList[Id - 1]; 
            //handle exception
        }

        public static string MarkAsDone(int Id)
        {
            return Edit(Id:Id, status:TodoStatus.DONE).Label;
        }

        public static Todo Edit(int Id, 
                        string? Label = null, 
                        string? Description = null, 
                        DateTimeOffset? DueDate = null, 
                        TodoPriority priority = TodoPriority.LOW, 
                        TodoStatus status = TodoStatus.INPROGRESS)
        {
            if(Id > 0 )
            {
                TodoList[Id - 1].Label = Label ?? TodoList[Id - 1].Label;
                TodoList[Id - 1].Description = Description;
                TodoList[Id - 1].DueDate = DueDate ?? TodoList[Id - 1].DueDate;
                TodoList[Id - 1].Priority = priority;
                TodoList[Id - 1].Status = status;
                return TodoList[Id - 1];
            }
            //catch Exception here
            return null;
        }

        public static IEnumerable<Todo> GetAllTask(TodoOrder order = TodoOrder.CREATEDATE)
        {
            IEnumerable<Todo> OrderedList;
            if (order == TodoOrder.DUEDATE)
            {
                OrderedList = TodoList.Where(_todo => _todo.Status == TodoStatus.INPROGRESS)
                                      .OrderByDescending(_todo => _todo.DueDate);
            }
            else if (order == TodoOrder.PRIORITY)
            {
                OrderedList = TodoList.Where(_todo => _todo.Status == TodoStatus.INPROGRESS)
                                      .OrderByDescending(_todo => _todo.Priority);
            }
            else
            {
                OrderedList = TodoList.Where(_todo => _todo.Status == TodoStatus.INPROGRESS)
                                      .OrderByDescending(_todo => _todo.CreatedDate);
            }
            return OrderedList;
        }        
    }
}

