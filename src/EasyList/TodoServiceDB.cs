using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleTableExt;
using ConsoleTables;
using EasyList.DataModels;
using EasyList.Enums;
using EasyList.Factories;
using EasyList.Interfaces;

namespace EasyList
{
    internal class TodoServiceDB : ITodoService
    {
        private readonly ITodoLiteDBRepository _todoRepository;

        public TodoServiceDB()
        {
            _todoRepository = Factory.CreateDBRepository();
        }
        public void AddTodo(Todo todo)
        {
            _todoRepository.AddTodo(todo);
        }
        public Todo? GetTodoByID(int id)
        {
            return _todoRepository.GetTodo(id);
        }
        public void DeleteTodo(Todo todo)
        {
            _todoRepository.DeleteTodo(todo);
        }
        public void MarkTodoAsDone(Todo todo)
        {
            todo.Status = TodoStatus.Done;
            _todoRepository.UpdateTodo(todo);
        }
        public void DisplayTodo(Todo todo)
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
            int DisplayId = 1;

            var _todoList = _todoRepository.GetAllTodo(todoOrder)
                            .Select(x => new { x.Id, DisplayId = DisplayId++, x.Label, x.Description, x.Priority, x.Status, CreateDate = x.CreatedDate.DateTime.ToLocalTime(), DueDate = x.DueDate?.DateTime.ToLocalTime() });

            ConsoleTable
                 .From(_todoList.ToList())
                 .Configure(o => o.NumberAlignment = Alignment.Right)
                 .Write();
        }


    }
}