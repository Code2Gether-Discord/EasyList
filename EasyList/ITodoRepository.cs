using System;
using System.Collections.Generic;

namespace EasyList
{
    interface ITodoRepository
    {
        void Add(Todo todo);
        Todo? GetTodo(int Id);
        IEnumerable<Todo> GetAllTodo(TodoOrder orderOfList = TodoOrder.DueDate);
        void Update(Todo todo);
        void Delete(Todo todo);
    }
}
