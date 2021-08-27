using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleTableExt;
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
            var _todoList = _todoRepository.GetAllTodo(todoOrder)
                            .Select(x => new { x.Id, x.Label, x.Description, x.Priority, x.Status, CreateDate = x.CreatedDate.DateTime.ToLocalTime() , DueDate = x.DueDate?.DateTime.ToLocalTime() });

            ConsoleTableBuilder
            .From(_todoList.ToList())
            .WithCharMapDefinition(
                    CharMapDefinition.FramePipDefinition,
                    new Dictionary<HeaderCharMapPositions, char> {
                        {HeaderCharMapPositions.TopLeft, '╒' },
                        {HeaderCharMapPositions.TopCenter, '╤' },
                        {HeaderCharMapPositions.TopRight, '╕' },
                        {HeaderCharMapPositions.BottomLeft, '╞' },
                        {HeaderCharMapPositions.BottomCenter, '╪' },
                        {HeaderCharMapPositions.BottomRight, '╡' },
                        {HeaderCharMapPositions.BorderTop, '═' },
                        {HeaderCharMapPositions.BorderRight, '│' },
                        {HeaderCharMapPositions.BorderBottom, '═' },
                        {HeaderCharMapPositions.BorderLeft, '│' },
                        {HeaderCharMapPositions.Divider, '│' },
                    })
                .ExportAndWriteLine(TableAligntment.Center);
        }

        
    }
}