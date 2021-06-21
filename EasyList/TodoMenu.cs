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
                    case TODOMENU.ADD:
                        var input_ADD = Prompt.Input<string>("Enter TODO ");
                        var parsed_ADD = ParseAdd.Parse(input_ADD.Split());
                        var newTodo = new Todo(parsed_ADD["label"],
                                        parsed_ADD["description"],
                                        DateTimeOffset.Parse(parsed_ADD["duedate"]),
                                        Enum.Parse<TodoPriority>(parsed_ADD["priority"]));
                        TodoRepository.Add(newTodo);
                        break;

                    case TODOMENU.DELETE:
                        var input_Delete = Prompt.Input<string>("Enter TODO ID(s) ");
                        foreach (var item in input_Delete.Split())
                        {
                            Console.WriteLine($"{TodoRepository.Delete(int.Parse(item))} is Deleted");
                        }
                        break;

                    case TODOMENU.VIEW:
                        var input_View = Prompt.Input<int>("Enter TODO ID ");
                        //try to make prettier
                        TodoDisplay.Display(TodoRepository.Get(input_View));
                        break;

                    case TODOMENU.MARK_AS_DONE:
                        var inputDone = Prompt.Input<string>("Enter TODO ID(s) ");
                        foreach (var item in inputDone.Split())
                        {
                            Console.WriteLine($"{TodoRepository.MarkAsDone(int.Parse(item))} is Completed.");
                        }
                        break;

                    case TODOMENU.LIST_ALL:
                        var input_List = Prompt.Select<TodoOrder>("Select List Order: ", defaultValue: TodoOrder.CREATEDATE);
                        TodoDisplay.Display(TodoRepository.GetAllTask(input_List));
                        break;
                }

                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}