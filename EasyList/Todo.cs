using System;
using System.Text;

namespace EasyList
{
    public class Todo
    {

        private static int TodoCount = 0;

        private readonly int _id;
        private string _label = string.Empty;
        private DateTimeOffset? _dueDate;
        //Add Task Duration relating with duedate
        // or allow both duration with input and set only the duedate

        public int Id => _id;
        public string Label
        {
            get => _label;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _label = value;
                }
                else
                {
                    //Add proper error messages for different scenarios
                    throw new ArgumentException("Invalid Label");
                }
            }
        }
        public string? Description { get; set; }
        public TodoPriority Priority { get; set; } = TodoPriority.Low;
               
        public DateTimeOffset CreatedDate { get; } = DateTimeOffset.UtcNow;
        
        public DateTimeOffset? DueDate
        {
            get => _dueDate;
            set
            {
                if (value > CreatedDate)
                {
                    _dueDate = value;
                }
                else
                {
                    throw new ArgumentException("Invalid Date! Due Date cannot be before Create Date.");
                }
            }
        }
        public TodoStatus Status { get; set; } = TodoStatus.InProgress;

        public Todo(string Label, string? Description = null, DateTimeOffset? DueDate = null, TodoPriority priority = TodoPriority.Low)
        {
            _id = ++TodoCount;
            this.Label = Label;
            this.Description = Description;
            this.Priority = priority;
            this.DueDate = DueDate;
            Status = TodoStatus.InProgress;
        }

        //public Todo(Todo newTodo)
        //{
        //    id = ++TodoCount;
        //    Label = newTodo.Label;
        //    Description = newTodo.Description;
        //    CreatedDate = newTodo.CreatedDate;
        //    DueDate = newTodo.DueDate;
        //    Status = newTodo.Status;
        //}
        
    }
}