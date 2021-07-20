namespace EasyList
{
    interface ITodoService
    {
        public void AddTodo(Todo todo);
        public void DeleteTodo(Todo todo);
        public void DisplayTodo(Todo todo);
        public void DisplayAllTodo(TodoOrder todoOrder);
        public Todo? GetTodoByID(int id);
        public void MarkTodoAsDone(Todo todo);
    }
}