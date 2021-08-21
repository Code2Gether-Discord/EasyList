using EasyList.Factories;
using EasyList.Interfaces;

namespace EasyList
{
    class Program
    {
        public static ITodoService TodoService => Factory.CreateTodoServiceDB();

        public static void Main(string[] args)
        {
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