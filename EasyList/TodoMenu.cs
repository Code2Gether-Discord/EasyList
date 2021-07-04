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
                            var todoId = Program.todoApp.GetByID(int.Parse(item));
                            Console.WriteLine($"Deleted: {todoId.Label}");
                            Program.todoApp.Delete(todoId);
                        }
                        break;

                    case TODOMENU.View:
                        var input_View = Prompt.Input<int>("Enter TODO ID ");
                        //try to make prettier
                        Program.todoApp.Display(Program.todoApp.GetByID(input_View));
                        break;

                    case TODOMENU.MarkAsDone:
                        var inputDone = Prompt.Input<string>("Enter TODO ID(s) ");
                        foreach (var item in inputDone.Split())
                        {
                            var todoId = Program.todoApp.GetByID(int.Parse(item));
                            Console.WriteLine($"Completed:{todoId.Label}.");
                            Program.todoApp.MarkAsDone(todoId);
                        }
                        break;

                    case TODOMENU.ListAll:
                        var input_List = Prompt.Select<TodoOrder>("Select List Order: ", defaultValue: TodoOrder.CreateDate);
                        Program.todoApp.DisplayAllTodo();
                        break;
                }

                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}