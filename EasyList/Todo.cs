using System.Text;

namespace EasyList
{
    public enum TodoPriority
    {
        Low,
        Normal,
        High
    }
    public class Todo
    {
        public int Id { get; } 
        public string Title { get; set; } 
        public bool Completion { get; set; }
        public TodoPriority Priority { get; set; }
        public string Description { get; set; }
        
        public Todo(int id)
        {
            Id = id;
            Title = string.Empty;
            Description = string.Empty;
            Completion = false;
            Priority = TodoPriority.Normal;
        }
        public Todo(int id, string title)
        {
            Id = id;
            Title = title;
            Description = string.Empty;
            Completion = false;
            Priority = TodoPriority.Normal;
        }
        public Todo(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
            Completion = false;
            Priority = TodoPriority.Normal;
        }
        public Todo(int id, string title, TodoPriority priority)
        {
            Id = id;
            Title = title;
            Description = string.Empty;
            Completion = false;
            Priority = priority;
        }
        public Todo(int id, string title, string description, TodoPriority priority)
        {
            Id = id;
            Title = title;
            Description = description;
            Completion = false;
            Priority = priority;
        }
        public override string ToString()
        {
            var output = new StringBuilder($"{Id}");
            var priorityChar = Priority switch
            {
                TodoPriority.Low => "L",
                TodoPriority.Normal => "N",
                TodoPriority.High => "H",
                _ => string.Empty
            };

            output.Append($"{priorityChar} ");
            output.AppendLine(Title);
            output.Append($"Status: {(Completion ? "Complete" : "In Progress")}");
            return output.ToString();
        }
        
    }
}