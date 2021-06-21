using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace EasyList
{
    public class NoobArgumentParser : IArgumentParser
    {

        public IEnumerable<int> ParseMultipleConsecutiveNumbers(Span<string> args)
        {
            return args.ToArray().Select(x => int.Parse(x));
        }

        public int ParseGet(string args)
        {
            return int.Parse(args);
        }

        public TodoOrder ParseList(string[] args)
        {
            if (args.Length > 1)
            {
                if (args[1].ToLower() == "priorioty" || args[1].ToLower() == "p")
                {
                    return TodoOrder.PRIORITY;
                }
                else if (args[1].ToLower() == "due" || args[1].ToLower() == "d")
                {
                    return TodoOrder.DUEDATE;
                }
            }
             return TodoOrder.CREATEDATE;
        }

        //Make a better Parse Method
        public Dictionary<string, string> ParseAdd(string[] args)
        {
            // describes all valid parameters
            List<string> OptionalParameterList = new List<string>() { "-d", "-t", "-p", };
            Dictionary<string, (int, int)> positions = new Dictionary<string, (int, int)>();
            bool flag = false;
            bool foundLabel = false;
            int StartIndex = 0, EndIndex = 0;

            for (; EndIndex < args.Length; ++EndIndex)
            {
                if (OptionalParameterList.Contains(args[EndIndex].ToLower()))
                {
                    if(foundLabel == false) 
                    { 
                        foundLabel = true;
                        positions.Add("add", (StartIndex, EndIndex - 1));
                    }
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
            string? description = positions.ContainsKey("-d") ? getData("-d").ToString() : null;
            DateTimeOffset dueDate = positions.ContainsKey("-t") ? DateTimeOffset.Parse(getData("-t").ToString()) : DateTimeOffset.MaxValue;
            TodoPriority priority = positions.ContainsKey("-p") ? Enum.Parse<TodoPriority>(getData("-p").ToString().ToUpper()) : TodoPriority.LOW;

            return new Dictionary<string, string> {
                                                    {"label" ,label },
                                                    {"description",description },
                                                    {"duedate",dueDate.ToString() },
                                                    {"priority",priority.ToString() }
                                                    };
        }

        public IEnumerable<int> ParseDelete(string[] args) => ParseMultipleConsecutiveNumbers(args.AsSpan());


        public int ParseRead(string[] args) => ParseGet(args[1]);

        public IEnumerable<int> ParseMarkAsDone(string[] args) => ParseMultipleConsecutiveNumbers(args.AsSpan());

        public int ParseEdit(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
