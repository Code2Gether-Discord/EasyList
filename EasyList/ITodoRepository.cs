using System;
using System.Collections.Generic;

namespace EasyList
{
    public interface ITodoRepository
    {
        public enum TodoOrder : Byte
        {
            CREATEDATE,
            DUEDATE,
            PRIORITY
        }
        public Todo Add(Todo newTodo);
        public string Delete(int Id);
        public Todo Get(int Id);
        public Todo MarkAsDone(int Id);
        public Todo Edit(int Id,string? Label = null, string? Description = null, DateTimeOffset? DueDate = null, Todo.TodoPriority priority = 0, Todo.TodoStatus status = Todo.TodoStatus.INPROGRESS);
        public IEnumerable<Todo> GetAllTask(TodoOrder orderOfList = TodoOrder.DUEDATE);
    }
}
