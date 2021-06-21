using System;
using System.Text.RegularExpressions;
namespace EasyList
{
    public class CommandHandler
    {
        static TodoRepository inMemoryRepository = new TodoRepository();
        static int relativeCount = 0;
        static TodoOrder lastOrder = TodoOrder.CREATEDATE;

        public void Command(string action)
        {
            var actionItem = action.Split(" ");
            var command = actionItem[0].ToLower();

            if (actionItem.Length == 1 && int.TryParse(actionItem[0], out int _))
            {
                command = "read";
            }

            switch (command)
            {
                //Parse Read Command
                //4
                case "read":
                    int realId = GetRealId(IArgumentParser.ParseRead(actionItem)) ?? throw new Exception("Real id is NULL");
                    Todo todo = inMemoryRepository.Get(realId);
                    Console.WriteLine($"Todo: {todo.Label}");
                    if (todo.Description != null)
                    {
                        Console.WriteLine($"Description: {todo.Description}");
                    }
                    if (todo.DueDate != DateTimeOffset.MaxValue)
                    {
                        Console.WriteLine($"DueDate: {todo.DueDate}");
                    }
                    Console.WriteLine($"Priority: {todo.Priority}");
                    break;
                //Parse Add Command
                //add go to library -d get the c# book -t 09:00pm -p HIGH  
                //add go to library -d get the c# book -t tomorrow -p HIGH  //this wont work
                //add go to library -d get the c# book -t 15m -p HIGH  //maybe this wont work 
                //in menu input the user should not be forced to use the add keyword 
                case "add":
                    Todo newtodo = IArgumentParser.ParseAdd(actionItem);
                    inMemoryRepository.Add(newtodo);
                    Console.WriteLine($"Todo: {newtodo.Label} Added");
                    break;
                //Parse Delete Command
                //del or delete
                //del 2
                //del 4 5
                //in menu input the user should not be forced to use the del keyword
                case "delete":
                case "del":
                    foreach (var item in IArgumentParser.ParseMultipleConsecutiveNumbers(actionItem.AsSpan(1)))
                    {
                        var tmp = GetRealId(item) ?? throw new Exception("Id not found for deleting");
                        Console.WriteLine($"Todo: {inMemoryRepository.Delete(tmp)} DELETED");
                    }
                    break;
                //Parse Mark As Done Command
                //done 1
                //done 1 4 
                //in menu input the user should not be forced to use the done keyword
                case "done":
                foreach (var item in IArgumentParser.ParseMarkAsDone(actionItem))
                {
                    var tmp = GetRealId(item) ?? throw new Exception("Id not found for marking as done");
                    Console.WriteLine($"Todo: {inMemoryRepository.MarkAsDone(tmp)} Completed");
                }
                    break;
                //Parse List All Task Command
                //list (default priority on enter)
                //list P or priority
                //list D or due
                //in menu input the user should not be forced to use the list keyword
                case "list":
                    relativeCount = 1;
                    lastOrder = IArgumentParser.ParseList(actionItem);
                    foreach (var item in inMemoryRepository.GetAllTask(lastOrder))
                    {
                        Console.WriteLine($"#{relativeCount} {item.Label}");
                        ++relativeCount;
                    }
                    break;
                default:
                    throw new NotImplementedException("This action is not implemented.");
            }
        }

        public static int? GetRealId(int id)
        {
            int temp = 1;
            foreach (var item in inMemoryRepository.GetAllTask(lastOrder))
            {
                if(temp == id)
                {
                    return item.Id;
                }
                ++temp;
            }
            return null;
            //need to check this before using
        }
    }
}
