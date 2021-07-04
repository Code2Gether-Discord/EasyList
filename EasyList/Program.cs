using System;
namespace EasyList
{
    class Program
    {
        public static ITodoRepository repository;
        public static TodoLogic todoApp;
        public static void Main(string[] args)
        {
            repository = new TodoRepository();
            todoApp = new TodoLogic(repository);

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