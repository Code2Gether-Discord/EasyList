using EasyList;
using EasyList.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

public static class Validate
{
    static List<string> errors = new List<string>();
    public static bool TodoErrors()
    {
        if (errors.Any())
        {
            Console.WriteLine("The input has the following errors:");
            foreach (var error in errors)
            {
                Console.WriteLine("\t" + error);
            }

            return true;
        }
        return false;
    }
    public static void Add(Dictionary<string, string> input)
    {
        Label(input["label"]);
        DueDate(input["duedate"]);
    }

    public static bool DueDate(string? input)
    {
        if (DateTimeOffset.TryParse(input, out DateTimeOffset tempDate))
        {
            if (tempDate < DateTime.UtcNow)
            {
                errors.Add("Due date cannot be in the past");
                return false;
            }
        }
        return true;
    }

    public static bool Label(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            errors.Add("Label cannot be empty");
            return false;
        }
        return true;
    }

    public static IEnumerable<Todo> MultipleIds(IEnumerable<int> todoIdList)
    {
        var validTodoList = new List<Todo>();
        foreach (var todoId in todoIdList)
        {
            Todo? tempTodo = Id(todoId);
            if (tempTodo != null)
            {
                validTodoList.Add(tempTodo);
            }
        }
        return validTodoList;
    }

    public static Todo? Id(int todoId)
    {
        Todo? todoView = Program._todoService.GetTodoByID(todoId);
        if (todoView == null)
        {
            errors.Add($"Invalid Id #{todoId}");
        }
        return todoView;
    }
}
