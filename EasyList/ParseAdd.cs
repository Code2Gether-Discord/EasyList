using System;
using System.Collections.Generic;
using System.Text;

namespace EasyList
{
    public static class ParseAdd
    {
        public static Dictionary<string, string> Parse(string[] args)
        {
            // describes all valid parameters
            List<string> ParameterList = new List<string>() { "add", "-d", "-t", "-p", };
            Dictionary<string, (int, int)> positions = new Dictionary<string, (int, int)>();
            bool flag = false;
            int StartIndex = 0, EndIndex = 0;

            for (; EndIndex < args.Length; ++EndIndex)
            {
                if (ParameterList.Contains(args[EndIndex].ToLower()))
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
    }
}
