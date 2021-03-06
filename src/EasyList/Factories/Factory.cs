using EasyList.Interfaces;

namespace EasyList.Factories
{
    internal static class Factory
    {
        public static ITodoLiteDBRepository CreateDBRepository()
        {
            return new TodoLiteDbRepository();
        }
        public static ITodoRepository CreateInMemoryRepository()
        {
            return new TodoRepository();
        }
        public static ITodoService CreateTodoServiceDB()
        {
            return new TodoServiceDB();
        }
        public static ITodoService CreateTodoServiceInMemory()
        {
            return new TodoServiceInMemory();
        }
    }
}
