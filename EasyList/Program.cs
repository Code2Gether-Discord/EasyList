using System;
namespace EasyList
{
    public class Program
    {

        public static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                //directly parse the string
            }
            else
            {
                TodoMenu.Run();
            }
        }
    }

}