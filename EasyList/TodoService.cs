namespace EasyList
{
    internal abstract class TodoService
    {
        public abstract ITodoRepository CreateRepository();
        public abstract void Add(Todo todo);
        public abstract void Delete(Todo todo);
        public abstract void Display(Todo todo);
        public abstract void DisplayAllTodo(TodoOrder todoOrder);
        public abstract Todo? GetByID(int id);
        public abstract void MarkAsDone(Todo todo);
    }
}