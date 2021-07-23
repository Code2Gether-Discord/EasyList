namespace EasyList.src.Interfaces
{
    internal interface ITodoService
    {
        void AddTodo(Todo todo);
        void DeleteTodo(Todo todo);
        void DisplayTodo(Todo todo);
        void DisplayAllTodo(TodoOrder todoOrder);
        Todo? GetTodoByID(int id);
        void MarkTodoAsDone(Todo todo);
    }
}