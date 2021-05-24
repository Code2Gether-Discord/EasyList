using System;
using System.Text;

namespace EasyList
{
    public enum Priority
    {
        Low,
        Normal,
        High
    }

    public class Todo : IComparable<Todo>
    {
        public Todo() // Defaults
        {
            Priority = Priority.Normal;
        }

        public int Id { get; init; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public Priority Priority { get; set; }
        public string Description { get; set; }

        
        //Sorts based on Priority, then  Id
        //Instead of this I would want for one to choose which field(s) to sort with - for later I guess
        public int CompareTo(Todo other)
        {
            if (ReferenceEquals(this, other)) return 0; // Same instance
            if (ReferenceEquals(null, other)) return 1;

            if (Priority != other.Priority)
            {
                if (Priority < other.Priority) return 1;
                if (Priority > other.Priority) return -1;
                return 0;
            }

            if (Id < other.Id) return 1;
            if (Id > other.Id) return -1;
            return 0;
        }

        public override string ToString()
        {
            var output = new StringBuilder($"{Id} -");
            output.Append($" {Priority.ToString()} Priority - ");
            output.AppendLine($"{(!string.IsNullOrWhiteSpace(Title) ? Title : "-")}");
            output.Append($"Status: {(IsCompleted ? "Complete" : "In Progress")}");
            return output.ToString();
        }


        // Same as The Compare
        public static bool operator >(Todo left, Todo right)
        {
            if (left.Priority != right.Priority) return left.Priority > right.Priority;
            return left.Id > right.Id;
        }

        public static bool operator <(Todo left, Todo right)
        {
            if (left.Priority != right.Priority) return left.Priority < right.Priority;
            return left.Id < right.Id;
        }
    }
}