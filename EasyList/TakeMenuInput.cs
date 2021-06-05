using System;
namespace EasyList
{
    public class TakeMenuInput
    {
        public static void Input()
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
                            CommandHandler.Command(Console.ReadLine());
                            break;
                        }
                    case 2:
                    case 4:
                        {
                            Console.WriteLine("Enter the Task ID(s) below:");
                            CommandHandler.Command(Console.ReadLine());
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Enter the Task ID below:");
                            CommandHandler.Command(Console.ReadLine());
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("Enter the order to list Tasks:");
                            CommandHandler.Command(Console.ReadLine());
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
            bool isValidInput = false;
            while(!isValidInput)
            {

                isValidInput = int.TryParse(Console.ReadLine(), out choice);

                if(isValidInput && (choice < 1 || choice > 6 ))
                {
                    Console.WriteLine("Invalid Choice!!!");
                    Console.WriteLine("Enter Again");
                    continue;
                }
                break;
            }
            return choice;
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
            Console.WriteLine("6.EXIT");
        }
    }
}