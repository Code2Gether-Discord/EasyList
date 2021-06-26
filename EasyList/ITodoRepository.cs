using System;
using System.Collections.Generic;

namespace EasyList
{
    interface ITodoRepository
    {
        Todo Add(Todo todo);
        string Delete(Todo todo);
        Todo Get(int Id);
        Todo MarkAsDone(Todo todo);
        Todo Edit(int Id,string? Label = null, string? Description = null, DateTimeOffset? DueDate = null, TodoPriority priority = 0, TodoStatus status = TodoStatus.InProgress);
        IEnumerable<Todo> GetAllTask(TodoOrder orderOfList = TodoOrder.DueDate);
    }
}
