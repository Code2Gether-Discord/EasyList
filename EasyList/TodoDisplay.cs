using System;
using System.Collections.Generic;

namespace EasyList
{
    public static class TodoDisplay
    {
        public static void Display(Todo newTodo)
        {
            Console.WriteLine($"Id: {newTodo.Id}");
            if(!string.IsNullOrEmpty(newTodo.Description))
                Console.WriteLine($"Description: {newTodo.Description}");
            Console.WriteLine($"Priority: {newTodo.Priority}");
            if(newTodo.DueDate != null)
                Console.WriteLine($"DueDate: {newTodo.DueDate}");
        }

        public static void Display(IEnumerable<Todo> todoList)
        {
            foreach (Todo todo in todoList)
            {
                Display(todo);
                Console.WriteLine();
            }
            
        }
    }
}