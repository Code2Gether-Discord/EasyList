using System;
using System.Collections.Generic;

namespace EasyList
{
    internal interface ITodoRepository
    {
        void Add(Todo todo);
        Todo? GetTodo(int Id);
        IEnumerable<Todo> GetAllTodo(TodoOrder orderOfList = TodoOrder.DueDate);
        void Delete(Todo todo);
    }
}
