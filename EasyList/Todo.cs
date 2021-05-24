using System.Text;

namespace EasyList
{
    public enum Priority
    {
        Low,
        Normal,
        High
    }
    public class Todo
    {
        public int Id { get; init; } 
        public string Title { get; set; } 
        public bool IsCompleted { get; set; }
        public Priority Priority { get; set; }
        public string Description { get; set; }
        
        public Todo() // Defaults
        {
            Title = null;
            Description = null;
            IsCompleted = false;
            Priority = Priority.Normal;
        }
        public Todo(int id) : this()
        {
             Id = id;
        }
        
        public override string ToString()
        {
            var output = new StringBuilder($"{Id}");
            var priorityChar = Priority switch
            {
                Priority.Low => "L",
                Priority.Normal => "N",
                Priority.High => "H",
                _ => string.Empty
            };

            output.Append($"{priorityChar} ");
            output.AppendLine($"{(!string.IsNullOrEmpty(Title) ? Title : "-")}");
            output.Append($"Status: {(IsCompleted ? "Complete" : "In Progress")}");
            return output.ToString();
            
        }
        
    }
}