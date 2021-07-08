using Sharprompt;
using System;
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
                        var parsedAdd = ParseAdd.Parse(inputAdd.Split());
                        var newTodo = new Todo(parsedAdd["label"],
                                        parsedAdd["description"],
                                        DateTimeOffset.Parse(parsedAdd["duedate"]),
                                        Enum.Parse<TodoPriority>(parsedAdd["priority"]));
                        Program.todoApp.Add(newTodo);
                        break;

                    case TODOMENU.Delete:
                        var inputDelete = Prompt.Input<string>("Enter TODO ID(s) ");
                        foreach (var item in inputDelete.Split())
                        {
                            var todoItem = Program.todoApp.GetByID(int.Parse(item));
                            if(todoItem != null)
                            {
                                Console.WriteLine($"Deleted: {todoItem.Label}");
                                Program.todoApp.Delete(todoItem);
                            }
                            else
                            {
                                Console.WriteLine($"Todo Id: {item} Not Found.");
                            }
                            
                        }
                        break;

                    case TODOMENU.View:
                        var inputView = Prompt.Input<int>("Enter TODO ID ");
                        var todo = Program.todoApp.GetByID(inputView);
                        if(todo != null)
                        {
                            Program.todoApp.Display(todo);
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
                            var todoItem = Program.todoApp.GetByID(int.Parse(item));
                            if(todoItem != null)
                            {
                                Console.WriteLine($"Completed:{todoItem.Label}.");
                                Program.todoApp.MarkAsDone(todoItem);
                            }
                            else
                            {
                                Console.WriteLine($"Todo Id: {item} Not Found.");
                            }
                        }
                        break;

                    case TODOMENU.ListAll:
                        var inputList = Prompt.Select<TodoOrder>("Select List Order: ", defaultValue: TodoOrder.CreateDate);
                        Program.todoApp.DisplayAllTodo(inputList);
                        break;
                }

                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}