namespace EasyList
{
    class Program
    {
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