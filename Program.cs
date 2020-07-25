using System;

namespace WeirdoAlert
{
    class Program
    {
        public static Logger MainLogger = new Logger();
        static void Main(string[] args)
        {
            MainLogger.Log("Application Started!");
            Console.WriteLine("Application Started!");
            throw new NotImplementedException();
        }
    }
}
