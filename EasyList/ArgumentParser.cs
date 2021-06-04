using System;
using System.Text;
using System.Collections.Generic;

namespace EasyList
{
    public static class ArgumentParser
    {

        public static (string, string, DateTimeOffset, Todo.TodoPriority) ParseAdd(string[] args)
        {
            // describes all valid parameters
            List<string> OptionalParameterList = new List<string>() {"add", "-d", "-t", "-p", };
            Dictionary<string, (int, int)> positions = new Dictionary<string, (int, int)>();
            bool flag = false;
            int StartIndex = 0, EndIndex = 0;
            
            for (; EndIndex < args.Length; ++EndIndex)
            {
                if (OptionalParameterList.Contains(args[EndIndex].ToLower()))
                {
                    flag = !flag;
                    if (flag)
                    {
                        StartIndex = EndIndex + 1;
                    }
                    else
                    {
                        positions.Add(args[StartIndex - 1], (StartIndex, EndIndex - 1));
                        flag = !flag;
                        StartIndex = EndIndex + 1;
                    }
                }
            }
            if (EndIndex == args.Length)
            {
                positions.Add(args[StartIndex - 1], (StartIndex, EndIndex - 1));
            }

            StringBuilder getData(string parameter)
            {
                StringBuilder temp = new StringBuilder();
                for (int i = positions[parameter].Item1; i <= positions[parameter].Item2; ++i)
                {
                    temp.Append(args[i]);
                    temp.Append(" ");
                }
                return temp;
            }

            string label = getData("add").ToString().Trim();
            string description = positions.ContainsKey("-d") ? getData("-d").ToString() : null;
            DateTimeOffset dueDate = positions.ContainsKey("-t") ? DateTimeOffset.Parse(getData("-t").ToString()) : DateTimeOffset.MaxValue;
            Todo.TodoPriority priority = positions.ContainsKey("-p") ? Enum.Parse<Todo.TodoPriority>(getData("-p").ToString().ToUpper() ) : Todo.TodoPriority.LOW;

            return (label,description, dueDate, priority);
        }

        public static IEnumerable<int> ParseDelete(string[] args)
        {
            for (int i = 1; i < args.Length; i++)
            {
                    
                yield return int.Parse(args[i]);
            }
        }

        public static IEnumerable<int> ParseMarkAsDone(string[] args)
        {
            for (int i = 1; i < args.Length; i++)
            {

                yield return int.Parse(args[i]);
            }
        }

        public static int ParseGet(string args)
        {
            return int.Parse(args);
        }

        public static ITodoRepository.TodoOrder ParseList(string[] args)
        {
            if (args.Length > 1)
            {
                if (args[1].ToLower() == "priorioty" || args[1].ToLower() == "p")
                {
                    return ITodoRepository.TodoOrder.PRIORITY;
                }
                else if (args[1].ToLower() == "due" || args[1].ToLower() == "d")
                {
                    return ITodoRepository.TodoOrder.DUEDATE;
                }
            }
             return ITodoRepository.TodoOrder.CREATEDATE;
        }
    }
}
