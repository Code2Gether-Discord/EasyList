using LiteDB;
using System;
using System.Text;

namespace EasyList
{
    public class Todo
    {

        internal static int TodoCount = 0;

        //private readonly int _id;
        //Add Task Duration relating with duedate
        // or allow both duration with input and set only the duedate
        [BsonId]
        public int Id { get; init; }
        public string Label { get; set; }
        public string? Description { get; set; }
        public TodoPriority Priority { get; set; } = TodoPriority.Low;
               
        public DateTimeOffset CreatedDate { get; } = DateTimeOffset.Now;
        
        public DateTimeOffset? DueDate { get; set;}
        public TodoStatus Status { get; set; } = TodoStatus.InProgress;

        [BsonCtor]
        public Todo(string Label, string? Description = null, DateTimeOffset? DueDate = null, TodoPriority priority = TodoPriority.Low)
        {
            Id = ++TodoCount;
            this.Label = Label;
            this.Description = Description;
            this.Priority = priority;
            this.DueDate = DueDate;
            Status = TodoStatus.InProgress;
        }

        public Todo()
        {
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