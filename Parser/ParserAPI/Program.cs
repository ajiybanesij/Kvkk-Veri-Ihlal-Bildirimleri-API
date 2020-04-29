using ParserAPI.Controller;
using ParserAPI.Models;
using System;
using System.Collections.Generic;

namespace ParserAPI
{
    class Program
    {

        static void Main(string[] args)
        {
            MainController controller = new MainController();
            List<Summary> _sumList=controller.ParseData();
            foreach (var item in _sumList)
            {
                Console.WriteLine(item.title);
            }
            Console.ReadKey();
        }
    }
}
