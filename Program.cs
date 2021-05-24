using System;
namespace EasyList
{
    class Program
    {
        static Repository inMemoryRepository = new Repository();//do better
        static void Main(string[] args)
        {
            while(true)
            {
                DisplayMenu();
                int choice = TakeValidInput();

                switch (choice)
                {
                    case 1:
                        {
                            Console.WriteLine("Enter the Task below:");
                            string taskLabel = Console.ReadLine();
                            inMemoryRepository.Add(taskLabel);
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Enter the Task ID below:");
                            int taskId = int.Parse(Console.ReadLine());
                            inMemoryRepository.Delete(taskId);
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Enter the Task ID below:");
                            int taskId = int.Parse(Console.ReadLine());
                            inMemoryRepository.Read(taskId);
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Enter the Task ID below:");
                            int taskId = int.Parse(Console.ReadLine());
                            inMemoryRepository.Done(taskId);
                            break;
                        }
                    case 5:
                        {
                            inMemoryRepository.ListAllTask();
                            break;
                        }
                    case 6:
                        {
                            Console.WriteLine("EXITING");
                            return;
                        }

                }
                Console.ReadKey();//do better than this
                Console.Clear();
            }
        }

        private static int TakeValidInput()
        {
            int choice = 0;
            while(true)
            {

                choice = int.Parse(Console.ReadLine());

                if(choice < 0 || choice > 6 )
                {
                    Console.WriteLine("Invalid option");
                    continue;
                }
                return choice;//add wrong input check
            }
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("Welcome to EasyList:");
            Console.WriteLine("What do you want to do:");
            Console.WriteLine("1.ADD");
            Console.WriteLine("2.DELETE");
            Console.WriteLine("3.READ A TASK");
            Console.WriteLine("4.MARK AS DONE");
            Console.WriteLine("5.LIST ALL TASK");
        }
    }
}