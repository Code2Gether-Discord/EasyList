using EasyList.DataModels;
using EasyList.Enums;
using EasyList.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace EasyList
{
    class TodoRepository : ITodoRepository
    {
		private static readonly List<Todo> todoList = new();
        
		public void AddTodo(Todo todo)
        {
            todoList.Add(todo);
        }
        public  Todo? GetTodo(int Id)
        {
            if(todoList.Count == 0 || Id > todoList.Count)
            {
                return null;
            }
            return todoList[Id - 1]; 
        }
        public IEnumerable<Todo> GetAllTodo(TodoOrder order = TodoOrder.CreateDate)
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
        public void DeleteTodo(Todo todo)
        {
            todoList.Remove(todo);
        }
    }
}

