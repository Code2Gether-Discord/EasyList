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
            IsCompleted = false;
            Priority = Priority.Normal;
        }
        
        public override string ToString()
        {
       
            var output = new StringBuilder($"{Id} -");
            var priorityChar = Priority switch
            {
                Priority.Low => "Low",
                Priority.Normal => "Normal",
                Priority.High => "High",
                _ => string.Empty
            };

            output.Append($" {priorityChar} Priority - ");
            output.AppendLine($"{(!string.IsNullOrWhiteSpace(Title) ? Title : "-")}");
            output.Append($"Status: {(IsCompleted ? "Complete" : "In Progress")}");
            return output.ToString();
            
        }
        
        
    }
}