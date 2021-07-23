using Sharprompt;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyList
{
    public class TodoMenu
    {
        private static IEnumerable<string> ValidateAdd(Dictionary<string, string> input)
        {
            if (string.IsNullOrWhiteSpace(input["label"]))
            {
                yield return "Label cannot be empty";
            }

            if (DateTimeOffset.Parse(input["duedate"]) < DateTime.UtcNow)
			{
                yield return "Due date cannot be in the past";
			}
        }

        private static bool Validate(TODOMENU command, Dictionary<string, string> input)
		{
            var errors = command switch {
                TODOMENU.Add => ValidateAdd(input),
                // register the rest of the options that need to be validated here
                _ => throw new InvalidOperationException($"No validator exists for {command}"),
            };
            
            if (errors.Any())
            {
                Console.WriteLine("The input has the following errors:");
                foreach (var error in errors)
                {
                    Console.WriteLine("\t"+error);
                }

                return false;
            }

            return true;
        }

        public static void Run()
        {
            ITodoService _todoService = Factory.CreateTodoServiceDB();
            while(true)
            {
                var action = Prompt.Select<TODOMENU>("Welcome to EasyList!");
                //Refactor this such that adding new should not modify this input layer
                switch (action)
                {
                    case TODOMENU.Add:
                        var inputAdd = Prompt.Input<string>("Enter TODO ");
                        var parsedAdd = ParseAdd.Parse(inputAdd?.Split() ?? Array.Empty<string>());

                        if (Validate(action, parsedAdd))
						{
                            var newTodo = new Todo(parsedAdd["label"],
                                    parsedAdd["description"],
                                    DateTimeOffset.Parse(parsedAdd["duedate"]),
                                    Enum.Parse<TodoPriority>(parsedAdd["priority"]));
                            _todoService.AddTodo(newTodo);
                        }
                        
                        break;

                    case TODOMENU.Delete:
                        var inputDelete = Prompt.Input<string>("Enter TODO ID(s) ");
                        foreach (var item in inputDelete.Split())
                        {
                            var todoItem = _todoService.GetTodoByID(int.Parse(item));
                            if(todoItem != null)
                            {
                                Console.WriteLine($"Deleted: {todoItem.Label}");
                                _todoService.DeleteTodo(todoItem);
                            }
                            else
                            {
                                Console.WriteLine($"Todo Id: {item} Not Found.");
                                Console.WriteLine("Try Again.");
                            }
                            
                        }
                        break;

                    case TODOMENU.View:
                        var inputView = Prompt.Input<int>("Enter TODO ID ");
                        var todo = _todoService.GetTodoByID(inputView);
                        if(todo != null)
                        {
                            _todoService.DisplayTodo(todo);
                        }
                        else
                        {
                            Console.WriteLine($"Todo Id: {inputView} Not Found.");
                        }
                        break;

                    case TODOMENU.MarkAsDone:
                        var inputDone = Prompt.Input<string>("Enter TODO ID(s) ");
                        foreach (var item in inputDone.Split())
                        {
                            var todoItem = _todoService.GetTodoByID(int.Parse(item));
                            if(todoItem != null)
                            {
                                Console.WriteLine($"Completed:{todoItem.Label}.");
                                _todoService.MarkTodoAsDone(todoItem);
                            }
                            else
                            {
                                Console.WriteLine($"Todo Id: {item} Not Found.");
                                Console.WriteLine("Try Again.");
                            }
                        }
                        break;

                    case TODOMENU.ListAll:
                        var inputList = Prompt.Select<TodoOrder>("Select List Order: ", defaultValue: TodoOrder.CreateDate);
                        _todoService.DisplayAllTodo(inputList);
                        break;
                }

                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}