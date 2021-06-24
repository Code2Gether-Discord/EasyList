using System;
using System.Collections.Generic;

namespace EasyList
{

    interface ITodoRepository
    {
        
        Todo Add(Todo newTodo);
        string Delete(int Id);
        Todo Get(int Id);
        Todo MarkAsDone(int Id);
        Todo Edit(int Id,string? Label = null, string? Description = null, DateTimeOffset? DueDate = null, TodoPriority priority = 0, TodoStatus status = TodoStatus.INPROGRESS);
        IEnumerable<Todo> GetAllTask(TodoOrder orderOfList = TodoOrder.DUEDATE);
    }
}
