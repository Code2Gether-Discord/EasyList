using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleTables;
using EasyList.DataModels;
using EasyList.Enums;
using EasyList.Factories;
using EasyList.Interfaces;

namespace EasyList
{
    internal class TodoServiceInMemory : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        public TodoServiceInMemory()
        {
            _todoRepository = Factory.CreateInMemoryRepository();
        }
        public  void AddTodo(Todo todo)
        {
            _todoRepository.AddTodo(todo);
        }
        public  Todo? GetTodoByID(int id)
        {
            return _todoRepository.GetTodo(id);
        }
        public  void DeleteTodo(Todo todo)
        {
            _todoRepository.DeleteTodo(todo);
        }
        public  void MarkTodoAsDone(Todo todo)
        {
            todo.Status = TodoStatus.Done;
        }
        public  void DisplayTodo(Todo todo)
        {
            Console.WriteLine($"Id: {todo.Id}");
            Console.WriteLine($"Label: {todo.Label}");
            if (!string.IsNullOrWhiteSpace(todo.Description))
                Console.WriteLine($"Description: {todo.Description}");
            Console.WriteLine($"Priority: {todo.Priority}");
            if (todo.DueDate != null)
                Console.WriteLine($"DueDate: {todo.DueDate}");
        }
        public  void DisplayAllTodo(TodoOrder todoOrder)
        {
            ConsoleTable
            .From<Todo>(_todoRepository.GetAllTodo(todoOrder))
            .Configure(o => o.NumberAlignment = Alignment.Right)
            .Write(Format.Alternative);
        }

        
    }
}