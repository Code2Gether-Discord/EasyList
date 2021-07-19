using System;
namespace EasyList
{
    class Program
    {
        public static ITodoRepository repository;
        public static TodoService todoApp;
        public static void Main(string[] args)
        {
            repository = new TodoLiteDbRepository();
            todoApp = new TodoService(repository);

            if(args.Length > 1)
            {
                //directly parse the string command
            }
            else
            {
                TodoMenu.Run();
            }
        }
    }
}