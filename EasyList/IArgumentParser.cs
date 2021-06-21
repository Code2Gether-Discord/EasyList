using System;
using System.Collections.Generic;

namespace EasyList
{
    public interface IArgumentParser
    {
        public Dictionary<string, string> ParseAdd(string[] args);
        public IEnumerable<int> ParseDelete(string[] args);
        public int ParseRead(string[] args);
        public IEnumerable<int> ParseMarkAsDone(string[] args);
        public int ParseEdit(string[] args);

        public TodoPriority List(string[] args);
    }
}