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
            var parameterList = new List<string>() { "-d", "-t", "-p"};
            Dictionary<string, (int, int)> positions = new Dictionary<string, (int, int)>();
            bool flag = false;
            int index = 0;
            int startIndex = 0, endIndex = 0;

            for (; endIndex < args.Length; ++endIndex)
            {
                if (parameterList.Contains(args[endIndex].ToLower()))
                {
                    flag = !flag;
                    
                    if (flag)
                    {
                        startIndex = endIndex + 1;
                        if (positions.Count == 0)
                        {
                            //when the first parameter is found.
                            index = endIndex - 1;
                        }
                    }
                    else
                    {
                        positions.Add(args[startIndex - 1], (startIndex, endIndex - 1));
                        flag = !flag;
                        startIndex = endIndex + 1;
                        
                    }
                }
            }
            if (endIndex == args.Length && flag)
            {
                positions.Add(args[startIndex - 1], (startIndex, endIndex - 1));
                positions.Add("add", (0,index));
            }
            else
            {
                //processing for Label
                positions.Add("add", (startIndex,endIndex - 1));
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
            TodoPriority priority = positions.ContainsKey("-p") ? Enum.Parse<TodoPriority>(getData("-p").ToString().ToUpper()) : TodoPriority.Low;

            return new Dictionary<string, string> {
                                                    {"label" ,label },
                                                    {"description",description },
                                                    {"duedate",dueDate.ToString() },
                                                    {"priority",priority.ToString() }
                                                    };
        }
    }
}
