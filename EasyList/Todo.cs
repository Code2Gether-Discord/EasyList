namespace EasyList
{
    public class Todo
    {
        public int Id { get; } //Only set on creation

        public string Title { get; } // --

        public bool Completion { get; set; }
        
        public Todo(int id, string title)
        {
            Id = id;
            Title = title;
            Completion = false;
        }

        
        public override string ToString() => $"{Id} " + Title + $"\nStatus: {(Completion ? "Complete" : "In Progress")}";
        
    }
}