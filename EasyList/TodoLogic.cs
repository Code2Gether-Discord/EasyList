using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleTables;

namespace EasyList
{
    class TodoLogic
    {
        private ITodoRepository _todoCollection;
        public TodoLogic(ITodoRepository todoCollection)
        {
            _todoCollection = todoCollection;
        }

        public void Add(Todo todo)
        {
            _todoCollection.Add(todo);
        }
        public Todo GetByID(int ID)
        {
            return _todoCollection.Get(ID);
        }
        public void Delete(Todo todo)
        {
            _todoCollection.Delete(todo);
        }
        public void MarkAsDone(Todo todo)
        {
            todo.Status = TodoStatus.Done;
        }
        public void Display(Todo todo)
        {
            Console.WriteLine($"Id: {todo.Id}");
            Console.WriteLine($"Label: {todo.Label}");
            if (!string.IsNullOrEmpty(todo.Description))
                Console.WriteLine($"Description: {todo.Description}");
            Console.WriteLine($"Priority: {todo.Priority}");
            if (todo.DueDate != null)
                Console.WriteLine($"DueDate: {todo.DueDate}");
        }

        public void DisplayAllTodo()
        {
            ConsoleTable
            .From<Todo>(_todoCollection.GetAllTodo())
            .Configure(o => o.NumberAlignment = Alignment.Right)
            .Write(Format.Alternative);
        }
    }
}