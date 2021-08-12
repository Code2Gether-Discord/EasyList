using System;
using ConsoleTables;
using EasyList.DataModels;
using EasyList.Enums;
using EasyList.Factories;
using EasyList.Interfaces;
using Sharprompt;

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
            .Write(Format.MarkDown);
        }

        public void UpdateTodo(Todo todo, TodoUpdate command)
        {
            switch (command)
            {
                case TodoUpdate.Label:
                    {
                        Console.WriteLine("Enter the New Label: ");
                        string? newLabel = Console.ReadLine();
                        if (Validate.Label(newLabel))
                        {
                            if (!Validate.TodoErrors())
                            {
                                todo.Label = newLabel!;
                            }
                        }
                        break;
                    }

                case TodoUpdate.Description:
                    {
                        Console.WriteLine("Enter the New Description: ");
                        string? newDescription = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newDescription))
                        {
                            todo.Description = newDescription;
                        }
                        break;
                    }

                case TodoUpdate.Priority:
                    {
                        var newPriority = Prompt.Select<TodoPriority>("Select new Priority.");
                        todo.Priority = newPriority;
                        break;
                    }

                case TodoUpdate.DueDate:
                    {
                        Console.WriteLine("Enter the New Due Date: ");
                        string? newDueDate = Console.ReadLine();
                        if (Validate.DueDate(newDueDate))
                        {
                            if (!Validate.TodoErrors())
                            {
                                todo.DueDate = DateTimeOffset.Parse(newDueDate!);
                            }
                        }
                        break;
                    }
            }
        }
    }
}