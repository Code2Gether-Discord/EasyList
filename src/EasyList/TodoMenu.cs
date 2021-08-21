using EasyList.DataModels;
using EasyList.Enums;
using Sharprompt;
using System;
using System.Collections.Generic;

namespace EasyList
{
    public class TodoMenu
    {

        public static void Run()
        {
            var _todoValidate = new Validate();
            while (true)
            {
                var action = Prompt.Select<TODOMENU>("Welcome to EasyList!");
                //Refactor this such that adding new should not modify this input layer
                switch (action)
                {
                    
                    case TODOMENU.Add:
                        {
                            var inputAdd = Prompt.Input<string>("Enter TODO ");
                            var parsedAdd = ParseAdd.Parse(inputAdd?.Split() ?? Array.Empty<string>());
                            
                            if (_todoValidate.IsAddValid(parsedAdd))
                            {
                                var newTodo = new Todo()
                                {
                                    Label = parsedAdd["label"],
                                    Description = parsedAdd["description"],
                                    DueDate = DateTimeOffset.TryParse(parsedAdd["duedate"], out DateTimeOffset tempDate) ? tempDate : null,
                                    Priority = Enum.Parse<TodoPriority>(parsedAdd["priority"])
                                };
                                Program.TodoService.AddTodo(newTodo);
                            }
                            break;
                        }
                    case TODOMENU.Delete:
                        var inputDelete = Prompt.Input<string>("Enter TODO ID(s) ").Split();

                        if (_todoValidate.IsDeleteValid(inputDelete,out List<Todo> todoDeleteList))
                        {
                            foreach (var todoDelete in todoDeleteList)
                            {
                                Program.TodoService.DeleteTodo(todoDelete);
                            }                            
                        }
                        break;

                    case TODOMENU.View:
                        {
                            var inputView = Prompt.Input<string>("Enter TODO ID ");
                            
                            if (_todoValidate.IsReadValid(inputView, out Todo validTodo))
                            {
                                Program.TodoService.DisplayTodo(validTodo);
                            }
                            break;
                        }
                    case TODOMENU.MarkAsDone:
                        {
                            var inputDone = Prompt.Input<string>("Enter TODO ID(s) ").Split();

                            if (_todoValidate.CanMarkAsDone(inputDone, out List<Todo> doneTodoList))
                            {
                                foreach (var doneTodo in doneTodoList)
                                {
                                    Program.TodoService.MarkTodoAsDone(doneTodo);
                                }
                            }
                            break;
                        }
                    case TODOMENU.Update:
                        {
                            var inputUpdateAction = Prompt.Select<TodoUpdate>("Select Update Action");
                            var inputUpdate = Prompt.Input<string>("Enter the ID to Update");
                            
                            if (_todoValidate.IsReadValid(inputUpdate, out Todo validTodo))
                            {
                                Program.TodoService.UpdateTodo(validTodo, inputUpdateAction);
                            }
                            break;
                        }
                    case TODOMENU.ListAll:
                        {
                            var inputList = Prompt.Select<TodoOrder>("Select List Order: ", defaultValue: TodoOrder.CreateDate);
                            Program.TodoService.DisplayAllTodo(inputList);
                            break;
                        }
                    case TODOMENU.Quit:
                        {
                            Console.WriteLine("Exiting...");
                            Environment.Exit(Environment.ExitCode = 0);
                            break;
                        }
                }

                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}