using EasyList.DataModels;
using EasyList.Enums;
using System.Collections.Generic;

namespace EasyList.Interfaces
{
    internal interface ITodoRepository
    {
        void AddTodo(Todo todo);
        Todo? GetTodo(int Id);
        IEnumerable<Todo> GetAllTodo(TodoOrder orderOfList = TodoOrder.DueDate);
        void DeleteTodo(Todo todo);
    }
}
