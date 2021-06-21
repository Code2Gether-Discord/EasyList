using System;
using System.Collections.Generic;

namespace EasyList
{

    public interface ITodoRepository
    {
        
        public Todo Add(Todo newTodo);
        public string Delete(int Id);
        public Todo Get(int Id);
        public Todo MarkAsDone(int Id);
        public Todo Edit(int Id,string? Label = null, string? Description = null, DateTimeOffset? DueDate = null, TodoPriority priority = 0, TodoStatus status = TodoStatus.INPROGRESS);
        public IEnumerable<Todo> GetAllTask(TodoOrder orderOfList = TodoOrder.DUEDATE);
    }
}
