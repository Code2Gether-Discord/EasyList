using EasyList;
using EasyList.DataModels;
using Sharprompt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace EasyList
{
    public class Validate
    { 
        private static bool IsErrorFree(IEnumerable<string> errors)
        {
            if (errors.Any())
            {
                Console.WriteLine("The input has the following errors:");
                foreach (var error in errors)
                {
                    Console.WriteLine($"\t{error}");
                }

                return false;
            }
            return true;
        }
        
        private void ValidateDueDate(string? input, List<string> errors)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (DateTimeOffset.TryParse(input, out DateTimeOffset tempDate))
                {
                    //do better
                    if (tempDate < DateTime.UtcNow)
                    {
                        errors.Add("Due date cannot be in the past");
                    }
                    return;
                }
                errors.Add("Couldn't Parse Due Date.");
            }
            
        }

        private void ValidateLabel(string? input, List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                errors.Add("Label cannot be empty");
            }
        }

        public bool IsAddValid(Dictionary<string, string> toAdd)
        {
            var errors = new List<string>();
            ValidateLabel(toAdd["label"], errors);
            ValidateDueDate(toAdd["duedate"], errors);

            return IsErrorFree(errors);
        }
        
        public bool IsDeleteValid(IEnumerable<string> inputDelete, out List<Todo> validTodos)
        {
            validTodos = new List<Todo>();
            return ValidateMultipleIDs(inputDelete,validTodos);
        }
        private bool ValidateId(string todoId, List<string> errors,ref Todo? _todo)
        {
            if (!int.TryParse(todoId, out int validId))
            {
                errors.Add($"Couldn't Parse {todoId}");
                return false;
            }
            _todo = Program.TodoService.GetTodoByID(validId);
            if (_todo == null)
            {
                errors.Add($"Invalid Id #{validId}");
                return false;
            }
            return true;
        }
        private bool ValidateMultipleIDs(IEnumerable<string> inputDelete, List<Todo> validTodos)
        {
            var errors = new List<string>();
            foreach (var todoId in inputDelete)
            {
                Todo? _todo = null;
                if (ValidateId(todoId,errors,ref _todo))
                {
                    validTodos.Add(_todo!);
                }
            }
            if (IsErrorFree(errors))
            {
                return true;
            }
            Console.Clear();
            var choice = Prompt.Confirm("Continue with Valid Ids ? ");
            Thread.Sleep(1500);
            return choice;
        }

        public bool CanMarkAsDone(IEnumerable<string> inputUpdate, out List<Todo> validTodos)
        {
            validTodos = new List<Todo>();
                return ValidateMultipleIDs(inputUpdate,validTodos);
        }
        public bool IsReadValid(string todoID,out Todo validTodo)
        {
            var errors = new List<string>();
            validTodo = null;
            ValidateId(todoID, errors,ref validTodo);
            return IsErrorFree(errors);
        }

        public bool IsLabelValid(string label)
        {
            var errors = new List<string>();
            ValidateLabel(label, errors);
            return IsErrorFree(errors);
        }
        public bool IsDueDateValid(string dueDate)
        {
            var errors = new List<string>();
            ValidateDueDate(dueDate, errors);
            return IsErrorFree(errors);
        }
        public bool IsUpdateDescriptionValid(string description)
        {
            return string.IsNullOrWhiteSpace(description);
        }

    }
}