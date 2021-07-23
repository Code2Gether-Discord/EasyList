using LiteDB;
using System;

namespace EasyList.src
{
    public class Todo
    {
        //The litedb itself handles assigning unique Ids to Todos,
        //Hence no need to keep count.
        //internal static int TodoCount = 0;

        //private readonly int _id;
        //Add Task Duration relating with duedate
        // or allow both duration with input and set only the duedate
        [BsonId]
        public int Id { get; init; }
        public string Label { get; set; }
        public string? Description { get; set; }
        public TodoPriority Priority { get; set; } = TodoPriority.Low;
        public DateTimeOffset CreatedDate { get; } = DateTimeOffset.Now;
        public DateTimeOffset? DueDate { get; set; }
        public TodoStatus Status { get; set; } = TodoStatus.InProgress;
        [BsonCtor]
        public Todo(string label, string? description = null, DateTimeOffset? dueDate = null, TodoPriority priority = TodoPriority.Low)
        {
            //The litedb itself handles assigning unique Ids to Todos.
            //Id = ++TodoCount;
            Label = label;
            Description = description;
            Priority = priority;
            DueDate = dueDate;
            Status = TodoStatus.InProgress;
        }
#nullable disable
        public Todo()
        {
        }
#nullable enable
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