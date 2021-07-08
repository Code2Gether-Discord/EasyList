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
                        var input_ADD = Prompt.Input<string>("Enter TODO ");
                        var parsed_ADD = ParseAdd.Parse(input_ADD.Split());
                        var newTodo = new Todo(parsed_ADD["label"],
                                        parsed_ADD["description"],
                                        DateTimeOffset.Parse(parsed_ADD["duedate"]),
                                        Enum.Parse<TodoPriority>(parsed_ADD["priority"]));
                        Program.todoApp.Add(newTodo);
                        break;

                    case TODOMENU.Delete:
                        var input_Delete = Prompt.Input<string>("Enter TODO ID(s) ");
                        foreach (var item in input_Delete.Split())
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
                        var input_View = Prompt.Input<int>("Enter TODO ID ");
                        var todo = Program.todoApp.GetByID(input_View);
                        if(todo != null)
                        {
                            Program.todoApp.Display(todo);
                        }
                        else
                        {
                            Console.WriteLine($"Todo Id: {input_View} Not Found.");
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
                        var input_List = Prompt.Select<TodoOrder>("Select List Order: ", defaultValue: TodoOrder.CreateDate);
                        Program.todoApp.DisplayAllTodo(input_List);
                        break;
                }

                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}