using System;
using System.Text.RegularExpressions;
namespace EasyList
{
    public class CommandHandler
    {
        static TodoRepository inMemoryRepository = new TodoRepository();
        static int relativeCount = 0;
        static ITodoRepository.TodoOrder lastOrder = ITodoRepository.TodoOrder.CREATEDATE;

        public static void Command(string action)
        {
            var actionItem = action.Split(" ");
            
            if (string.Compare(actionItem[0],"add",true) == 0)
            {
                //Parse Add Command
                //add go to library -d get the c# book -t 09:00pm -p HIGH  
                //add go to library -d get the c# book -t tomorrow -p HIGH  //this wont work
                //add go to library -d get the c# book -t 15m -p HIGH  //maybe this wont work 
                //in menu input the user should not be forced to use the add keyword 
                (string,string,DateTimeOffset,Todo.TodoPriority)parsed = ArgumentParser.ParseAdd(actionItem);
                Todo newtodo = new Todo(Label: parsed.Item1,
                                        Description: parsed.Item2,
                                        DueDate: parsed.Item3,
                                        priority: parsed.Item4);
                Todo todo = inMemoryRepository.Add(newtodo);
                Console.WriteLine($"Todo: {todo.Label} Added");
            }
            else if (string.Compare(actionItem[0], "del", true) == 0 || string.Compare(actionItem[0], "delete", true) == 0)
            {
                //Parse Delete Command
                //del or delete
                //del 2
                //del 4 5
                //in menu input the user should not be forced to use the del keyword
                foreach (var item in ArgumentParser.ParseMultipleConsecutiveNumbers(actionItem.AsSpan(1)))
                {
                    var _ = GetRealId(item) ?? throw new Exception("Id not found for deleting");
                    Console.WriteLine($"Todo: {inMemoryRepository.Delete(_)} DELETED");
                }
            }
            else if ((actionItem.Length == 1) && (int.TryParse(actionItem[0],out int temp)))
            {
                //Parse Read Command
                //4
                var _ = GetRealId(ArgumentParser.ParseGet(actionItem[0])) ?? throw new Exception("Id not found for reading.");
                Todo todo = inMemoryRepository.Get(_);
                Console.WriteLine($"Todo: {todo.Label}");
                if(todo.Description != null)
                {
                Console.WriteLine($"Description: {todo.Description}");
                }
                if (todo.DueDate != DateTimeOffset.MaxValue)
                {
                    Console.WriteLine($"DueDate: {todo.DueDate}");
                }
                Console.WriteLine($"Priority: {todo.Priority}");
            }
            else if (string.Compare(actionItem[0], "done", true) == 0)
            {
                //Parse Mark As Done Command
                //done 1
                //done 1 4 
                //in menu input the user should not be forced to use the done keyword

                foreach (var item in ArgumentParser.ParseMultipleConsecutiveNumbers(actionItem.AsSpan(1)))
                {
                    var _ = GetRealId(item) ?? throw new Exception("Id not found for marking as done");
                    Console.WriteLine($"Todo: {inMemoryRepository.MarkAsDone(_)} Completed");
                }
            }
            else if (string.Compare(actionItem[0], "list", true) == 0)
            {
                //Parse List All Task Command
                //list (default priority on enter)
                //list P or priority
                //list D or due
                //in menu input the user should not be forced to use the list keyword
                relativeCount = 1;
                lastOrder = ArgumentParser.ParseList(actionItem);
                foreach (var item in inMemoryRepository.GetAllTask(lastOrder))
                {
                    Console.WriteLine($"#{relativeCount} {item.Label}");
                    ++relativeCount;
                }
            }
            else
            {
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
