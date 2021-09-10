using EasyList.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace EasyList
{
    public static class OptionParser
    {
        /// <summary>
        /// Gets the values from the input string out of a choosen set of options
        /// Use .TryGetValue(nameof(Class.PropertyName)) on the returned result
        /// </summary>
        /// <param name="options"></param>
        /// <param name="inputs"></param>
        /// <param name="prefixToRemove"></param>
        /// <returns>The option ToTitleCase if a prefix will be removed and its corresponding value in a dictionary.</returns>
        public static Dictionary<string, string> Parse(string[] options, string[] inputs, string prefixToRemove = null)
        {
            var optionsValues = new Dictionary<string, string>();

            var optionIndexes = options
                .Where(x => inputs.Contains(x))
                .Select((value) => (value, index: Array.IndexOf(inputs, value)))
                .OrderBy(x => x.index).ToArray();

            for (int i = 0; i < optionIndexes.Length; i++)
            {
                var valueStartIndex = 0;
                var valueEndIndex = 0;
                var nextIndex = i + 1;

                if (nextIndex == optionIndexes.Length) //last option
                {
                    valueStartIndex = optionIndexes[i].index + 1; //value comes after the option
                    valueEndIndex = inputs.Length;
                }
                else
                {
                    valueStartIndex = optionIndexes[i].index + 1;
                    valueEndIndex = optionIndexes[nextIndex].index;
                }

                var values = inputs[valueStartIndex..valueEndIndex];

                if (values.Length < 1) //no value inputted
                {
                    continue;
                }

                var value = string.Join(' ', values);

                if (!string.IsNullOrWhiteSpace(prefixToRemove))
                {
                    var formattedOption = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(optionIndexes[i].value.Replace(prefixToRemove, string.Empty));
                    optionsValues.Add(formattedOption, value);
                }
                else
                {
                    optionsValues.Add(optionIndexes[i].value, value);
                }
            }

            return optionsValues;
        }
    }

    public static class ParseAdd
    {
        public static Dictionary<string, string> Parse(string[] args)
        {
            // describes all valid parameters
            var parameterList = new List<string>() { "-d", "-t", "-p" };
            Dictionary<string, (int parameterStartIndex, int parameterEndIndex)> positions = new();
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
                positions.Add("add", (0, index));
            }
            else
            {
                //processing for Label
                positions.Add("add", (startIndex, endIndex - 1));
            }

            string label = GetData("add", args, positions) ?? string.Empty;

            string description = GetData("-d", args, positions) ?? string.Empty;
            string dueDate = GetData("-t", args, positions) ?? string.Empty;
            string priority = GetData("-p", args, positions) ?? $"{TodoPriority.Low}";

            return new Dictionary<string, string> {
                                                    {"label" ,label },
                                                    {"description",description },
                                                    {"duedate",dueDate },
                                                    {"priority",priority }
                                                    };
        }
        private static string? GetData(string parameter, string[] args, Dictionary<string, (int startIndex, int endIndex)> positions)
        {
            if (positions.ContainsKey(parameter))
            {
                StringBuilder temp = new();
                for (int i = positions[parameter].startIndex; i <= positions[parameter].endIndex; ++i)
                {
                    temp.Append(args[i]);
                    temp.Append(' ');
                }
                return $"{temp}";
            }
            return null;
        }
    }
}
