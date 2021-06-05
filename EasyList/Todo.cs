using System;
using System.Text;

namespace EasyList
{
    public class Todo
    {
        public enum TodoStatus : Byte
        {
            INPROGRESS,
            DONE,
        }
        public enum TodoPriority
        {
            LOW,
            L = LOW,
            NORMAL,
            N = NORMAL,
            HIGH,
            H = HIGH,
        }

        private static int TodoCount = 0;

        private int id;
        private string label;
        private string description;
        private DateTimeOffset dueDate;
        //Add Task Duration relating with duedate
        // or allow both duration with input and set only the duedate

        public int Id => id;
        public string Label
        {
            get => label;
            set
            {
                if (!(string.IsNullOrEmpty(value)))
                {
                    label = value;
                }
                else
                {
                    //figure out why ???
                    throw new Exception("Invalid Label");
                }
            }
        }
        public string Description { get => description; set => description = value; }
        public TodoPriority Priority { get; set; } = TodoPriority.LOW;
               
        public DateTimeOffset CreatedDate { get; } = DateTimeOffset.UtcNow;
        
        public DateTimeOffset DueDate
        {
            get => dueDate;
            set
            {
                if (value > this.CreatedDate)
                {
                    dueDate = value;
                }
                else
                {
                    throw new Exception("Invalid Date");
                }
            }
        }
        public TodoStatus Status { get; set; } = TodoStatus.INPROGRESS;

        public Todo(string Label, string Description = null, DateTimeOffset? DueDate = null, TodoPriority priority = TodoPriority.LOW)
        {
            this.id = ++TodoCount;
            this.Label = Label;
            this.Description = Description;
            this.Priority = priority;
            this.DueDate = DueDate ?? DateTimeOffset.MaxValue;
            this.Status = TodoStatus.INPROGRESS;
        }

        public Todo(Todo newTodo)
        {
            this.id = ++TodoCount;
            this.Label = newTodo.Label;
            this.Description = newTodo.Description;
            this.CreatedDate = newTodo.CreatedDate;
            this.DueDate = newTodo.DueDate;
            this.Status = newTodo.Status;
        }
        
    }
}