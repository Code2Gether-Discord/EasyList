namespace EasyList.src.Interfaces
{
    interface ITodoLiteDBRepository : ITodoRepository
    {
        void UpdateTodo(Todo todo);
    }
}
