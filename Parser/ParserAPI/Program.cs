using ParserAPI.Controller;
using System;

namespace ParserAPI
{
    class Program
    {

        static void Main(string[] args)
        {
            MainController controller = new MainController();
            controller.OneSummary();
            Console.ReadKey();
        }
    }
}
