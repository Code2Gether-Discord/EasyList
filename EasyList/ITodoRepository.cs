using System;
using System.Collections.Generic;

namespace EasyList
{
    interface ITodoRepository
    {
        void Add(Todo todo);
        void Delete(Todo todo);
        Todo Get(int Id);
        void MarkAsDone(Todo todo);
        void Edit(int Id,string? Label = null, string? Description = null, DateTimeOffset? DueDate = null, TodoPriority priority = 0, TodoStatus status = TodoStatus.InProgress);
        IEnumerable<Todo> GetAllTask(TodoOrder orderOfList = TodoOrder.DueDate);
    }
}
