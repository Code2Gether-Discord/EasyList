using System.Text;

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
        
        protected Todo() // add a protected default constructor ?
        {
            //Id = 0;
            //Title = string.Empty;
            //Completion = false;
        }


        public override string ToString()
        {
            var output = new StringBuilder($"{Id} ");
            output.AppendLine(Title);
            output.Append($"Status: {(Completion ? "Complete" : "In Progress")}");
            return output.ToString();
        }
        
    }
}