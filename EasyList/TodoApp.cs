using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleTables;

namespace EasyList
{
    class TodoApp
    {
        private readonly ITodoRepository _todoRepository;
        public TodoApp(ITodoRepository todoCollection)
        {
            _todoRepository = todoCollection;
        }

        public void Add(Todo todo)
        {
            _todoRepository.Add(todo);
        }
        public Todo? GetByID(int id)
        {
            return _todoRepository.Get(id);
        }
        public void Delete(Todo todo)
        {
            _todoRepository.Delete(todo);
        }
        public void MarkAsDone(Todo todo)
        {
            todo.Status = TodoStatus.Done;
            _todoRepository.Update(todo);
        }
        public void Display(Todo todo)
        {
            Console.WriteLine($"Id: {todo.Id}");
            Console.WriteLine($"Label: {todo.Label}");
            if (!string.IsNullOrWhiteSpace(todo.Description))
                Console.WriteLine($"Description: {todo.Description}");
            Console.WriteLine($"Priority: {todo.Priority}");
            if (todo.DueDate != null)
                Console.WriteLine($"DueDate: {todo.DueDate}");
        }

        public void DisplayAllTodo(TodoOrder todoOrder)
        {
            ConsoleTable
            .From<Todo>(_todoRepository.GetAllTodo(todoOrder))
            .Configure(o => o.NumberAlignment = Alignment.Right)
            .Write(Format.Alternative);
        }
    }
}