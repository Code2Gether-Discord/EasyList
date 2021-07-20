using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleTables;

namespace EasyList
{
    internal class TodoServiceDB : TodoService
    {
        private readonly ITodoLiteDBRepository _todoRepository;

        public TodoServiceDB()
        {
            _todoRepository = CreateRepository();
        }

        public override ITodoLiteDBRepository CreateRepository()
        {
            return new TodoLiteDbRepository();
        }
        public override void Add(Todo todo)
        {
            _todoRepository.Add(todo);
        }
        public override Todo? GetByID(int id)
        {
            return _todoRepository.GetTodo(id);
        }
        public override void Delete(Todo todo)
        {
            _todoRepository.Delete(todo);
        }
        public override void MarkAsDone(Todo todo)
        {
            todo.Status = TodoStatus.Done;
            _todoRepository.Update(todo);
        }
        public override void Display(Todo todo)
        {
            Console.WriteLine($"Id: {todo.Id}");
            Console.WriteLine($"Label: {todo.Label}");
            if (!string.IsNullOrWhiteSpace(todo.Description))
                Console.WriteLine($"Description: {todo.Description}");
            Console.WriteLine($"Priority: {todo.Priority}");
            if (todo.DueDate != null)
                Console.WriteLine($"DueDate: {todo.DueDate}");
        }
        public override void DisplayAllTodo(TodoOrder todoOrder)
        {
            ConsoleTable
            .From<Todo>(_todoRepository.GetAllTodo(todoOrder))
            .Configure(o => o.NumberAlignment = Alignment.Right)
            .Write(Format.Alternative);
        }

        
    }
}