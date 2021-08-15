using EasyList.DataModels;
using EasyList.Enums;
using EasyList.Factories;
using EasyList.Interfaces;
using Sharprompt;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyList
{
    public class TodoMenu
    {
        public static void Run()
        {

            while(true)
            {
                var action = Prompt.Select<TODOMENU>("Welcome to EasyList!");
                //Refactor this such that adding new should not modify this input layer
                switch (action)
                {
                    case TODOMENU.Add:
                        var inputAdd = Prompt.Input<string>("Enter TODO ");
                        var parsedAdd = ParseAdd.Parse(inputAdd?.Split() ?? Array.Empty<string>());

                        Validate.Add(parsedAdd);

                        if (!Validate.TodoErrors())
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

                    case TODOMENU.Delete:
                        var inputDelete = Prompt.Input<string>("Enter TODO ID(s) ").Split();
                        
                        var validDeleteList = Validate.MultipleIds(inputDelete.ToIntIds());

                        if (!Validate.TodoErrors())
                        {
                            foreach (var todoDelete in validDeleteList)
                            {
                                Program.TodoService.DeleteTodo(todoDelete);
                            }                            
                        }
                        break;

                    case TODOMENU.View:
                        var inputView = Prompt.Input<int>("Enter TODO ID ");

                        var todoView = Validate.Id(inputView);

                        if(!Validate.TodoErrors())
                        {
                            Program.TodoService.DisplayTodo(todoView!);
                        }
                        break;

                    case TODOMENU.MarkAsDone:
                        var inputDone = Prompt.Input<string>("Enter TODO ID(s) ").Split();

                        var validTodoList = Validate.MultipleIds(inputDone.ToIntIds());

                        if (!Validate.TodoErrors())
                        {
                            foreach (var todoDelete in validTodoList)
                            {
                                Program.TodoService.MarkTodoAsDone(todoDelete);
                            }
                        }
                        break;
                    case TODOMENU.Update:
                        var inputUpdateAction = Prompt.Select<TodoUpdate>("Select Update Action");
                        var inputUpdate = Prompt.Input<int>("Enter the ID to Update");

                        var validTodo = Validate.Id(inputUpdate);

                        if (!Validate.TodoErrors())
                        {
                            Program.TodoService.UpdateTodo(validTodo!, inputUpdateAction);
                        }
                        break;
                    case TODOMENU.ListAll:
                        var inputList = Prompt.Select<TodoOrder>("Select List Order: ", defaultValue: TodoOrder.CreateDate);
                        Program.TodoService.DisplayAllTodo(inputList);
                        break;
                    case TODOMENU.Quit:
                        Console.WriteLine("Exiting...");
                        Environment.Exit(Environment.ExitCode = 0);
                        break;
                }

                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}