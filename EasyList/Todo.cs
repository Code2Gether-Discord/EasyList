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
        public Todo() // Defaults
        {
            Priority = Priority.Normal;
        }

        public int Id { get; init; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public Priority Priority { get; set; }
        public string Description { get; set; }
        
        //Outputs Id - Priority - Title - Status
        public override string ToString()
        {
            var output = new StringBuilder($"{Id} -");
            output.Append($" {Priority.ToString()} Priority - ");
            output.AppendLine($"{(!string.IsNullOrWhiteSpace(Title) ? Title : "-")}");
            output.Append($"Status: {(IsCompleted ? "Complete" : "In Progress")}");
            return output.ToString();
        }
        
    }
}