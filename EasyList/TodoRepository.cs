using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyList
{
	public class TodoRepository : ITodoRepository //ensure only one such instance is present 
    {
        //could use and let input layer handle numbering
		private static List<Todo> TodoList = new List<Todo>();
        
		public Todo Add(Todo newTodo)
        {
            TodoList.Add(newTodo);
            return newTodo;
        }

        public string Delete(int Id)
        {
            if (Id > 1)
            {
                var todoLabel = TodoList[Id - 1].Label;
                TodoList.Remove(Get(Id));
                return todoLabel;
            }
            return string.Empty;           
        }

        public Todo Get(int Id)
        {
            return TodoList[Id - 1]; 
            //handle exception
        }

        public Todo MarkAsDone(int Id)
        {
            return Edit(Id:Id, status:Todo.TodoStatus.DONE);
        }

        public Todo Edit(int Id, 
                        string Label = null, 
                        string Description = null, 
                        DateTimeOffset? DueDate = null, 
                        Todo.TodoPriority priority = Todo.TodoPriority.LOW, 
                        Todo.TodoStatus status = Todo.TodoStatus.INPROGRESS)
        {
            if(Id > 1)
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

        public IEnumerable<Todo> GetAllTask(ITodoRepository.TodoOrder order = ITodoRepository.TodoOrder.CREATEDATE)
        {
            IEnumerable<Todo> OrderedList;
            if (order == ITodoRepository.TodoOrder.DUEDATE)
            {
                OrderedList = TodoList.Where(_todo => _todo.Status == Todo.TodoStatus.INPROGRESS)
                                      .OrderByDescending(_todo => _todo.DueDate);
            }
            else if (order == ITodoRepository.TodoOrder.PRIORITY)
            {
                OrderedList = TodoList.Where(_todo => _todo.Status == Todo.TodoStatus.INPROGRESS)
                                      .OrderByDescending(_todo => _todo.Priority);
            }
            else
            {
                OrderedList = TodoList.Where(_todo => _todo.Status == Todo.TodoStatus.INPROGRESS)
                                      .OrderByDescending(_todo => _todo.CreatedDate);
            }
            return OrderedList;
        }        
    }
}

