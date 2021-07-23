using EasyList.DataModels;

namespace EasyList.Interfaces
{
    interface ITodoLiteDBRepository : ITodoRepository
    {
        void UpdateTodo(Todo todo);
    }
}
